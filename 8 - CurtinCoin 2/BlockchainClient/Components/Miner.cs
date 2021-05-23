using BlockchainLibrary;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;

namespace BlockchainClient.Components
{
    public class Miner
    {
        public void MiningThread()
        {
            Console.WriteLine($"[{Blockchain.MyClientName}Miner] Mining Thread running");

            const string ServerURL = "http://localhost:58351/";
            RestClient restClient = new RestClient(ServerURL);
            while (true)
            {
                //Check if there are any transactions
                if(Transactions.GetTransactionCount() > 0)
                {
                    Console.WriteLine($"[{Blockchain.MyClientName}Miner] Processing transaction");
                    Transaction transaction = Transactions.GetCurrentTransaction();

                    try
                    {
                        //Validate that the sender has enough coins in their account (With the sole exception being ID 0, the bank)
                        if ((transaction.SenderID != 0) && (Blockchain.GetUserBalance(transaction.SenderID) < transaction.Amount))
                            throw new Exception("Sender does not have enough coins");

                        //Create and Insert transaction details into a block
                        Block block = new Block();
                        block.Amount = transaction.Amount;
                        block.SenderID = transaction.SenderID;
                        block.ReceiverID = transaction.ReceiverID;

                        //Insert previous block hash into the block
                        Block lastBlock = Blockchain.GetCurrentBlock();
                        block.BlockID = lastBlock.BlockID + 1;
                        block.PreviousHash = lastBlock.Hash;

                        //Create a new hash
                        HashGenerator hashGenenerator = new HashGenerator();
                        string hashInput = $"{block.BlockID}{block.SenderID}{block.ReceiverID}{block.Amount}{block.PreviousHash}";
                        hashGenenerator.GenerateHash(hashInput, out block.Offset, out block.Hash);

                        //Submit block for inclusion into blockchain
                        Blockchain.AddBlock(block);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"[{Blockchain.MyClientName}Miner] Error processing transaction: {e.Message}");
                    }

                }

                //Time to determine who has the most popular blockchain
                List<string> hashes = new List<string>();

                //Retrieve clients
                var clientListRequest = new RestRequest("api/Client");
                var clientListResponse = restClient.Get(clientListRequest);
                List<ClientStruct> clients = JsonConvert.DeserializeObject<List<ClientStruct>>(clientListResponse.Content);

                if (clients != null)
                {
                    if (clients.Count != 0)
                    {
                        //Iterate through each client
                        foreach (ClientStruct client in clients)
                        {
                            ChannelFactory<ClientServerInterface> channelFactory;
                            NetTcpBinding tcp = new NetTcpBinding();

                            channelFactory = new ChannelFactory<ClientServerInterface>(tcp, $"net.tcp://localhost:{client.Port}/ClientServer");
                            ClientServerInterface clientServer = channelFactory.CreateChannel();

                            Block block = clientServer.GetCurrentBlock();
                            hashes.Add(block.Hash);
                        }
                    }
                }

                //To determine which hash is the most popular, we will need at least 3 blockchains to work with.
                if (hashes.Count >= 3)
                {
                    Console.WriteLine($"[{Blockchain.MyClientName}Miner] Calculating most popular blockchain");
                    //https://stackoverflow.com/questions/454601/how-to-count-duplicates-in-list-with-linq
                    var sortedHashes = hashes.GroupBy(hash => hash)
                                             .Select(hash => new 
                                             { 
                                                 Count = hash.Count(), 
                                                 Hash = hash.Key 
                                             })
                                             .OrderByDescending(hash => hash.Count);

                    //If the most popular hash is not unique
                    if (sortedHashes.First().Count > 1)
                    {
                        string mostPopularHash = sortedHashes.First().Hash;
                        string currentHash = Blockchain.GetCurrentBlock().Hash;

                        Console.WriteLine($"[{Blockchain.MyClientName}Miner] Most popular hash is {mostPopularHash}, current hash is {currentHash}");
                        //If the most popular hash does not match the current hash
                        if (mostPopularHash.Equals(currentHash) == false)
                        {
                            Console.WriteLine($"[{Blockchain.MyClientName}Miner] Replacing current blockchain");
                            List<Block> popularBlockchain = null;

                            //Find the most popular blockchain that has the hash
                            foreach (ClientStruct client in clients)
                            {
                                ChannelFactory<ClientServerInterface> channelFactory;
                                NetTcpBinding tcp = new NetTcpBinding();

                                channelFactory = new ChannelFactory<ClientServerInterface>(tcp, $"net.tcp://localhost:{client.Port}/ClientServer");
                                ClientServerInterface clientServer = channelFactory.CreateChannel();

                                Block block = clientServer.GetCurrentBlock();
                                if (mostPopularHash.Equals(block.Hash))
                                {
                                    popularBlockchain = clientServer.GetCurrentBlockchain();
                                    break;
                                }
                            }

                            Blockchain.ReplaceCurrentBlockchain(popularBlockchain);
                        }
                    }
                }

                //Add a 3 second delay between each loop
                Thread.Sleep(3000);
            }
        }
    }
}
