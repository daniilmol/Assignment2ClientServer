using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Assignment2
{
    class Receiver
    {
        Form1 gameArea;
        Socket sock;
        EndPoint ep = (EndPoint)(Sender.iep);
        static int senders = 0;
        public bool isHost { get; set; }
        public Receiver(Form1 gameArea) {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sock.Bind(new IPEndPoint(IPAddress.Any, Program.PORT));
            sock.MulticastLoopback = false;
            Debug.WriteLine("Listening on port " + Program.PORT);
            sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse("239.50.50.51")));
            this.gameArea = gameArea;
            isHost = false;
        }
        public void SetMulticastLoopback(bool b) {
            sock.MulticastLoopback = b;
        }
        public void run() {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024];
                    int recv = sock.ReceiveFrom(data, ref ep);
                    string stringData = Encoding.ASCII.GetString(data, 0, recv);
                    StringReader reader = new StringReader(stringData);
                    
                }
            }
            catch (SocketException e) {
                Debug.WriteLine("Problem with the socket, " + e.Message);
                sock.Close();
            }
        }
        public void EnterGame()
        {
            Debug.WriteLine("inside EnterGame()");
            sock.MulticastLoopback = true;
            try
            {
                byte[] data = new byte[1024];
                int recv = sock.ReceiveFrom(data, ref ep);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                StringReader reader = new StringReader(stringData);
                
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: " + e.Message);
            }
            
        }
    }
}
