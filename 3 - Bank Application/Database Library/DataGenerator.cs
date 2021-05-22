using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Library
{
    internal class DataGenerator
    {
        private readonly Random random = new Random(DateTime.Now.Second);

        private readonly string[] _firstNames = new string[20] {
            "James", "John", "Robert", "Michael", "William",
            "David", "Richard", "Joseph", "Thomas", "Charles",
            "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth",
            "Barbara", "Susan", "Jessica", "Sarah", "Karen"
        };

        private readonly string[] _lastNames = new string[20] {
            "Smith", "Johnson", "Williams", "Brown", "Jones",
            "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson",
            "Thomas", "Taylor", "Moore", "Jackson", "Martin"
        };

        private readonly List<Bitmap> _icons;

        //Taken from Provided Tutorial 1 Solution
        public DataGenerator()
        {
            _icons = new List<Bitmap>();
            for (var i = 0; i < 10; i++)
            {
                var image = new Bitmap(64, 64);
                for (var x = 0; x < 64; x++)
                {
                    for (var y = 0; y < 64; y++)
                    {
                        image.SetPixel(x, y, Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
                    }
                }
                _icons.Add(image);
            }
        }

        private string GetFirstname()
        {
            return _firstNames[random.Next(0, _firstNames.Length - 1)];
        }

        private string GetLastname()
        {
            return _lastNames[random.Next(0, _lastNames.Length - 1)];
        }

        private uint GetPIN()
        {
            return Convert.ToUInt32(random.Next(0, 9999));
        }

        private uint GetAcctNo()
        {
            return Convert.ToUInt32(random.Next(100000000, 999999999));
        }

        private int GetBalance()
        {
            return random.Next(-1000, 1000000);
        }

        private Bitmap GetIcon()
        {
            return _icons[random.Next(0, _icons.Count - 1)];
        }
        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance, out Bitmap icon)
        {
            firstName = GetFirstname();
            lastName = GetLastname();
            pin = GetPIN();
            acctNo = GetAcctNo();
            balance = GetBalance();
            icon = GetIcon();
        }
    }
}
