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
            InitListView(msgListView, msgImageList);

#if DEBUG
            path = Common.GetApplicationPath() + "AppData/Config.xml";
#else
            path = AppDomain.CurrentDomain.BaseDirectory + "AppData/Config.xml";
#endif
            XDocument doc = XDocument.Load(path);

            XElement mqttConfig = doc.Element("MqttConfig");
            if (mqttConfig != null)
            {
                txtAddress.Text = mqttConfig.Element("Address") != null ? mqttConfig.Element("Address")?.Value : "";
                txtClientId.Text = mqttConfig.Element("ClientId") != null ? mqttConfig.Element("ClientId")?.Value : "";
                txtUserName.Text = mqttConfig.Element("UserName") != null ? mqttConfig.Element("UserName")?.Value : "";
                txtPassword.Text = mqttConfig.Element("Password") != null ? mqttConfig.Element("Password")?.Value : "";
                //txtAddress.Text = mqqttConfig.Element("IsTls").Value;

                decimal port = 1883;
                if (!string.IsNullOrWhiteSpace(mqttConfig.Element("Port")?.Value))
                {
                    decimal.TryParse(mqttConfig.Element("Port")?.Value, out port);
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

                    if (await mqttClientService.MqttClientStart(config))
                        mqttClientService.IsStart = true;//记录开始状态

                }).Start();
            }
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            if (!mqttClientService.IsStart)
            {
                return;
            }

            if (mqttClientService.mqttClient.IsConnected)
            {
                await mqttClientService.MqttClientStop();
                mqttClientService = new MqttClientService();
                //mqttClientService.IsStart = false;
            }
        }


        private async void txtSubscribe_Click(object sender, EventArgs e)
        {
            if (!mqttClientService.IsStart)
            {
                return;
            }

            if (mqttClientService.mqttClient.IsConnected)
            {
                if (await mqttClientService.SubscribeAsync(txtTopic.Text, MqttQualityOfServiceLevel.AtLeastOnce))
                {
                    listSubscribe.Items.Add(txtTopic.Text);
                }
            }
            else
            {
                MessageBox.Show("链接已断开");
            }
        }

        private async void btnUnSubscribe_Click(object sender, EventArgs e)
        {
            if (!mqttClientService.IsStart)
            {
                return;
            }
            //判断是否已订阅         
            if (listSubscribe.Items.Count == 0)
            {
                MessageBox.Show("请先订阅节点");
                return;
            }
            if (listSubscribe.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要取消订阅的节点");
                return;
            }
            var topic = listSubscribe.SelectedItem.ToString();

            if (mqttClientService.mqttClient.IsConnected)
            {
                if (await mqttClientService.UnsubscribeAsync(topic))
                {
                    listSubscribe.Items.Remove(topic);
                }
            }
            else
            {
                MessageBox.Show("链接已断开");
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            //判断是否已订阅         
            if (listSubscribe.Items.Count == 0)
            {
                MessageBox.Show("请先订阅主题");
                return;
            }
            if (!listSubscribe.Items.Contains(txtTopic.Text))
            {
                MessageBox.Show("订阅主题不存在");
                return;
            }

            if (!mqttClientService.IsStart)
            {
                return;
            }

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
            msgListView.Items.Clear();
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
                   Addlog(m.Type, m.Data);
                   //txtMessage.Text += m.Data + "\r\n";
                   //PrintLog(0, m.Data);                 
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

        private void listSubscribe_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listSubscribe.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                // MessageBox.Show(this.listSubscribe.SelectedItem.ToString()); //执行双击事件
                txtTopic.Text = listSubscribe.SelectedItem.ToString();
            }
            else
            {
                listSubscribe.SelectedIndex = -1;//不做任何操作，将ListBox的选中项取消
            }
        }

        #region 日志       

        private void InitListView(ListView listView, ImageList imageList)
        {
            ColumnHeader logTime = new ColumnHeader() { Name = "logTime", Text = "时间", Width = 240 };
            ColumnHeader logMsg = new ColumnHeader() { Name = "logMsg", Text = "消息", Width = 850 };
            listView.Columns.AddRange(new ColumnHeader[] { logTime, logMsg });
            listView.View = View.Details;
            listView.SmallImageList = imageList;
        }

        private void Addlog(int imageIndex, string info)
        {
            Color color;
            switch (imageIndex)
            {
                case 0:
                    color = Color.Red;
                    break;
                case 2:
                    color = Color.Green;
                    break;
                case 3:
                    color = Color.Orange;
                    break;
                default:
                    color = Color.Black;
                    break;
            }
            Addlog(msgListView, imageIndex, info, color);
        }

        private void Addlog(ListView listView, int imageIndex, string info, Color color, int maxDisplayItems = 100)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new Action(() =>
                {
                    if (listView.Items.Count > maxDisplayItems)
                    {
                        listView.Items.RemoveAt(maxDisplayItems);
                    }

                    ListViewItem listItem = new ListViewItem(" " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), imageIndex);
                    listItem.ForeColor = color;
                    listItem.SubItems.Add(info);
                    listView.Items.Insert(0, listItem);
                }));
            }
            else
            {
                if (listView.Items.Count > maxDisplayItems)
                {
                    listView.Items.RemoveAt(maxDisplayItems);
                }

                ListViewItem listItem = new ListViewItem(" " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), imageIndex);
                listItem.ForeColor = color;
                listItem.SubItems.Add(info);
                listView.Items.Insert(0, listItem);
            }
        }

        #endregion      

    }
}
