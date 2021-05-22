using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainLibrary
{
    public class HashGenerator
    {
        public void GenerateHash(string hashInput, out uint hashOffset, out string hash)
        {
            hashOffset = 0;
            while (true)
            {
                //Increment the hash offset to the next multiple of 5
                hashOffset = hashOffset + 5;

                //Generate the hash - Convert to UInt64 and then to string
                SHA256 hashAlgorithm = SHA256.Create();
                byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes($"{hashInput}{hashOffset}"));
                ulong longHash = BitConverter.ToUInt64(data, 0);
                hash = longHash.ToString();

                //If hash is valid, break while loop
                if (hash.StartsWith("12345")){
                    break;
                }
            }
        }

        public bool ValidateHash(Block block)
        {
            string hashInput = $"{block.BlockID}{block.SenderID}{block.ReceiverID}{block.Amount}{block.PreviousHash}{block.Offset}";

            SHA256 hashAlgorithm = SHA256.Create();
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(hashInput));
            ulong longHash = BitConverter.ToUInt64(data, 0);
            string hash = longHash.ToString();

            if (block.Hash.Equals(hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
