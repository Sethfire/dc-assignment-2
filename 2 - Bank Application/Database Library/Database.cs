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

        private List<DataStruct> database;

        public Database()
        {
            database = new List<DataStruct>();
            DataGenerator dataGenerator = new DataGenerator();

            for (int i = 0; i < NUMBER_OF_ACCOUNTS; i++)
            {
                DataStruct data = new DataStruct();
                dataGenerator.GetNextAccount(out data.pin, out data.acctNo, out data.firstName, out data.lastName, out data.balance, out data.icon);
                database.Add(data);
            }
        }

        public uint GetAcctNoByIndex(int index)
        {
            return database[index].acctNo;
        }

        public uint GetPINByIndex(int index)
        {
            return database[index].pin;
        }

        public string GetFirstNameByIndex(int index)
        {
            return database[index].firstName;
        }

        public string GetLastNameByIndex(int index)
        {
            return database[index].lastName;
        }

        public int GetBalanceByIndex(int index)
        {
            return database[index].balance;
        }

        public Bitmap GetIconByIndex(int index)
        {
            return database[index].icon;
        }

        public int GetNumRecords()
        {
            return database.Count;
        }
    }
}
