using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Library
{
    public class Database
    {
        readonly uint NUMBER_OF_ACCOUNTS = 100000;

        private List<DataStruct> _database;

        public Database()
        {
            _database = new List<DataStruct>();
            DataGenerator dataGenerator = new DataGenerator();

            for (int i = 0; i < NUMBER_OF_ACCOUNTS; i++)
            {
                DataStruct data = new DataStruct();
                dataGenerator.GetNextAccount(out data.pin, out data.acctNo, out data.firstName, out data.lastName, out data.balance, out data.icon);
                _database.Add(data);
            }
        }

        public uint GetAcctNoByIndex(int index)
        {
            return _database[index].acctNo;
        }

        public uint GetPINByIndex(int index)
        {
            return _database[index].pin;
        }

        public string GetFirstNameByIndex(int index)
        {
            return _database[index].firstName;
        }

        public string GetLastNameByIndex(int index)
        {
            return _database[index].lastName;
        }

        public int GetBalanceByIndex(int index)
        {
            return _database[index].balance;
        }

        public Bitmap GetIconByIndex(int index)
        {
            return _database[index].icon;
        }

        public int GetNumRecords()
        {
            return _database.Count;
        }
    }
}
