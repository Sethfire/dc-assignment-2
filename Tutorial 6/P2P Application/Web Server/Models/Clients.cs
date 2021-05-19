using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Server.Models
{
    public static class Clients
    {
        private static List<Client> clients = new List<Client>();

        public static List<Client> GetClients()
        {
            return clients;
        }

        public static void AddClient(Client client)
        {
            clients.Add(client);
        }
    }
}