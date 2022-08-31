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
using System.Xml;
using System.Xml.Linq;

namespace OPCForm
{
    public partial class Form2 : Form
    {
        MqttClientService mqttClientService = new MqttClientService();
        //private string path = AppDomain.CurrentDomain.BaseDirectory + "AppData/Config.xml";
        private string path;
        public Form2()
        {
            InitializeComponent();

#if DEBUG
            path = Common.GetApplicationPath() + "AppData/Config.xml";
#else
          path = AppDomain.CurrentDomain.BaseDirectory + "AppData/Config.xml";
#endif
            XDocument doc = XDocument.Load(path);

            XElement mqqttConfig = doc.Element("MqttConfig");
            if (mqqttConfig != null)
            {
                txtAddress.Text = mqqttConfig.Element("Address") != null ? mqqttConfig.Element("Address")?.Value : "";
                txtClientId.Text = mqqttConfig.Element("ClientId") != null ? mqqttConfig.Element("ClientId")?.Value : "";
                txtUserName.Text = mqqttConfig.Element("UserName") != null ? mqqttConfig.Element("UserName")?.Value : "";
                txtPassword.Text = mqqttConfig.Element("Password") != null ? mqqttConfig.Element("Password")?.Value : "";
                //txtAddress.Text = mqqttConfig.Element("IsTls").Value;

                decimal port = 1883;
                if (!string.IsNullOrWhiteSpace(mqqttConfig.Element("Port")?.Value))
                {
                    decimal.TryParse(mqqttConfig.Element("Port")?.Value, out port);
                }
                txtPort.Value = port;
            }
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

        private async void btnUnSubscribe_Click(object sender, EventArgs e)
        {
            if (mqttClientService.mqttClient.IsConnected)
            {
                await mqttClientService.UnsubscribeAsync(txtTopic.Text);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {               
                XDocument doc = XDocument.Load(path);
                XElement xel = doc.Element("MqttConfig");
                xel.SetElementValue("Address", txtAddress.Text.Trim());
                xel.SetElementValue("Port", txtPort.Value);
                xel.SetElementValue("ClientId", txtClientId.Text.Trim());
                xel.SetElementValue("UserName", txtUserName.Text.Trim());
                xel.SetElementValue("Password", txtPassword.Text.Trim());
                xel.SetElementValue("IsTls", false);
                doc.Save(path);

                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存异常" + ex.Message);
            }
        }

    }
}
