using Database_Interface;
using System;
using System.Drawing;
using System.ServiceModel;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Database_Client
{
    public partial class MainWindow : Window
    {
        private DataServerInterface dataServer;

        public MainWindow()
        {
            //Setup window
            InitializeComponent();

            //Factory that generates remote connections
            ChannelFactory<DataServerInterface> channelFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            //Set URL and create connection
            string URL = "net.tcp://localhost:8100/DataService";
            channelFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
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

        private void LoadData(int index)
        {
            try
            {
                uint acctNo = 0;
                uint pin = 0;
                int bal = 0;
                string fName = "";
                string lName = "";
                Bitmap icon;

                dataServer.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out icon);

                FirstNameTextBox.Text = fName;
                LastNameTextBox.Text = lName;
                BalanceTextBox.Text = bal.ToString("C");
                AccountNoTextBox.Text = acctNo.ToString();
                PINTextBox.Text = pin.ToString("D4");

                //Taken from provided solution
                UserImage.Source = Imaging.CreateBitmapSourceFromHBitmap(icon.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                icon.Dispose();
            }
            catch (FaultException<IndexFault> e)
            {
                MessageBox.Show("Error: " + e.Detail.Issue);
            }
        }
    }
}
