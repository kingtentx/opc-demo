using Newtonsoft.Json;
using TreeCollections;
using System.Text;
using System.Diagnostics;
using Opc.Ua.Client;
using Opc.Ua;
using System.Net;
using OpcUaHelper;
using System.Threading;
using TreeCollections;

namespace OpcClientTest
{
    public class Program
    {
        private static Stopwatch stopwatch1 = new Stopwatch();

        /// <summary>
        /// Opc客户端的核心类
        /// </summary>
        private static OpcUaClient m_OpcUaClient = null;

        /// <summary>
        /// 初始化
        /// </summary>
        private void OpcUaClientInitialization()
        {
            m_OpcUaClient = new OpcUaClient();
            //m_OpcUaClient.OpcStatusChange += M_OpcUaClient_OpcStatusChange1; ;
            //m_OpcUaClient.ConnectComplete += M_OpcUaClient_ConnectComplete;
        }

        public async static Task Main(string[] args)
        {

            //Console.WriteLine("【1】创建ApplicationConfiguration...");
            //var config = UpcUaClient_1.CreateAppConfiguration();
            //config.Validate(ApplicationType.Client).GetAwaiter().GetResult();
            //if (config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            //{
            //    config.CertificateValidator.CertificateValidation += (s, e) => { e.Accept = (e.Error.StatusCode == StatusCodes.BadCertificateUntrusted); };
            //}

            //var application = new Opc.Ua.Configuration.ApplicationInstance
            //{
            //    ApplicationName = "MyHomework",
            //    ApplicationType = ApplicationType.Client,
            //    ApplicationConfiguration = config
            //};
            //application.CheckApplicationInstanceCertificate(false, 2048).GetAwaiter().GetResult();

            //var host = Dns.GetHostName();

            //Console.WriteLine("【2】创建Session...");

            // Task <Session> session =  UpcUaClient_1.ConnectOpcServer("opc.tcp://localhost:4840/", config);

            //var opcinfo = session.Result;

            //var no = opcinfo.ReadValue("ns=3;s=factory_1/line2/Temp");

            //Console.WriteLine(no);


            //Console.WriteLine("【3】创建subscription...");
            //var subscription = CreateSubscription(session.Result, 1000);
            //Console.WriteLine("【4】增加Event监听.");

            //Console.WriteLine("【4】增加监控项目.");
            //AddMonitorItem(subscription);
            //Console.WriteLine("【5】开始订阅.");

            //subscription.Create();
            // UpcUaClient.Write(session.Result, "ns=2;s=MES.DEV01.CURRENT_PIECE", 13, "aabbccdd");


            m_OpcUaClient = new OpcUaClient();
            m_OpcUaClient.UserIdentity = new UserIdentity("user", "123456");
            await m_OpcUaClient.ConnectServer("opc.tcp://localhost:4840/");

            string nodeId = "ns=3;s=factory_1/line2/Temp";

            if (m_OpcUaClient.Connected)
            {
                var obj = ObjectIds.ObjectsFolder;
                PopulateBranch(obj);

                ////读取节点
                //DataValue dataValue = m_OpcUaClient.ReadNode(nodeId);
                //Console.WriteLine("【1】读取" + nodeId + "：" + dataValue);


                ////订阅节点
                //Session session = m_OpcUaClient.Session;

                //Console.WriteLine("【3】创建subscription...");
                //var subscription = CreateSubscription(session, 600);
                //Console.WriteLine("【4】增加Event监听.");

                ////Console.WriteLine("【4】增加监控项目.");
                //AddMonitorItem(subscription);
                //Console.WriteLine("【5】开始订阅.");

                //subscription.Create();


            }



            Console.ReadKey(true);

        }



        /// <summary>
        /// 在会话中新增一个订阅
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private static Subscription CreateSubscription(Session session, int publishingInterval)
        {

            var subscription = new Subscription(session.DefaultSubscription)
            {
                PublishingInterval = publishingInterval,
                PublishingEnabled = true

            };

            subscription.DefaultItem.AttributeId = Attributes.Value;
            subscription.DefaultItem.MonitoringMode = MonitoringMode.Reporting;
            subscription.DefaultItem.SamplingInterval = 1000;
            subscription.DefaultItem.QueueSize = 0;
            subscription.DefaultItem.DiscardOldest = true;
            session.AddSubscription(subscription);

            return subscription;

        }

        private static void AddMonitorItem(Subscription subscription)
        {
            //定义MonitorItems默认值
            var item = new MonitoredItem(subscription.DefaultItem)
            {
                StartNodeId = "ns=3;s=factory_1/line2/Temp",
                DisplayName = "温度"

            };
            item.Notification += monitoredItem_Notification;
            subscription.AddItem(item);

            //subscription.AddItem(CreateMonitorItems("ns=2;s=POLISH.RobotPLC.MEM.HMI在线"));
            //subscription.AddItem(CreateMonitorItems("ns=2;s=POLISH.RobotPLC.EXTY.ExtYGoto", "下个位置"));
            //subscription.AddItem(CreateMonitorItems("ns=2;s=POLISH.RobotPLC.EXTY.ExtYOnPosition", "导轨就位"));
            //subscription.AddItem(CreateMonitorItems("ns=2;s=POLISH.RobotPLC.EXTY.ExtYPosition", "当前位置"));

        }

