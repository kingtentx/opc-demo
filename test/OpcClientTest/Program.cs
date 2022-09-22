using Opc.UaFx;
using Opc.UaFx.Client;
using Newtonsoft.Json;
using TreeCollections;
using System.Text;
using System.Diagnostics;

namespace OpcClientTest
{
    public class Program
    {
        private static Stopwatch stopwatch1 = new Stopwatch();

        public static void Main(string[] args)
        {

            //using (var client = new OpcClient("opc.tcp://192.168.0.1:4840/"))
            //{
            //    client.Connect();
            //    var node = client.BrowseNode(new OpcNodeId("\"communication data\".\"int array to send\"", 3));

            //    List<OpcNodeId> nodeList = new List<OpcNodeId>();
            //    OpcNodeId[] nodes;
            //    Browse(node, nodeList);
            //    Console.WriteLine("done loading");
            //    nodes = nodeList.ToArray();
            //    stopwatch1.Start();

            //    SampleaAndMessureWithSubscription(client, nodes);

            //    Console.Read();
            //};



            Console.WriteLine("Hello, World!");

            var client = new OpcClient("opc.tcp://127.0.0.1:4840");
            client.Security.UserIdentity = new OpcClientIdentity("user", "123456");
            //client.Security.EndpointPolicy = new OpcSecurityPolicy(OpcSecurityMode.SignAndEncrypt, OpcSecurityAlgorithm.Basic256);
            client.Connect();


            var value = client.ReadNode("ns=3;s=factory_1/line1/Temp").Value;
            double[] tem = (double[])value;
            Console.WriteLine(string.Join(",", tem));

            //读数据
            //var node = client.BrowseNode(OpcObjectTypes.ObjectsFolder);
            //Program.Browse(node);
            //client.Disconnect();
            //Console.ReadKey(true);

            //写数据          
            OpcWriteNode[] wCommands = new OpcWriteNode[] {
                new OpcWriteNode("ns=3;s=factory_1/line2/Temp", (double)18)
            };
            OpcStatusCollection results = client.WriteNodes(wCommands);
            if (results[0].IsBad)
            {
                Console.WriteLine(results[0].Description);
            }

            // 订阅
            OpcSubscription subscription = client.SubscribeDataChange("ns=3;s=factory_1/line2/Temp", HandleDataChanged);
            subscription.PublishingInterval = 1000;
            subscription.ApplyChanges();

            Thread.Sleep(60000);

            // 取消订阅
            if (client.State == OpcClientState.Connected)
            {
                if (subscription != null)
                {
                    subscription.Unsubscribe();
                    Console.WriteLine("取消订阅");
                }

            }
            //client.Disconnect();

            Console.ReadKey(true);
        }

        private static void HandleDataChanged(object sender, OpcDataChangeReceivedEventArgs e)
        {
            OpcMonitoredItem item = (OpcMonitoredItem)sender;
            Console.WriteLine("DataChange: {0} = {1}", item.NodeId, e.Item.Value);
        }

        #region ---------- Private static methods ----------

        private static void Browse(OpcNodeInfo node)
        {
            Program.Browse(node, 0);
        }

        private static void Browse(OpcNodeInfo node, int level)
        {
            //// In general attributes and children are retrieved from the server on demand. This
            //// is done to reduce the amount of traffic and to improve the preformance when
            //// searching/browsing for specific attributes or children. After attributes or
            //// children of a node were browsed they are stored internally so that subsequent
            //// attribute and children requests are processed without any interaction with the
            //// OPC UA server.

            // Browse the DisplayName attribute of the node. It is also possible to browse
            // multiple attributes at once (see the method Attributes(...)).
            var displayName = node.Attribute(OpcAttribute.DisplayName);

            Console.WriteLine(
                    "{0}{1} ({2})",
                    new string(' ', level * 4),
                    node.NodeId.ToString(OpcNodeIdFormat.Foundation),
                    displayName.Value);

            // Browse the children of the node and continue browsing in preorder.
            foreach (var childNode in node.Children())
                Program.Browse(childNode, level + 1);
        }

        #endregion

        private static void SampleaAndMessureWithSubscription(OpcClient client, OpcNodeId[] nodes)
        {
            OpcSubscription subscription = client.SubscribeNodes();
            for (int i = 0; i < nodes.Length; i++)
            {
                var item = new OpcMonitoredItem(nodes[i], OpcAttribute.Value);
                item.DataChangeReceived += HandleDataChanged;
                item.Tag = i;
                item.SamplingInterval = 20;

                subscription.AddMonitoredItem(item);
            }
            subscription.PublishingInterval = 20;
            subscription.ApplyChanges();
        }
    }
}

