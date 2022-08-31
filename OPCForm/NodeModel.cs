using Opc.UaFx;
using Opc.UaFx.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCForm
{

    public class SubscriptionItem
    {
        public string NodeId { get; set; }

        public OpcValue NodeValue { get; set; }

    }

    public class SubscriptionInfo
    {
        public string NodeId { get; set; }

        public OpcSubscription Subscription { get; set; }
    }

    public class ConsumerSubscriptionInfo
    {
        public string NodeId { get; set; }

        public CancellationTokenSource TokenSource = new CancellationTokenSource();

        public BlockingCollection<SubscriptionItem> SubscriptionDataChanges = new BlockingCollection<SubscriptionItem>();

        public Thread ConsumerThread { get; set; }
      
    }

 
}
