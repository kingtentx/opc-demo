using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMqttClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            MqttClientService clientService = new();
            clientService.MqttClientStart();

            Thread.Sleep(3000);
            //clientService.Publish("tstt");
            clientService.Subscribe("demo2");
            Console.ReadLine();
        }


    }
}

