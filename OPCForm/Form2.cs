using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using OPCForm.Mqtt;
using OPCForm.Mqtt.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPCForm
{
    public partial class Form2 : Form
    {
        MqttClientService mqttClientService = new MqttClientService();
        public Form2()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (!mqttClientService.IsStart)
            {
                new Task(async () =>
                {
                    var config = new MqttConfig()
                    {
                        Address = txtAddress.Text,
                        Port = (int)txtPort.Value,
                        ClientId = txtClientId.Text,
                        UserName = txtUserName.Text,
                        Password = txtPassword.Text
                    };
                    // 启动Mqtt             
                    mqttClientService.MqttMessage += Mqtt_Message;// 订阅消息
                    await mqttClientService.MqttClientStart(config);
                    mqttClientService.IsStart = true;//记录开始状态

                }).Start();
            }
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            if (mqttClientService.mqttClient.IsConnected)
            {
                await mqttClientService.MqttClientStop();
                mqttClientService.IsStart = false;             
            }
        }


        private async void txtSubscribe_Click(object sender, EventArgs e)
        {
            if (mqttClientService.mqttClient.IsConnected)
            {
                await mqttClientService.SubscribeAsync(txtTopic.Text, MqttQualityOfServiceLevel.AtLeastOnce);
            }
            else
            {
                MessageBox.Show("链接已断开");
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (mqttClientService.mqttClient.IsConnected)
            {
                if (!string.IsNullOrWhiteSpace(txtSend.Text.Trim()))
                {
                    if (await mqttClientService.PublishAsync(txtTopic.Text, txtSend.Text.Trim()))
                    {
                        txtSend.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("请输入消息内容");
                }
            }
            else
            {
                MessageBox.Show("链接已断开");
            }
        }

        private void txtClear_Click(object sender, EventArgs e)
        {
            txtMessage.Text = "";
        }

        /// <summary>
        /// Mqtt消息回调
        /// </summary>
        /// <param name="sender"></param>
        private void Mqtt_Message(object sender)
        {
            BeginInvoke(new Action(() =>
            {
                if (sender.GetType() == typeof(MqttSignal))
                {
                    MqttSignal m = sender as MqttSignal;
                    txtMessage.Text += m.Data + "\r\n";
                }
            }));
        }


    }
}
