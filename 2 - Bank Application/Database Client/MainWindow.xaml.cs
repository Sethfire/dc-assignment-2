using Database_Interface;
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
        private readonly BusinessServerInterface dataServer;

        public MainWindow()
        {
            //Setup window
            InitializeComponent();

            //Factory that generates remote connections
            ChannelFactory<BusinessServerInterface> channelFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            //Set URL and create connection
            var URL = "net.tcp://localhost:8200/BusinessService";
            channelFactory = new ChannelFactory<BusinessServerInterface>(tcp, URL);
            dataServer = channelFactory.CreateChannel();

            //Set Defaults
            LoadData(0);
            IndexTextBox.Text = "0";
            TotalItemsLabel.Content = $"Total Items: {dataServer.GetNumEntries()}";
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
                //Set Text Boxes to ReadOnly
                IndexTextBox.IsReadOnly = true;
                SearchTextBox.IsReadOnly = true;
                //Disable Buttons
                IndexButton.IsEnabled = false;
                SearchButton.IsEnabled = false;
                //Set Progress Bar
                SearchProgressBar.IsIndeterminate = true;

                //Asynchronously call dataServer.SearchForEntry()
                SearchDelegate searchDelegate = new SearchDelegate(dataServer.SearchForEntry);
                AsyncCallback callbackMethod = OnSearchDataCompletion;
                searchDelegate.BeginInvoke(searchTerm, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon, callbackMethod, null);
            }
            catch (FaultException<SearchFault> exception)
            {
                MessageBox.Show("Error: " + exception.Detail.Issue);
            }
        }

        private void OnSearchDataCompletion(IAsyncResult asyncResult)
        {
            AsyncResult asyncObject = (AsyncResult) asyncResult;
            SearchDelegate searchDelegate = (SearchDelegate) asyncObject.AsyncDelegate;

            //Retrieve the information
            searchDelegate.EndInvoke(out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon, asyncObject);

            //Set the content to the retrieved information
            FirstNameTextBox.Text = fName;
            LastNameTextBox.Text = lName;
            BalanceTextBox.Text = bal.ToString("C");
            AccountNoTextBox.Text = acctNo.ToString();
            PINTextBox.Text = pin.ToString("D4");

            UserImage.Source = Imaging.CreateBitmapSourceFromHBitmap(icon.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            icon.Dispose();

            //Return to normal
            IndexTextBox.IsReadOnly = false;
            SearchTextBox.IsReadOnly = false;
            IndexButton.IsEnabled = true;
            SearchButton.IsEnabled = true;
            SearchProgressBar.IsIndeterminate = false;

            asyncObject.AsyncWaitHandle.Close();
        }

        private void LoadData(int index)
        {
            try
            {
                dataServer.GetValuesForEntry(index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);

                FirstNameTextBox.Text = fName;
                LastNameTextBox.Text = lName;
                BalanceTextBox.Text = bal.ToString("C");
                AccountNoTextBox.Text = acctNo.ToString();
                PINTextBox.Text = pin.ToString("D4");

                UserImage.Source = Imaging.CreateBitmapSourceFromHBitmap(icon.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                icon.Dispose();
            }
            catch (FaultException<IndexFault> exception)
            {
                MessageBox.Show("Error: " + exception.Detail.Issue);
            }
        }
    }
}
