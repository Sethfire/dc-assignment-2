using API_Classes;
using Database_Interface;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Database_Client
{
    public delegate void SearchDelegate(string searchTerm, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);
    public partial class MainWindow : Window
    {
        public const string BusinessServerURL = "http://localhost:57278/";

        public MainWindow()
        {
            //Setup window
            InitializeComponent();

            //Connect to Web API
            RestClient client = new RestClient(BusinessServerURL);
            var request = new RestRequest("api/values");
            var response = client.Get(request);
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
            //Set Text Boxes to ReadOnly
            IndexTextBox.IsReadOnly = true;
            SearchTextBox.IsReadOnly = true;
            //Disable Buttons
            IndexButton.IsEnabled = false;
            SearchButton.IsEnabled = false;
            //Set Progress Bar
            SearchProgressBar.IsIndeterminate = true;

            string searchTerm = SearchTextBox.Text;
            Thread thread = new Thread(()=>SearchData(searchTerm));
            thread.Start();
        }

        private void SearchData(string searchTerm)
        {
            RestClient client = new RestClient(BusinessServerURL);
            var request = new RestRequest($"api/values/search/{searchTerm}");
            var response = client.Get(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                DataStruct data = JsonConvert.DeserializeObject<DataStruct>(response.Content);

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    FirstNameTextBox.Text = data.fName;
                    LastNameTextBox.Text = data.lName;
                    BalanceTextBox.Text = data.bal.ToString("C");
                    AccountNoTextBox.Text = data.acctNo.ToString();
                    PINTextBox.Text = data.pin.ToString("D4");

                    IndexTextBox.IsReadOnly = false;
                    SearchTextBox.IsReadOnly = false;
                    IndexButton.IsEnabled = true;
                    SearchButton.IsEnabled = true;
                    SearchProgressBar.IsIndeterminate = false;
                }));
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show("Error: Account not found");
            }
}

        private void LoadData(int index)
        {
            RestClient client = new RestClient(BusinessServerURL);
            var request = new RestRequest($"api/values/{index}");
            var response = client.Get(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                DataStruct data = JsonConvert.DeserializeObject<DataStruct>(response.Content);

                FirstNameTextBox.Text = data.fName;
                LastNameTextBox.Text = data.lName;
                BalanceTextBox.Text = data.bal.ToString("C");
                AccountNoTextBox.Text = data.acctNo.ToString();
                PINTextBox.Text = data.pin.ToString("D4");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show("Error: Account not found");
            }
        }
    }
}
