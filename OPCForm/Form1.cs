using Opc.UaFx;
using Opc.UaFx.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Xml.Linq;
using OPC.Repository;
using OPC.Data;
using Serilog;

namespace OPCForm
{
    public partial class Form1 : Form
    {
        public static bool mqtt = false;

        private OpcClient client;
        private static List<ConsumerSubscriptionInfo> consumerList = new List<ConsumerSubscriptionInfo>();
        private static Dictionary<string, string> nodeList = new Dictionary<string, string>();
        private static List<SubscriptionInfo> subscriptionList = new List<SubscriptionInfo>();
        private static string currentNodeId;
        private bool IsLoop = false;
        private string path = AppConfig.GetXmlConfig;
        private NodeInfo _nodeInfo;

        public Form1()
        {
            InitializeComponent();
            //初始化opcClient
            this.client = new OpcClient();
            //初始化listview
            InitListView(logListView, logImageList);

            XDocument doc = XDocument.Load(path);
            XElement mqttConfig = doc.Element("Root").Element("OpcConfig");
            if (mqttConfig != null)
            {
                txtAddress.Text = mqttConfig.Element("Address") != null ? mqttConfig.Element("Address")?.Value : "";
                txtUserName.Text = mqttConfig.Element("UserName") != null ? mqttConfig.Element("UserName")?.Value : "";
                txtPassword.Text = mqttConfig.Element("Password") != null ? mqttConfig.Element("Password")?.Value : "";
            }

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.client.Disconnect();
            base.OnFormClosing(e);
        }
        private void ShowMessage(string caption, string text)
        {
            MessageBox.Show(
                    owner: this,
                    text,
                    caption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        #region 连接
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                ShowMessage("登录", "请输入opc服务器地址");
                return;
            }
            //if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            //{
            //    ShowMessage("登录", "请输入用户名或密码！");
            //    return;
            //}

            this.client.Disconnect();

            if (Uri.TryCreate(this.txtAddress.Text, UriKind.Absolute, out var serverAddress))
            {
                this.client.ServerAddress = serverAddress;

                if (this.Connect(txtAddress.Text, txtUserName.Text, txtPassword.Text))
                {
                    btnConnect.Enabled = false;
                    btnConnect.ForeColor = Color.White;
                    btnStop.Enabled = true;
                    btnStop.ForeColor = Color.Black;
                    this.Browse();
                }

            }
            else
            {
                this.ShowMessage("连接", "Invalid server address.");
            }
        }

        private bool Connect(string address, string name, string password)
        {
            var result = false;

            try
            {
                client = new OpcClient(address);
                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(password))
                {
                    client.Security.UserIdentity = new OpcClientIdentity(name, password);
                }


                this.client.Connect();
                result = true;
            }
            catch (OpcException ex)
            {
                this.ShowMessage("连接", "Failed to connect: " + ex.Message);
            }

            return result;
        }

        private bool Browse()
        {
            var result = false;

            try
            {
                var node = this.client.BrowseNode(OpcObjectTypes.RootFolder);
                result = this.Browse(node);
            }
            catch (OpcException ex)
            {
                this.ShowMessage("查看", "Failed to browse: " + ex.Message);
            }

            return result;
        }

        private bool Browse(OpcNodeInfo node)
        {
            this.nodesTreeView.Nodes.Clear();
            return this.Browse(node, this.nodesTreeView.Nodes);
        }

        private bool Browse(OpcNodeInfo node, TreeNodeCollection treeNodes)
        {
            var result = false;

            try
            {
                var treeNode = treeNodes.Add(node.DisplayName.Value);

                if (node is OpcObjectNodeInfo)
                {
                    treeNode.ImageIndex = 0;

                    if (node.Reference.TypeDefinitionId == Opc.Ua.ObjectTypeIds.FolderType)
                        treeNode.ImageIndex = 1;
                }
                else if (node is OpcMethodNodeInfo)
                {
                    treeNode.ImageIndex = 2;
                }
                else if (node is OpcVariableNodeInfo)
                {
                    treeNode.ImageIndex = 3;

                    if (node.Reference.ReferenceType == OpcReferenceType.HasProperty)
                        treeNode.ImageIndex = 4;
                }

                treeNode.Tag = node;
                //txtNode.Text = node.NodeId.Value.ToString();
                treeNode.Nodes.Add("Browsing...");
                result = true;
            }
            catch (OpcException ex)
            {
                this.ShowMessage("查看", "Failed to browse: " + ex.Message);
            }

            return result;
        }

