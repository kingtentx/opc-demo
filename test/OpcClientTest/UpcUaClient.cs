using Opc.Ua;
using Opc.Ua.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpcClientTest
{
    class UpcUaClient
    {

        /// <summary>
        /// 创建应用配置（ApplicationConfiguration）
        /// </summary>
        /// <returns></returns>
        public static ApplicationConfiguration CreateAppConfiguration()
        {
            return new Opc.Ua.ApplicationConfiguration()
            {


                ApplicationName = "MyHomework",
                ApplicationUri = Utils.Format(@"urn:{0}:MyHomework", System.Net.Dns.GetHostName()),
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration
                {
                    ApplicationCertificate = new CertificateIdentifier
                    {
                        StoreType = @"Directory",
                        StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\MachineDefault",
                        SubjectName = Utils.Format(@"CN={0}, DC={1}", "MyHomework", System.Net.Dns.GetHostName())
                    },
                    TrustedIssuerCertificates = new CertificateTrustList
                    {
                        StoreType = @"Directory",
                        StorePath =
                            @"%CommonApplicationData%\OPC Foundation\CertificateStores\UA Certificate Authorities"
                    },
                    TrustedPeerCertificates = new CertificateTrustList
                    {
                        StoreType = @"Directory",
                        StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\UA Applications"
                    },
                    RejectedCertificateStore = new CertificateTrustList
                    {
                        StoreType = @"Directory",
                        StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\RejectedCertificates"
                    },
                    AutoAcceptUntrustedCertificates = true,
                    AddAppCertToTrustedStore = true
                },
                TransportConfigurations = new TransportConfigurationCollection(),
                TransportQuotas = new TransportQuotas { OperationTimeout = 15000 },
                ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 },
                TraceConfiguration = new TraceConfiguration()
            };
        }

        #region 写操作



        /// <summary>
        /// 写操作
        /// </summary>
        /// <param name="m_session"></param>
        /// <param name="m_nodeId">节点</param>
        /// <param name="m_attributeId">属性</param>
        /// <param name="value">值</param>
        public static void Write(Session m_session, NodeId m_nodeId, uint m_attributeId, object value)
        {
            try
            {
                // 写的值
                Opc.Ua.WriteValue valueToWrite = new WriteValue();

                valueToWrite.NodeId = m_nodeId;
                // valueToWrite.AttributeId = m_attributeId;
                valueToWrite.AttributeId = Attributes.Value;
                valueToWrite.Value.Value = value;
                valueToWrite.Value.StatusCode = StatusCodes.Good;
                valueToWrite.Value.ServerTimestamp = DateTime.MinValue;
                valueToWrite.Value.SourceTimestamp = DateTime.MinValue;
                // 将需要写的值放入集合中
                WriteValueCollection valuesToWrite = new WriteValueCollection();
                valuesToWrite.Add(valueToWrite);

                // 写操作
                StatusCodeCollection results = null;
                DiagnosticInfoCollection diagnosticInfos = null;

                m_session.Write(null, valuesToWrite, out results, out diagnosticInfos);
                //验证结果
                ClientBase.ValidateResponse(results, valuesToWrite);
                ClientBase.ValidateDiagnosticInfos(diagnosticInfos, valuesToWrite);

                if (StatusCode.IsBad(results[0]))
                {
                    throw new ServiceResultException(results[0]);
                }


            }
            catch (Exception exception)
            {
                //  ClientUtils.HandleException("Error Writing Value", exception);
            }
        }
        #endregion


        public static async Task<Session> ConnectOpcServer(string url, ApplicationConfiguration config)
        {
            bool useSecurity = false;//不使用安全SSL
            int operationTimeout = 15000;  //操作超时
            UInt32 sessionTimeout = 60000;  //Session超时
            bool updateBeforeConnect = false;
            var selectedEndpoint = CoreClientUtils.SelectEndpoint(url, useSecurity: useSecurity);

            ConfiguredEndpoint configuredEndpoint = new ConfiguredEndpoint(null, selectedEndpoint, EndpointConfiguration.Create(config));
            Session s = await Session.Create(config, configuredEndpoint, updateBeforeConnect, "", sessionTimeout, null, null);
            return s;

        }

        public void Brows()
        {
            /*
            ReferenceDescriptionCollection refs;
            Byte[] cp;
            session.Browse(null, null, ObjectIds.ObjectsFolder, 0u, BrowseDirection.Forward,
                ReferenceTypeIds.HierarchicalReferences, true,
                (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method, out cp, out refs);
            Console.WriteLine("DisplayName: BrowseName, NodeClass");
            foreach (var rd in refs)
            {
                Console.WriteLine("{0}: {1}, {2}", rd.DisplayName, rd.BrowseName, rd.NodeClass);
                ReferenceDescriptionCollection nextRefs;
                byte[] nextCp;
                session.Browse(null, null, ExpandedNodeId.ToNodeId(rd.NodeId, session.NamespaceUris), 0u,
                    BrowseDirection.Forward, ReferenceTypeIds.HierarchicalReferences, true,
                    (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method, out nextCp,
                    out nextRefs);
                foreach (var nextRd in nextRefs)
                {
                    Console.WriteLine("+ {0}: {1}, {2}", nextRd.DisplayName, nextRd.BrowseName, nextRd.NodeClass);
                }

            }*/
        }

        public void Add(Session session)
        {

            //（读取）读取一个节点的值
            var da = session.ReadValue(new NodeId("ns=2;s=POLISH.RobotPLC.EXTY.ExtYGoto"));




        }
    }
}
