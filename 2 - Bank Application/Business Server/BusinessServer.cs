using Database_Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business_Server
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class BusinessServer : BusinessServerInterface
    {
        private const string DataServiceURL = "net.tcp://localhost:8100/DataService";

        private readonly DataServerInterface dataServer;
        private uint logNumber;

        public BusinessServer()
        {
            ChannelFactory<DataServerInterface> channelFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            
            channelFactory = new ChannelFactory<DataServerInterface>(tcp, DataServiceURL);
            dataServer = channelFactory.CreateChannel();

            logNumber = 0;
        }

        public int GetNumEntries()
        {
            Log($"Retrieving Number of Entries ({dataServer.GetNumEntries()})");
            logNumber++;
            return dataServer.GetNumEntries();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon)
        {
            try
            {
                dataServer.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out Bitmap newIcon);
                icon = new Bitmap(newIcon);

                Log($"Retrieving data for index {index}: AcctNo={acctNo}, PIN={pin}, Bal={bal}, FName={fName}, LName={lName}");
                logNumber++;
            }
            catch (FaultException<IndexFault> exception)
            {
                Log($"Encountered an Index Fault");
            logNumber++;
                IndexFault indexFault = new IndexFault();
                indexFault.Issue = exception.Detail.Issue;
                throw new FaultException<IndexFault>(indexFault);
            }
        }
        public void SearchForEntry(string searchTerm, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon)
        {
            //Add a 1 second delay to make it "feel" more realistic
            //Thread.Sleep(1000);

            bool found = false;

            acctNo = 0;
            pin = 0;
            bal = 0;
            fName = "";
            lName = "";
            icon = null;

            Log($"Searching for {searchTerm}");
            for (int i = 0; i < dataServer.GetNumEntries(); i++)
            {
                dataServer.GetValuesForEntry(i, out uint _acctNo, out uint _pin, out int _bal, out string _fName, out string _lName, out Bitmap _icon);
                if (_lName.Equals(searchTerm))
                {
                    found = true;

                    acctNo = _acctNo;
                    pin = _pin;
                    bal = _bal;
                    fName = _fName;
                    lName = _lName;
                    icon = _icon;

                    Log($"Search found at index {i}: AcctNo={acctNo}, PIN={pin}, Bal={bal}, FName={fName}, LName={lName}");
                    logNumber++;
                    break;
                }
            }

            if (found == false)
            {
                Log($"Account not found");
                logNumber++;
                SearchFault searchFault = new SearchFault();
                searchFault.Issue = "Name not Found";
                throw new FaultException<SearchFault>(searchFault);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Log(string logString)
        {
            Console.WriteLine($"{logNumber} {logString}");
        }
    }
}
