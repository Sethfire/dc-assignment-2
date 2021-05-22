using BlockchainLibrary;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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

namespace TransactionGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string BlockchainServerURL = "http://localhost:63894/";
        public const string BlockchainMinerURL = "http://localhost:50180/";

        public MainWindow()
        {
            InitializeComponent();
            LoadBlockchainState();
        }

        private void LoadBlockchainState()
        {
            RestClient client = new RestClient(BlockchainServerURL);
            var request = new RestRequest("api/Blockchain/Status");
            var response = client.Get(request);

            BlockchainState blockchainState = JsonConvert.DeserializeObject<BlockchainState>(response.Content);
            NumberOfBlocks.Text = $"Number of Blocks: {blockchainState.NumberOfBlocks.ToString()}";
        }

        private void TransactionButton_Click(object sender, RoutedEventArgs e)
        {
            if(UInt32.TryParse(AmountBox.Text, out uint amount) &&
               UInt32.TryParse(SenderIDBox.Text, out uint senderID) &&
               UInt32.TryParse(ReceiverIDBox.Text, out uint receiverID))
            {
                //Create Transaction
                Transaction transaction = new Transaction();
                transaction.Amount = amount;
                transaction.SenderID = senderID;
                transaction.ReceiverID = receiverID;

                //Send POST request to miner
                RestClient client = new RestClient(BlockchainMinerURL);
                var request = new RestRequest("api/Mine");
                request.AddJsonBody(transaction);
                var response = client.Post(request);
                
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    TransactionResult.Text = "Transaction successful";
                    AmountBox.Text = "";
                    SenderIDBox.Text = "";
                    ReceiverIDBox.Text = "";
                    LoadBlockchainState();
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string message = JsonConvert.DeserializeObject<string>(response.Content);
                    TransactionResult.Text = $"Transaction error: {message}";
                }
            }
            else
            {
                TransactionResult.Text = "Invalid input";
            }
        }

        private void BalanceButton_Click(object sender, RoutedEventArgs e)
        {
            if (UInt32.TryParse(UserIDBox.Text, out uint userID))
            {
                //Send GET request to server
                RestClient client = new RestClient(BlockchainServerURL);
                var request = new RestRequest($"api/Blockchain/User/{userID}");
                var response = client.Get(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    float balance = JsonConvert.DeserializeObject<float>(response.Content);
                    UserBalance.Text = $"Balance: {balance} coins";

                    BalanceResult.Text = $"User {userID} balance retrieved";
                    UserIDBox.Text = "";
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string message = JsonConvert.DeserializeObject<string>(response.Content);
                    BalanceResult.Text = $"Error: {message}";
                }
            }
            else
            {
                BalanceResult.Text = "Invalid input";
            }
        }
    }
}
