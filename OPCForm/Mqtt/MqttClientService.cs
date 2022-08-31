using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using OPCForm.Mqtt.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCForm.Mqtt
{
    public class MqttClientService
    {
        /// <summary>
        /// 消息调用的事件
        /// </summary>
        /// <param name="sender"></param>
        public delegate void InformHandle(object sender);

        #region 消息
        /// <summary>
        /// 返回的消息
        /// </summary>
        public event InformHandle MqttMessage;

        public bool IsStart = false;
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="data"></param>
        public virtual void ToSignal(object data)
        {
            if (MqttMessage != null) MqttMessage(data);
        }
        #endregion

        #region 环境变量
        /// <summary>
        /// mqtt连接信息
        /// </summary>
        public IMqttClient mqttClient { get; set; }

        #endregion

        #region 私有方法
        /// <summary>
        /// 时间差计算
        /// </summary>
        /// <param name="tim"></param>
        /// <returns></returns>
        private long TimeSecondDif(DateTime tim)
        {
            return (DateTime.Now.Ticks - tim.Ticks) / 10000000;
        }
        #endregion

        public async Task<bool> MqttClientStart(MqttConfig config)
        {
            try
            {
                var optionsBuilder = new MqttClientOptionsBuilder()
                    .WithTcpServer(config.Address, config.Port) // 要访问的mqtt服务端的 ip 和 端口号
                    .WithCredentials(config.UserName, config.Password) // 要访问的mqtt服务端的用户名和密码
                    .WithClientId(config.ClientId) // 设置客户端id
                    .WithCleanSession()
                    .WithTls(new MqttClientOptionsBuilderTlsParameters
                    {
                        UseTls = config.IsTls  // 是否使用 tls加密
                    });

                var clientOptions = optionsBuilder.Build();
                mqttClient = new MqttFactory().CreateMqttClient();

                mqttClient.ConnectedAsync += _mqttClient_ConnectedAsync; // 客户端连接成功事件
                mqttClient.DisconnectedAsync += _mqttClient_DisconnectedAsync; // 客户端连接关闭事件          
                mqttClient.ApplicationMessageReceivedAsync += _mqttClient_ApplicationMessageReceivedAsync; // 收到消息事件
              
                await mqttClient.ConnectAsync(clientOptions);

                ToSignal(new MqttSignal() { Type = 1, Data = $"客户端[{clientOptions.ClientId}]尝试连接..." });
                DateTime OutTime = DateTime.Now;

                while (mqttClient != null && !mqttClient.IsConnected)
                {
                    // 30秒链接超时
                    if (TimeSecondDif(OutTime) >= 30) return false;
                    Thread.Sleep(500);
                }

                Thread.Sleep(100);
                if (mqttClient == null)
                {
                    ToSignal(new MqttSignal() { Type = 0, Data = $"客户端连接超时..." });
                    Log.Error($"客户端连接超时...");
                    return false;
                }
                ToSignal(new MqttSignal() { Type = 1, Data = $"客户端[{clientOptions.ClientId}]连接成功..." });
                return true;
            }
            catch (Exception ex)
            {
                ToSignal(new MqttSignal() { Type = 0, Data = $"客户端尝试连接出错：{ex.Message}" });
                Log.Error($"客户端尝试连接出错：{ex.Message}");
            }
            return false;
        }

        /// <summary>
        /// 客户端连接关闭事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task _mqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            ToSignal(new MqttSignal() { Type = 1, Data = $"客户端已断开与服务端的连接……" });
            //Log.Information($"客户端已断开与服务端的连接……");
            await Task.CompletedTask;
        }


        /// <summary>
        /// 客户端连接成功事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task _mqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            ToSignal(new MqttSignal() { Type = 1, Data = $"客户端已连接服务端……" });
            //Log.Information($"客户端已连接服务端……");

            await Task.CompletedTask;
        }

        /// <summary>
        /// 收到消息事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task _mqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            var resutl = $"客户端ID=【{arg.ClientId}】接收到消息。 Topic主题=【{arg.ApplicationMessage.Topic}】 消息=【{Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}】 qos等级=【{arg.ApplicationMessage.QualityOfServiceLevel}】";
            ToSignal(new MqttSignal() { Type = 1, Data = resutl });
            //Log.Information(resutl);
            await Task.CompletedTask;
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public async Task<bool> SubscribeAsync(string topic, MqttQualityOfServiceLevel qosLevel = MqttQualityOfServiceLevel.AtLeastOnce)
        {
            try
            {
                ToSignal(new MqttSignal() { Type = 1, Data = string.Format("客户端[{0}]订阅主题[{1}]成功！", mqttClient.Options.ClientId, topic) });
                await mqttClient.SubscribeAsync(topic, qosLevel);
                return true;
            }
            catch (Exception ex)
            {
                ToSignal(new MqttSignal() { Type = 0, Data = $"客户端订阅主题出错：{ex.Message}" });
            }
            return false;

        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public async Task<bool> UnsubscribeAsync(string topic)
        {
            try
            {
                await mqttClient.UnsubscribeAsync(topic);
                ToSignal(new MqttSignal() { Type = 1, Data = string.Format("客户端[{0}]取消主题[{1}]成功！", mqttClient.Options.ClientId, topic) });
                return true;
            }
            catch (Exception ex)
            {
                ToSignal(new MqttSignal() { Type = 0, Data = $"客户端取消主题出错：{ex.Message}" });
            }
            return false;
        }

        /// <summary>
        /// 关闭链接
        /// </summary>
        /// <returns></returns>
        public async Task<bool> MqttClientStop()
        {
            try
            {
                await mqttClient.DisconnectAsync();
                ToSignal(new MqttSignal() { Type = 1, Data = $"客户端断开连接..." });
                return true;
            }
            catch (Exception ex)
            {
                ToSignal(new MqttSignal() { Type = 1, Data = $"客户端断开连接异常..." + ex.Message });
            }
            return false;
        }

        public async Task<bool> PublishAsync(string topic, string data, MqttQualityOfServiceLevel qosLevel = MqttQualityOfServiceLevel.AtLeastOnce, bool isRetain = false)
        {
            try
            {
                var message = new MqttApplicationMessage
                {
                    Topic = topic,
                    Payload = Encoding.Default.GetBytes(data),
                    QualityOfServiceLevel = qosLevel,
                    Retain = isRetain  //服务端是否保留消息。true为保留，如果有新的订阅者连接，就会立马收到该消息。
                };
                await mqttClient.PublishAsync(message);
                ToSignal(new MqttSignal() { Type = 1, Data = string.Format("客户端[{0}]，主题[{1}]发送消息：【{2}】", mqttClient.Options.ClientId, topic, data) });
                return true;
            }
            catch (Exception ex)
            {
                ToSignal(new MqttSignal() { Type = 0, Data = string.Format("客户端[{0}]，主题[{1}]发送消息[{2}]异常！>{3}", mqttClient.Options.ClientId, topic, data, ex.Message) });
            }
            return false;
        }
    }


}
