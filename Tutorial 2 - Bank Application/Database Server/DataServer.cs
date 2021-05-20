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
        private Database _database;

        public DataServer()
        {
            _database = new Database();
        }

        public int GetNumEntries()
        {
            return _database.GetNumRecords();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon)
        {
            Console.WriteLine("Retrieving Data for " + index);
            // Check if index is out-of-range, if it is return an error
            if (index < 0 || index >= _database.GetNumRecords())
            {
                IndexFault indexFault = new IndexFault();
                indexFault.Issue = "Out of Range";
                throw new FaultException<IndexFault>(indexFault);
            }

            acctNo = _database.GetAcctNoByIndex(index);
            pin = _database.GetPINByIndex(index);
            bal = _database.GetBalanceByIndex(index);
            fName = _database.GetFirstNameByIndex(index);
            lName = _database.GetLastNameByIndex(index);
            icon = new Bitmap(_database.GetIconByIndex(index));
        }
    }
}
