using BlockchainClient.Components;
using BlockchainLibrary;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlockchainClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string ServerURL = "http://localhost:58351/";

        public MainWindow()
        {
            InitializeComponent();

            Task.Factory.StartNew(() =>
            {
                GUIThread();
            });

            Miner miner = new Miner();
            Task.Factory.StartNew(() =>
            {
                miner.MiningThread();
            });
        }

        public void GUIThread()
        {
            Console.WriteLine("GUI Thread Running");

            while (true)
            {
                Dispatcher.Invoke(() =>
                {
                    NumberOfBlocks.Text = $"Number of Blocks: {Blockchain.GetNumberOfBlocks().ToString()}";
                });

                //Add a 1 second delay between each loop
                Thread.Sleep(1000);
            }
        }

        public void BlockchainThread(ClientStruct client)
        {
            Console.WriteLine($"[{Blockchain.MyClientName}Server] Server Thread Running");

            //Create .NET Remoting Server
            ServiceHost host;
            NetTcpBinding tcp = new NetTcpBinding();

            host = new ServiceHost(typeof(ClientServer));
            host.AddServiceEndpoint(typeof(ClientServerInterface), tcp, $"net.tcp://{client.IP}:{client.Port}/ClientServer");
            host.Open();

            //Post Client to Web Server
            RestClient restClient = new RestClient(ServerURL);
            var request = new RestRequest("api/Client/New");
            request.AddJsonBody(client);
            var response = restClient.Post(request);
            Console.WriteLine($"[{Blockchain.MyClientName}Server] Client successfully posted to Web Server");

            Blockchain.MyClientName = client.Name;
        }

        private void StartServerButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"[{Blockchain.MyClientName}GUI] Start Server Button Clicked");

            //Disable UI
            ClientNameBox.IsReadOnly = true;
            PortNumberBox.IsReadOnly = true;
            StartServerButton.IsEnabled = false;

            //Create ClientStruct
            ClientStruct client = new ClientStruct();
            client.Name = ClientNameBox.Text;
            client.IP = "0.0.0.0"; //Placeholder for now, I'm not too sure what to do with the IP address field
            client.Port = UInt32.Parse(PortNumberBox.Text);

            //Start Server Thread
            Task.Factory.StartNew(() =>
            {
                BlockchainThread(client);
            });
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"[{Blockchain.MyClientName}GUI] Submit Transaction Button Clicked");
            if (UInt32.TryParse(AmountBox.Text, out uint amount) &&
               UInt32.TryParse(SenderIDBox.Text, out uint senderID) &&
               UInt32.TryParse(ReceiverIDBox.Text, out uint receiverID))
            {
                //Create Transaction
                Transaction transaction = new Transaction();
                transaction.Amount = amount;
                transaction.SenderID = senderID;
                transaction.ReceiverID = receiverID;

                //Send to Transactions
                RestClient restClient = new RestClient(ServerURL);
                var clientListRequest = new RestRequest("api/Client");
                var clientListResponse = restClient.Get(clientListRequest);
                List<ClientStruct> clients = JsonConvert.DeserializeObject<List<ClientStruct>>(clientListResponse.Content);

                foreach (ClientStruct client in clients)
                {
                    ChannelFactory<ClientServerInterface> channelFactory;
                    NetTcpBinding tcp = new NetTcpBinding();

                    channelFactory = new ChannelFactory<ClientServerInterface>(tcp, $"net.tcp://localhost:{client.Port}/ClientServer");
                    ClientServerInterface clientServer = channelFactory.CreateChannel();

                    try
                    {
                        Console.WriteLine($"[{Blockchain.MyClientName}GUI] Sending transaction to {client.Name}");
                        clientServer.ReceiveNewTransaction(transaction);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"[{Blockchain.MyClientName}GUI] Error: {exception.Message}");
                    }
                }

                AmountBox.Text = "";
                SenderIDBox.Text = "";
                ReceiverIDBox.Text = "";
                TransactionResult.Text = $"Transaction submitted";
            }
            else
            {
                TransactionResult.Text = "Invalid input";
            }
        }

        private void BalanceButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"[{Blockchain.MyClientName}GUI] Get Balance Button Clicked");
            if (UInt32.TryParse(UserIDBox.Text, out uint userID))
            {
                try
                {
                    if(userID == 0)
                    {
                        UserBalance.Text = $"Balance: Infinite coins";
                    }
                    else
                    {
                        float balance = Blockchain.GetUserBalance(userID);
                        UserBalance.Text = $"Balance: {balance} coins";
                    }

                    BalanceResult.Text = $"User {userID} balance retrieved";
                    UserIDBox.Text = "";
                }
                catch(Exception exception)
                {
                    BalanceResult.Text = $"Error: {exception.Message}";
                }
            }
            else
            {
                BalanceResult.Text = "Invalid input";
            }
        }
    }
}
