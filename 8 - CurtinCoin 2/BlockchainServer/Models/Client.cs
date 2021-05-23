using BlockchainLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockchainServer.Models
{
    public static class Client
    {
        private static List<ClientStruct> clients = new List<ClientStruct>();

        public static List<ClientStruct> GetClients()
        {
            return clients;
        }

        public static void AddClient(ClientStruct client)
        {
            clients.Add(client);
        }
    }
}