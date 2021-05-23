using API_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServer.Models
{
    public static class Clients
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