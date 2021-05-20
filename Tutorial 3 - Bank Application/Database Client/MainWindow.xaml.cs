using API_Classes;
using Database_Interface;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Database_Client
{
    public delegate void SearchDelegate(string searchTerm, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);
    public partial class MainWindow : Window
    {
        private const string BusinessServerURL = "http://localhost:57278/";

        public MainWindow()
        {
            //Setup window
            InitializeComponent();

            //Connect to Web API
            RestClient client = new RestClient(BusinessServerURL);
            RestRequest request = new RestRequest("api/values");

            IRestResponse response = client.Get(request);
            int numEntries = JsonConvert.DeserializeObject<int>(response.Content);

            //Set Defaults
            LoadData(0);
            IndexTextBox.Text = "0";
            TotalItemsLabel.Content = $"Total Items: {numEntries}";
        }
        private void IndexButton_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(IndexTextBox.Text, out int index))
            {
                LoadData(index);
            }
            else
            {
                MessageBox.Show("Error: Invalid Index");
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchData(SearchTextBox.Text);
        }

        private void SearchData(string searchTerm)
        {
            try
            {
                RestClient client = new RestClient(BusinessServerURL);
                RestRequest request = new RestRequest($"api/values/search/{searchTerm}");

                IRestResponse response = client.Get(request);
                DataStruct data = JsonConvert.DeserializeObject<DataStruct>(response.Content);

                FirstNameTextBox.Text = data.fName;
                LastNameTextBox.Text = data.lName;
                BalanceTextBox.Text = data.bal.ToString("C");
                AccountNoTextBox.Text = data.acctNo.ToString();
                PINTextBox.Text = data.pin.ToString("D4");
            }
            catch (FaultException<SearchFault> exception)
            {
                MessageBox.Show("Error: " + exception.Detail.Issue);
            }
        }

        private void LoadData(int index)
        {
            try
            {
                RestClient client = new RestClient(BusinessServerURL);
                RestRequest request = new RestRequest($"api/values/{index}");

                IRestResponse response = client.Get(request);
                DataStruct data = JsonConvert.DeserializeObject<DataStruct>(response.Content);

                FirstNameTextBox.Text = data.fName;
                LastNameTextBox.Text = data.lName;
                BalanceTextBox.Text = data.bal.ToString("C");
                AccountNoTextBox.Text = data.acctNo.ToString();
                PINTextBox.Text = data.pin.ToString("D4");
            }
            catch (FaultException<IndexFault> exception)
            {
                MessageBox.Show("Error: " + exception.Detail.Issue);
            }
        }
    }
}
