using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class ClientWindow : Form
    {
        static string currentClient;
        private bool IsConnected = false;
        const int port = 8888;
        private   StreamReader reader;
        private   StreamWriter writer;
        TcpClient client = new TcpClient();

        
        public ClientWindow()
        {
            InitializeComponent();
            btnDisconnect.Enabled = false;

        }


        private void ClientWindow_Load(object sender, EventArgs e)
        {

        }

        //connect to server 
        private void btnConnet_Click(object sender, EventArgs e)
        {
            
            lstMonitor.Items.Add("Connecting to Remote Server...");
            Task.Run(() => { ConnectClient(); });
            btnConnet.Enabled = false;
            btnDisconnect.Enabled = true;
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            lstMonitor.Invoke((MethodInvoker)delegate { lstMonitor.Items.Add("Disconnected from server"); });
            writer.WriteLine(client.Client.LocalEndPoint + " Disconnected from server");
            writer.Flush();
          
            if (this.client.Connected) {
            
                reader.Close();
                writer.Close();
                btnDisconnect.Enabled = false;
                            
                //this.client.Client.Shutdown(SocketShutdown.Both);
                this.client.Close();

            }
  


        }
        
        private void listActiveClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentClient = listActiveClient.SelectedItem.ToString();

        }

        //send the ball(data) to the server
        private void btnPassBall_Click(object sender, EventArgs e)
        {
            this.writer.WriteLine("Ball pass to " + currentClient);
            this.writer.Flush();
          //  Task.Run(() => {

            //    if (currentClient != null)
            //        {
            //            writer.WriteLine("Ball pass to " + currentClient);
            //            writer.Flush();
            //        }
            //        else
            //        {
            //        lstMonitor.Invoke((MethodInvoker)delegate { lstMonitor.Items.Add("You are not authorised to pass the ball"); });
            //        }

            //string line = reader.ReadLine();
            //Console.WriteLine(line);
            //if (line == null)
            //{
            //    throw new Exception(line);
            //}
            //else
            //{
            //    lstMonitor.Invoke((MethodInvoker)delegate { lstMonitor.Items.Add(line); });
            //}

        //});

            
        }

        //set client and connec to the server 
        public void ConnectClient()
        {
            try { 
                client.Connect("localhost", port);
                NetworkStream serverStream = client.GetStream();
                this.reader = new StreamReader(serverStream);
                this.writer = new StreamWriter(serverStream);


                //ADDED TO THE CLIENT GUI
                listActiveClient.Invoke((MethodInvoker)delegate { listActiveClient.Items.Add(client.Client.LocalEndPoint); });
                lstMonitor.Invoke((MethodInvoker)delegate { lstMonitor.Items.Add("You are connected to the Server"); });

                //thread activate the continous read from server
                Thread thread = new Thread(x => ReadServerStatus((TcpClient)x));
                thread.Start(client);
                thread.Join();
                IsConnected = true;
               
            }
            catch (SocketException ex)
            {
            
                lstMonitor.Invoke((MethodInvoker)delegate { lstMonitor.Items.Add(ex.Message); });
                
            }
        }

        //reads all the data back from server
        //TODO: upgarde to decide where to add to in UI like client list or message list
        public void ReadServerStatus(TcpClient myclient)
        {
            NetworkStream stream = myclient.GetStream();
            this.reader = new StreamReader(stream);
            string status = this.reader.ReadLine();
            
            while (status != null)
            {
                
                lstMonitor.Invoke((MethodInvoker)delegate { lstMonitor.Items.Add(status); });
                Thread.Sleep(1000);
                status=this.reader.ReadLine();
            }
        }

    }

}
