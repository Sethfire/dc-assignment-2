using Database_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Business_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host;
            NetTcpBinding tcp = new NetTcpBinding();

            host = new ServiceHost(typeof(BusinessServer));
            host.AddServiceEndpoint(typeof(BusinessServerInterface), tcp, "net.tcp://0.0.0.0:8200/BusinessService");
            host.Open();

            Console.WriteLine("Business Server Online");
            Console.ReadLine();
            host.Close();
        }
    }
}
