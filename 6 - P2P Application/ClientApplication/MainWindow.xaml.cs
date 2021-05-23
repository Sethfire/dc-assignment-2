using API_Library;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
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

namespace ClientApplication
{

    public partial class MainWindow : Window
    {
        public const string ServerURL = "http://localhost:62718/";
        private string myClientName;
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("Starting up Client Application");

            Task.Factory.StartNew(() =>
            {
                GUIThread();
            });

            Task.Factory.StartNew(() =>
            {
                NetworkingThread();
            });
        }

        private void GUIThread()
        {
            Console.WriteLine("GUI Thread Running");

            while (true)
            {
                Dispatcher.Invoke(() =>
                {
                    AvailableJobs.Text = $"Available Jobs: {JobBoard.GetNumOfAvailableJobs()}";
                    CompletedJobs.Text = $"Completed Jobs: {JobBoard.GetNumOfCompletedJobs()}";
                });

                //Add a 1 second delay between each loop
                Thread.Sleep(1000);
            }
        }

        private void StartServerButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Creating new Client Struct and Posting to Web Server");

            //Disable UI
            ClientNameBox.IsReadOnly = true;
            PortNumberBox.IsReadOnly = true;
            StartServerButton.IsEnabled = false;

            //Create ClientStruct
            ClientStruct client = new ClientStruct();
            client.ClientName = ClientNameBox.Text;
            client.IP = "0.0.0.0"; //Placeholder, I'm not too sure what to do with the IP address field
            client.Port = UInt32.Parse(PortNumberBox.Text);

            //Start New Thread
            Task.Factory.StartNew(() =>
            {
                ServerThread(client);
            });
        }

        private void PostJobButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Creating new Job Struct and adding to Job Board");
            JobStruct job = new JobStruct();
            Random rand = new Random();
            job.JobID = rand.Next(1000000, 9999999);
            job.Code = JobTextBox.Text;
            job.FunctionName = JobTextBox.Text;
            job.VarA = Int32.Parse(VarABox.Text);
            job.VarB = Int32.Parse(VarBBox.Text);

            JobBoard.NewJob(job);
        }

        private void NetworkingThread()
        {
            Console.WriteLine("Networking Thread Running");

            RestClient restClient = new RestClient(ServerURL);

            while (true)
            {
                //Look for new clients
                var clientListRequest = new RestRequest("api/Client");
                var clientListResponse = restClient.Get(clientListRequest);
                List<ClientStruct> clients = JsonConvert.DeserializeObject<List<ClientStruct>>(clientListResponse.Content);

                //Client List isn't null or empty
                if(clients != null)
                {
                    if(clients.Count != 0)
                    {
                        //Check each client for jobs and do them if it can
                        foreach (ClientStruct client in clients)
                        {
                            //Check if this is not our own client
                            if (myClientName != null)
                            {
                                if (myClientName.Equals(client.ClientName) == false)
                                {
                                    try
                                    {
                                        Console.WriteLine($"{myClientName}: Looking for jobs by {client.ClientName}");
                                        ChannelFactory<ClientServerInterface> channelFactory;
                                        NetTcpBinding tcp = new NetTcpBinding();

                                        channelFactory = new ChannelFactory<ClientServerInterface>(tcp, $"net.tcp://localhost:{client.Port}/ClientServer");
                                        ClientServerInterface clientServer = channelFactory.CreateChannel();

                                        //Check the number of available jobs
                                        if (clientServer.GetNumOfAvailableJobs() > 0)
                                        {
                                            Console.WriteLine($"{myClientName}: Job Found");
                                            //Download the job
                                            JobStruct job = clientServer.DownloadJob();
                                            string solution = SolveJob(job.Code, job.FunctionName, job.VarA, job.VarB);
                                            clientServer.UploadSolution(job.JobID, solution);

                                        }
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                }
                            }
                        }
                    }
                }

                //Add a 1 second delay between each loop
                Thread.Sleep(1000);
            }
        }

        private string SolveJob(string code, string functionName, int varA, int varB)
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            engine.Execute(code, scope);
            dynamic jobFunction = scope.GetVariable(functionName);
            var solution = jobFunction(varA, varB);

            return solution;
        }

        private void ServerThread(ClientStruct client)
        {
            Console.WriteLine("Server Thread Running");

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
            Console.WriteLine("Client successfully posted to Web Server");

            myClientName = client.ClientName;
        }
    }
}