        private void NodesTreeViewAfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is OpcNodeInfo node)
            {
                var treeNodes = e.Node.Nodes;
                treeNodes.Clear();

                foreach (var childNode in node.Children())
                {
                    if (!this.Browse(childNode, treeNodes))
                        break;
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            txtNode.Text = "";
            btnConnect.Enabled = true;
            btnConnect.ForeColor = System.Drawing.Color.Black;
            btnStop.Enabled = false;
            btnStop.ForeColor = System.Drawing.Color.White;
            nodesTreeView.Nodes.Clear();
            client.Disconnect();
        }

        #endregion    

        #region 读节点
        private void nodesTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            nodesTreeView.SelectedNode = e.Node; //一定要先指定e.node，否则不能正确运行，下面加入自己的代码
            var nodeInfo = e.Node.Tag as OpcNodeInfo;
            currentNodeId = txtNode.Text = nodeInfo?.NodeId.ToString();

            var opcvalue = client.ReadNode(nodeInfo.NodeId);

            //var nodeclass = nodeInfo.Reference.Category;//节点类型 Object Variable
            //赋值
            _nodeInfo = new NodeInfo()
            {
                NodeName = nodeInfo.DisplayName,
                NodeId = nodeInfo.NodeId.ToString(),
                DataType = opcvalue != null ? opcvalue.DataType.ToString() : "",
                NodeClass = nodeInfo.Reference.Category.ToString(),
                DataModel = "",
                CreateTime = DateTime.Now
            };
        }

        private async void btnRead_Click(object sender, EventArgs e)
        {
            if (client.State == OpcClientState.Created)
            {
                return;
            }

            if (!chkIsLoop.Checked)
            {
                var nodeObj = client.ReadNode(txtNode.Text);
                if (nodeObj.Value != null)
                {
                    Addlog(0, txtNode.Text, JsonConvert.SerializeObject(nodeObj.Value));
                }
            }
            else
            {
                while (IsLoop)
                {
                    var nodeObj = client.ReadNode(txtNode.Text);
                    await Task.Delay(Convert.ToInt32(txtTime.Value));
                    if (nodeObj.Value != null)
                    {
                        Addlog(0, txtNode.Text, JsonConvert.SerializeObject(nodeObj.Value));
                    }
                }
            }
        }
        #endregion


        #region 订阅

        private void btnSubscription_Click(object sender, EventArgs e)
        {
            try
            {
                //判断是否已订阅
                if (listSubscribe.Items.Contains(txtNode.Text))
                {
                    return;
                }
                else
                {
                    GetNodes(client.BrowseNode(txtNode.Text));

                    var token = new CancellationTokenSource();
                    var dataChanges = new BlockingCollection<SubscriptionItem>();

                    var consumerThread = new Thread(ConsumeDataChanges);

                    consumerThread.Start(client);

                    var consumer = new ConsumerSubscriptionInfo()
                    {
                        NodeId = txtNode.Text,
                        TokenSource = token,
                        SubscriptionDataChanges = dataChanges,
                        ConsumerThread = consumerThread
                    };
                    consumerList.Add(consumer);
                    //Log.Information("订阅consumerList=>" + JsonConvert.SerializeObject(consumerList.Select(p => p.NodeId)) + "--nodeList=>" + JsonConvert.SerializeObject(nodeList));
                    //-----------
                    //this.SubscriptionNode(txtNode.Text, Convert.ToInt32(txtTime.Text));

                    //listBox记录订阅
                    listSubscribe.Items.Add(txtNode.Text);
                    //GetNodes(client.BrowseNode(txtNode.Text));
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error", ex.Message);
            }

        }
        //查询当前节点，记录子节点
        private void GetNodes(OpcNodeInfo node)
        {
            string strKey = node.NodeId.ToString(OpcNodeIdFormat.Foundation);
            if (!nodeList.ContainsKey(strKey))
                nodeList.Add(strKey, strKey);

            if (node.Children().Count() > 0 && node.Reference.Category.ToString().Equals(NodeClassType.Object.ToString()))
            {
                nodeList.Add(strKey, strKey);
                foreach (var childNode in node.Children())
                {
                    strKey = childNode.NodeId.ToString(OpcNodeIdFormat.Foundation);
                    if (!nodeList.ContainsKey(strKey))
                    {
                        nodeList.Add(strKey, node.NodeId.ToString(OpcNodeIdFormat.Foundation));
                    }

                }
            }
            //else
            //{
            //    strKey = node.NodeId.ToString(OpcNodeIdFormat.Foundation);
            //    if (!nodeList.ContainsKey(strKey))
            //        nodeList.Add(strKey, strKey);
            //}
        }

        #region test
        private void ConsumeDataChanges(object sender)
        {
            var consumer = consumerList.Where(p => p.NodeId.Equals(currentNodeId)).FirstOrDefault();

            var subscription = client.SubscribeNodes(CreateCommands(client, currentNodeId));
            // Enforce the fastest supported publishing interval.
            subscription.PublishingInterval = Convert.ToInt32(txtTime.Value);
            // Commit recent changes to the subscription.
            subscription.ApplyChanges();

            //记录订阅          
            subscriptionList.Add(new SubscriptionInfo() { NodeId = currentNodeId, Subscription = subscription });

            while (!consumer.TokenSource.IsCancellationRequested)
            {
                try
                {
                    var data = consumer.SubscriptionDataChanges.Take(consumer.TokenSource.Token);

                    //Addlog(0, data.NodeId, JsonConvert.SerializeObject(data.NodeValue));
                    Addlog(0, data.NodeId, data.NodeValue.ToString());

                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }

        }

        private static IEnumerable<OpcSubscribeDataChange> CreateCommands(OpcClient client, OpcNodeId rootNodeId)
        {
            var nodeInfo = client.BrowseNode(rootNodeId);
            //Log.Information("NodeClass=>" + nodeInfo.Reference.Category.ToString());
            if (nodeInfo.Reference.Category.ToString().Equals(NodeClassType.Variable.ToString()))
            {
                yield return new OpcSubscribeDataChange(nodeInfo.NodeId, HandleDataChange);
            }
            else
            {
                if (nodeInfo.Children().Count() > 0)
                {
                    foreach (var childNode in nodeInfo.Children())
                        yield return new OpcSubscribeDataChange(childNode.NodeId, HandleDataChange);
                }
                //else
                //{
                //    yield return new OpcSubscribeDataChange(nodeInfo.NodeId, HandleDataChange);
                //}
            }
        }

        private static void HandleDataChange(object sender, OpcDataChangeReceivedEventArgs e)
        {

            var item = (OpcMonitoredItem)sender;
            //var subItem = new SubscriptionItem() { NodeId = item.NodeId.ValueAsString, NodeValue = e.Item.Value };
            var strNodeId = item.NodeId.ToString(OpcNodeIdFormat.Foundation);

            //Log.Information(JsonConvert.SerializeObject(consumerList.Select(p => p.NodeId)) + "--strNodeId=>" + strNodeId + "--nodeList=>" + nodeList[strNodeId]);
            //Log.Information(JsonConvert.SerializeObject(consumerList.Select(p => p.NodeId)) + "--strNodeId=>" + strNodeId);
            var subItem = new SubscriptionItem() { NodeId = strNodeId, NodeValue = e.Item.Value };

            var consumer = consumerList.Where(p => p.NodeId.Equals(nodeList[strNodeId])).FirstOrDefault();
            consumer.SubscriptionDataChanges.Add(subItem);
        }

        #endregion

        #region 订阅消息  

        //private void SubscriptionNode(string nodeName, int times)
        //{
        //    if (client.State == OpcClientState.Connected)
        //    {
        //        var nodeObj = client.ReadNode(nodeName);
        //        if (nodeObj.Value != null)
        //        {
        //            var subscription = client.SubscribeDataChange(nodeName, HandleDataChanged);
        //            subscription.PublishingInterval = times;
        //            subscription.ApplyChanges();

        //            var info = new SubscriptionInfo() { NodeId = nodeName, Subscription = subscription };
        //            subscriptionList.Add(info);
        //        }
        //    }
        //}
        //private void HandleDataChanged(object sender, OpcDataChangeReceivedEventArgs e)
        //{
        //    OpcMonitoredItem item = (OpcMonitoredItem)sender;
        //    //Console.WriteLine("DataChange: {0} = {1}", item.NodeId, e.Item.Value);        

        //    var nodeObj = e.Item.Value;
        //    if (nodeObj.Value != null)
        //    {
        //        try
        //        {
        //            PrintLog(0, item.NodeId.ToString(), JsonConvert.SerializeObject(nodeObj.Value));
        //        }
        //        catch (Exception ex)
        //        {
        //            PrintLog(1, item.NodeId.ToString(), ex.Message);
        //        }
        //    }
        //}

        #endregion

        private void btnUnSubscription_Click(object sender, EventArgs e)
        {
            if (client.State == OpcClientState.Created)
            {
                return;
            }
            //判断是否已订阅         
            if (listSubscribe.Items.Count == 0)
            {
                ShowMessage("提示", "请先订阅节点");
                return;
            }
            if (listSubscribe.SelectedIndex == -1)
            {
                ShowMessage("提示", "请选择要取消订阅的节点");
                return;
            }
            var nodeId = listSubscribe.SelectedItem.ToString();

            #region 单节点
            //if (client.State == OpcClientState.Connected)
            //{
            //    //var item = subscriptionList.Where(p => p.NodeId.Equals(txtNode.Text)).FirstOrDefault(); //client.SubscribeDataChange(txtNode.Text, HandleDataChanged);
            //    var item = subscriptionList.Where(p => p.NodeId.Equals(nodeId)).FirstOrDefault(); //client.SubscribeDataChange(txtNode.Text, HandleDataChanged);
            //    if (item != null)
            //    {
            //        item.Subscription.Unsubscribe();
            //        subscriptionList.Remove(item);
            //        //listBox移除订阅记录
            //        listSubscribe.Items.Remove(nodeId);
            //    }
            //}
            #endregion

            var item = subscriptionList.Where(p => p.NodeId.Equals(nodeId)).FirstOrDefault(); //client.SubscribeDataChange(txtNode.Text, HandleDataChanged);              
            if (item != null)
            {
                var consumer = consumerList.Where(p => p.NodeId.Equals(nodeId)).FirstOrDefault();
                consumer.TokenSource.Cancel();
                consumer.ConsumerThread.Join();
                consumerList.Remove(consumer);
                //移除订阅
                item.Subscription.Unsubscribe();
                subscriptionList.Remove(item);
                //listBox移除订阅记录
                listSubscribe.Items.Remove(nodeId);
                //移除key
                nodeList.Where(p => p.Value.Equals(nodeId)).Select(p => p.Key).ToList().ForEach(x =>
                {
                    nodeList.Remove(x.ToString());
                });

            }
        }


        #endregion

        #region 订阅日志       
        private void PrintLog(int msgType, string node, string msg)
        {
            switch (msgType)
            {
                default:
                case 0:
                    Addlog(msgType, node, msg);
                    break;
                case 1:
                    Addlog(msgType, node, "错误");
                    break;
                case 2:
                    Addlog(msgType, node, "警告");
                    break;
            }
        }
        private void InitListView(ListView listView, ImageList imageList)
        {
            ColumnHeader logTime = new ColumnHeader() { Name = "logTime", Text = "时间", Width = 240 };
            ColumnHeader logNode = new ColumnHeader() { Name = "logNode", Text = "节点", Width = 300 };
            ColumnHeader logMsg = new ColumnHeader() { Name = "logMsg", Text = "消息", Width = 660 };
            listView.Columns.AddRange(new ColumnHeader[] { logTime, logNode, logMsg });
            listView.View = View.Details;
            listView.SmallImageList = imageList;
        }

        private void Addlog(int imageIndex, string node, string info)
        {
            Addlog(logListView, logImageList, imageIndex, node, info);
        }

        private void Addlog(ListView listView, ImageList imageList, int imageIndex, string node, string info, int maxDisplayItems = 100)
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
                    listItem.SubItems.Add(node);
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
                listItem.SubItems.Add(node);
                listItem.SubItems.Add(info);
                listView.Items.Insert(0, listItem);
            }
        }

        #endregion      

        private void chkIsLoop_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsLoop.Checked)
            {
                IsLoop = true;
            }
            else
            {
                IsLoop = false;
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            logListView.Items.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                XDocument doc = XDocument.Load(path);
                XElement xel = doc.Element("Root").Element("OpcConfig");
                xel.SetElementValue("Address", txtAddress.Text.Trim());
                xel.SetElementValue("UserName", txtUserName.Text.Trim());
                xel.SetElementValue("Password", txtPassword.Text.Trim());
                doc.Save(path);

                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存异常" + ex.Message);
            }
        }


        private void chkPush_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPush.Checked)
            {
                mqtt = true;
            }
            else
            {
                mqtt = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var _db = new AppDbContext();
            _db.NodeInfo.Add(_nodeInfo);
            //db.SaveChanges();
            if (_db.SaveChanges() > 0)
            {
                MessageBox.Show("success");
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{

        //    //var user = new User()
        //    //{
        //    //    Name = "123",
        //    //    Password = "123qwe"
        //    //};
        //    //_userRepository.Add(user);

        //    //MessageBox.Show(_userRepository.GetList(p => p.Name.Equals("123")).FirstOrDefault().Name);

        //    var list = _userRepository.GetList(p => p.Name == "123").FirstOrDefault();
        //    MessageBox.Show(list != null ? list.Name : "-");
        //}
    }
}