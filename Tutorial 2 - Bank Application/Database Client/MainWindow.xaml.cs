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
    public delegate void SearchOperation(string searchTerm);
    public partial class MainWindow : Window
    {
        private BusinessServerInterface dataServer;

        public MainWindow()
        {
            //Setup window
            InitializeComponent();

            //Factory that generates remote connections
            ChannelFactory<BusinessServerInterface> channelFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            //Set URL and create connection
            string URL = "net.tcp://localhost:8200/BusinessService";
            channelFactory = new ChannelFactory<BusinessServerInterface>(tcp, URL);
            dataServer = channelFactory.CreateChannel();

            //Set Defaults
            LoadData(0);
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
            //Set Text Boxes to ReadOnly
            IndexTextBox.IsReadOnly = true;
            SearchTextBox.IsReadOnly = true;
            //Disable Buttons
            IndexButton.IsEnabled = false;
            SearchButton.IsEnabled = false;
            //Set Progress Bar
            SearchProgressBar.IsIndeterminate = true;

            SearchOperation searchDelegate = SearchData;
            AsyncCallback callbackDelegate = OnSearchDataCompletion;
            searchDelegate.BeginInvoke(SearchTextBox.Text, callbackDelegate, null);
        }

        private void SearchData(string searchTerm)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    dataServer.SearchForEntry(SearchTextBox.Text, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);

                    FirstNameTextBox.Text = fName;
                    LastNameTextBox.Text = lName;
                    BalanceTextBox.Text = bal.ToString("C");
                    AccountNoTextBox.Text = acctNo.ToString();
                    PINTextBox.Text = pin.ToString("D4");

                    UserImage.Source = Imaging.CreateBitmapSourceFromHBitmap(icon.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    icon.Dispose();
                });
            }
            catch (FaultException<SearchFault> exception)
            {
                MessageBox.Show("Error: " + exception.Detail.Issue);
            }
        }

        private void OnSearchDataCompletion(IAsyncResult asyncResult)
        {
            SearchOperation searchDelegate;
            AsyncResult asyncObject = (AsyncResult) asyncResult;
            if (asyncObject.EndInvokeCalled == false)
            {
                searchDelegate = (SearchOperation)asyncObject.AsyncDelegate;
                searchDelegate.EndInvoke(asyncObject);

                Dispatcher.Invoke(() =>
                {
                    IndexTextBox.IsReadOnly = false;
                    SearchTextBox.IsReadOnly = false;
                    IndexButton.IsEnabled = true;
                    SearchButton.IsEnabled = true;
                    SearchProgressBar.IsIndeterminate = false;
                });
            }
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
