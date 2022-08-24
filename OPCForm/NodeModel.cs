using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCForm
{
    public class NodeModel
    {
        public string? NodeId { get; set; }

        public string? NodeValue { get; set; }

        public string DataType { get; set; }

        public bool IsSubscription { get; set; } = false;

        public int Interval { get; set; } = 1000;
    }
}
