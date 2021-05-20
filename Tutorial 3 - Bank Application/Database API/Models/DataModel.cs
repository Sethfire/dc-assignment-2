using Database_Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Database_API.Models
{
    public static class DataModel
    {
        private const string DataServiceURL = "net.tcp://localhost:8100/DataService";
        private static DataServerInterface dataServer;

        static DataModel()
        {
            ChannelFactory<DataServerInterface> channelFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            channelFactory = new ChannelFactory<DataServerInterface>(tcp, DataServiceURL);
            dataServer = channelFactory.CreateChannel();
        }

        public static int GetNumEntries()
        {
            return dataServer.GetNumEntries();
        }

        public static void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName)
        {
            try
            {
                dataServer.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out Bitmap icon);
            }
            catch (FaultException<IndexFault> exception)
            {
                IndexFault indexFault = new IndexFault();
                indexFault.Issue = exception.Detail.Issue;
                throw new FaultException<IndexFault>(indexFault);
            }
        }

        public static void SearchForEntry(string searchTerm, out uint acctNo, out uint pin, out int bal, out string fName, out string lName)
        {
            //Add a 1 second delay to make it "feel" more realistic
            //Thread.Sleep(1000);

            bool found = false;

            acctNo = 0;
            pin = 0;
            bal = 0;
            fName = "";
            lName = "";
            //icon = null;

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
                    //icon = _icon;

                    break;
                }
            }

            if (found == false)
            {
                SearchFault searchFault = new SearchFault();
                searchFault.Issue = "Name not Found";
                throw new FaultException<SearchFault>(searchFault);
            }
        }
    }
}