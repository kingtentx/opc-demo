using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCForm.Mqtt.Model
{
    public class MqttConfig
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsTls { get; set; } = false;
    }
}