        /// <summary>
        /// 创建一个监控项目
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        private static MonitoredItem CreateMonitorItems(NodeId nodeId, string displayName)
        {
            var monitoredItem = new MonitoredItem();
            monitoredItem.StartNodeId = nodeId;
            monitoredItem.DisplayName = displayName;
            monitoredItem.AttributeId = Attributes.Value;
            monitoredItem.MonitoringMode = MonitoringMode.Reporting;
            monitoredItem.SamplingInterval = 50;
            monitoredItem.QueueSize = 0;
            monitoredItem.DiscardOldest = true;
            // 定义监控项的事件处理
            monitoredItem.Notification += monitoredItem_Notification;
            return monitoredItem;

        }

        /// <summary>
        /// 监控到数据变化的处理
        /// </summary>
        /// <param name="monitoredItem"></param>
        /// <param name="e"></param>
        static void monitoredItem_Notification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {

            MonitoredItemNotification notification = e.NotificationValue as MonitoredItemNotification;
            if (notification == null)
            {
                return;
            }

            string outStr = monitoredItem.StartNodeId.ToString() + "value: " + Utils.Format("{0}", notification.Value.WrappedValue.ToString()) +
                                       ";  StatusCode: " + Utils.Format("{0}", notification.Value.StatusCode.ToString()) +
                                       ";  Source timestamp: " + notification.Value.SourceTimestamp.ToString() +
                                       ";  Server timestamp: " + notification.Value.ServerTimestamp.ToString();
            Console.WriteLine(outStr);
        }

        private static void OnNotification(MonitoredItem item, MonitoredItemNotificationEventArgs e)
        {
            foreach (var value in item.DequeueValues())
            {
                Console.WriteLine("{0}: {1}, {2}, {3}", item.DisplayName, value.Value, value.SourceTimestamp, value.StatusCode);
            }
        }

        #region test
        private async static void PopulateBranch(NodeId sourceId)
        {

            // fetch references from the server.

            ReferenceDescriptionCollection references = GetReferenceDescriptionCollection(sourceId);
            // List<TreeNode> list = new List<TreeNode>();
            if (references != null)
            {
                // process results.
                for (int ii = 0; ii < references.Count; ii++)
                {
                    ReferenceDescription target = references[ii];

                    Console.WriteLine(ii + "-------------" + target);
                    //TreeNode child = new TreeNode(Utils.Format("{0}", target));

                    // child.Tag = target;
                    //string key = GetImageKeyFromDescription(target, sourceId);
                    // child.ImageKey = key;
                    // child.SelectedImageKey = key;

                    // if (target.NodeClass == NodeClass.Object || target.NodeClass == NodeClass.Unspecified || expanded)
                    // {
                    //     child.Nodes.Add(new TreeNode());
                    // }




                    // list.Add(child);
                }
            }

        }

        private static ReferenceDescriptionCollection GetReferenceDescriptionCollection(NodeId sourceId)
        {
            TaskCompletionSource<ReferenceDescriptionCollection> task = new TaskCompletionSource<ReferenceDescriptionCollection>();

            // find all of the components of the node.
            BrowseDescription nodeToBrowse1 = new BrowseDescription();

            nodeToBrowse1.NodeId = sourceId;
            nodeToBrowse1.BrowseDirection = BrowseDirection.Forward;
            nodeToBrowse1.ReferenceTypeId = ReferenceTypeIds.Aggregates;
            nodeToBrowse1.IncludeSubtypes = true;
            nodeToBrowse1.NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable | NodeClass.Method | NodeClass.ReferenceType | NodeClass.ObjectType | NodeClass.View | NodeClass.VariableType | NodeClass.DataType);
            nodeToBrowse1.ResultMask = (uint)BrowseResultMask.All;

            // find all nodes organized by the node.
            BrowseDescription nodeToBrowse2 = new BrowseDescription();

            nodeToBrowse2.NodeId = sourceId;
            nodeToBrowse2.BrowseDirection = BrowseDirection.Forward;
            nodeToBrowse2.ReferenceTypeId = ReferenceTypeIds.Organizes;
            nodeToBrowse2.IncludeSubtypes = true;
            nodeToBrowse2.NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable | NodeClass.Method | NodeClass.View | NodeClass.ReferenceType | NodeClass.ObjectType | NodeClass.VariableType | NodeClass.DataType);
            nodeToBrowse2.ResultMask = (uint)BrowseResultMask.All;

            BrowseDescriptionCollection nodesToBrowse = new BrowseDescriptionCollection();
            nodesToBrowse.Add(nodeToBrowse1);
            nodesToBrowse.Add(nodeToBrowse2);

            // fetch references from the server.
            ReferenceDescriptionCollection references = FormUtils.Browse(m_OpcUaClient.Session, nodesToBrowse, false);
            return references;
        }
        #endregion

    }
}

