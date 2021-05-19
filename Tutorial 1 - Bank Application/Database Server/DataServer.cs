using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Database_Interface;
using Database_Library;

namespace Database_Server
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class DataServer : DataServerInterface
    {
        private readonly Database database;

        public DataServer()
        {
            database = new Database();
        }

        public int GetNumEntries()
        {
            return database.GetNumRecords();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon)
        {
            // Check if index is out-of-range, if it is return an error
            if (index < 0 || index >= database.GetNumRecords())
            {
                Console.WriteLine("Client tried to get a record that was out of range...");

                IndexFault indexFault = new IndexFault();
                indexFault.Issue = "Out of Range";
                throw new FaultException<IndexFault>(indexFault);
            }

            acctNo = database.GetAcctNoByIndex(index);
            pin = database.GetPINByIndex(index);
            bal = database.GetBalanceByIndex(index);
            fName = database.GetFirstNameByIndex(index);
            lName = database.GetLastNameByIndex(index);
            icon = database.GetIconByIndex(index);
        }
    }
}
