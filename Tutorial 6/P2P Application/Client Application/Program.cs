using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client_Application
{
    public delegate int BinaryOperation(int operand1, int operand2);

    class Program
    {
        static void Main(string[] args)
        {
            /*
            BinaryOperation addDelegate;
            IAsyncResult asyncObj1, asyncObj2;

            addDelegate = Add;
            asyncObj1 = addDelegate.BeginInvoke(2, 2, null, null);
            asyncObj2 = addDelegate.BeginInvoke(5, 5, null, null);

            asyncObj1.AsyncWaitHandle.Close();
            asyncObj2.AsyncWaitHandle.Close();
            */

            NetTcpBinding tcp = new NetTcpBinding();
            ServiceHost host = new ServiceHost(typeof(DataServer));
            host.AddServiceEndpoint(typeof(DataServerInterface), tcp, "net.tcp://0.0.0.0:8100/DataService");
            host.open();

            Console.WriteLine("Client Application Running");
            Console.ReadLine();
            host.close();
        }

        private uint Add(uint a, uint b)
        {
            return a + b;
        }
    }
}
