using BlockchainLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Block block = new Block();

            block.BlockID = 4;
            block.SenderID = 0001;
            block.ReceiverID = 0002;
            block.Amount = 400;
            block.PreviousHash = "12345";

            string hashInput = $"{block.BlockID}{block.SenderID}{block.ReceiverID}{block.Amount}{block.PreviousHash}";
            HashGenerator hashGenenerator = new HashGenerator();
            hashGenenerator.GenerateHash(hashInput, out block.Offset, out block.Hash);

            Console.WriteLine($"Hash Input: {hashInput}");
            Console.WriteLine($"Hash Offset: {block.Offset}");
            Console.WriteLine($"Hash: {block.Hash}");
            Console.WriteLine($"Hash Validation: {hashGenenerator.ValidateHash(block)}");
            Console.ReadLine();
        }
    }
}
