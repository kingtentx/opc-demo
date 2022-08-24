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

namespace OPCForm
{
    public partial class Form1 : Form
    {
        private OpcClient client;
        private OpcSubscription subscription;
        //private OpcNodeInfo nodeInfo;
        private NodeModel nodeModel = new NodeModel();
        public Form1()
        {
            this.client = new OpcClient();
            InitializeComponent();
            InitListView(logListView, logImageList);
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
                ShowMessage("login", "请输入opc服务器地址");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowMessage("login", "请输入用户名或密码！");
                return;
            }

            this.client.Disconnect();

            if (Uri.TryCreate(this.txtAddress.Text, UriKind.Absolute, out var serverAddress))
            {
                this.client.ServerAddress = serverAddress;

                if (this.Connect(txtAddress.Text, txtUserName.Text, txtPassword.Text))
                {
                    btnConnect.Enabled = false;
                    btnConnect.ForeColor = System.Drawing.Color.White;
                    btnStop.Enabled = true;
                    btnStop.ForeColor = System.Drawing.Color.Black;
                    this.Browse();
                }

            }
            else
            {
                this.ShowMessage("Connect", "Invalid server address.");
            }
        }

        private bool Connect(string address, string name, string password)
        {
            var result = false;

            try
            {
                client = new OpcClient(address);
                client.Security.UserIdentity = new OpcClientIdentity(name, password);
                this.client.Connect();
                result = true;
            }
            catch (OpcException ex)
            {
                this.ShowMessage("Connect", "Failed to connect: " + ex.Message);
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
                this.ShowMessage("Browse", "Failed to browse: " + ex.Message);
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
                this.ShowMessage("Browse", "Failed to browse: " + ex.Message);
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
            txtNode.Text = nodeInfo?.NodeId.ToString();
            nodeModel.NodeId = nodeInfo?.NodeId.ToString();
            //nodeModel.NodeValue = JsonConvert.SerializeObject(nodeInfo?.NodeId.Value);
            //nodeModel.DataType = nodeInfo?.NodeId.Type.ToString();
        }


        private void btnRead_Click(object sender, EventArgs e)
        {
            var nodeObj = client.ReadNode(txtNode.Text);
            if (nodeObj.Value != null)
            {
                Addlog(0, txtNode.Text, JsonConvert.SerializeObject(nodeObj.Value));
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
                    this.SubscriptionNode(txtNode.Text, Convert.ToInt32(txtTime.Text));
                    //listBox记录订阅
                    listSubscribe.Items.Add(txtNode.Text);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error", ex.Message);
            }

        }

        //订阅消息
        private void SubscriptionNode(string nodeName, int times)
        {
            if (client.State == OpcClientState.Connected)
            {
                var nodeObj = client.ReadNode(nodeName);

                if (nodeObj.Value != null)
                {
                    subscription = client.SubscribeDataChange(nodeName, HandleDataChanged);
                    subscription.PublishingInterval = times;
                    subscription.ApplyChanges();
                }
            }
        }
        private void HandleDataChanged(object sender, OpcDataChangeReceivedEventArgs e)
        {
            OpcMonitoredItem item = (OpcMonitoredItem)sender;         
            //Console.WriteLine("DataChange: {0} = {1}", item.NodeId, e.Item.Value);        

            var nodeObj = e.Item.Value;
            if (nodeObj.Value != null)
            {
                try
                {
                    PrintLog(0, item.NodeId.ToString(), JsonConvert.SerializeObject(nodeObj.Value));
                }
                catch (Exception ex)
                {
                    PrintLog(1, item.NodeId.ToString(), ex.Message);
                }
            }
        }

        private void btnUnSubscription_Click(object sender, EventArgs e)
        {
            //判断是否已订阅
            if (!listSubscribe.Items.Contains(txtNode.Text))
            {
                return;
            }

            if (client.State == OpcClientState.Connected)
            {
                // var subscription = client.SubscribeDataChange(txtNode.Text, HandleDataChanged);
                if (subscription != null)
                {
                    subscription.Unsubscribe();
                    //listBox移除订阅记录
                    listSubscribe.Items.Remove(txtNode.Text);
                }

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
            ColumnHeader logTime = new ColumnHeader() { Name = "logTime", Text = "时间", Width = 200 };
            ColumnHeader logNode = new ColumnHeader() { Name = "logNode", Text = "节点", Width = 220 };
            ColumnHeader logMsg = new ColumnHeader() { Name = "logMsg", Text = "消息", Width = 400 };
            listView.Columns.AddRange(new ColumnHeader[] { logTime, logNode, logMsg });
            listView.View = View.Details;
            listView.SmallImageList = imageList;
        }

        private void Addlog(int imageIndex, string node, string info)
        {
            Addlog(logListView, logImageList, imageIndex, node, info, 20);
        }

        private void Addlog(ListView listView, ImageList imageList, int imageIndex, string node, string info, int maxDisplayItems)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new Action(() =>
                {
                    if (listView.Items.Count > maxDisplayItems)
                    {
                        listView.Items.RemoveAt(maxDisplayItems);
                    }

                    ListViewItem listItem = new ListViewItem(" " + DateTime.Now.ToString(), imageIndex);
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

                ListViewItem listItem = new ListViewItem(" " + DateTime.Now.ToString(), imageIndex);
                listItem.SubItems.Add(node);
                listItem.SubItems.Add(info);
                listView.Items.Insert(0, listItem);
            }
        }

        #endregion


        #region ListBox

        private void listSubscribe_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listSubscribe.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                // MessageBox.Show(this.listSubscribe.SelectedItem.ToString()); //执行双击事件
                //this.SubscriptionNode(this.listSubscribe.SelectedItem.ToString(), Convert.ToInt32(txtTime.Text));
                listSubscribe.SelectedIndex = -1;//不做任何操作，将ListBox的选中项取消
            }
            else
            {
                listSubscribe.SelectedIndex = -1;//不做任何操作，将ListBox的选中项取消
            }
        }

        #endregion
    }
}