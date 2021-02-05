using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

using System.Threading.Tasks;

namespace Assignment2
{
    class Receiver
    {
        Form1 gameArea;
        public Socket sock
        {
            get; private set;
        }
        EndPoint ep = (EndPoint)(Sender.iep);
        public bool isHost { get; set; }

        public bool bTest
        {
            get; set;
        }
        public Receiver(Form1 gameArea)
        {

            sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint test = new IPEndPoint(IPAddress.Any, Program.PORT);
            try
            {
                sock.Bind(test);
            }
            catch (Exception e)
            {
                bTest = true;
                Console.WriteLine($"Already opened, {e}");
            }

            sock.MulticastLoopback = false;
            Debug.WriteLine("Listening on port " + Program.PORT);
            sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse("239.50.50.51")));
            this.gameArea = gameArea;
            isHost = false;
        }
        public void SetMulticastLoopback(bool b)
        {
            sock.MulticastLoopback = b;
        }
        public void run()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024];
                    Console.WriteLine("Ran handle game message method");

                    //Console.WriteLine(sock.Available);
                    int recv = sock.ReceiveFrom(data, ref ep);
                    string stringData = Encoding.ASCII.GetString(data, 0, recv);
                    StringReader reader = new StringReader(stringData);
                    HandleGameMsg(reader.ReadLine());
                    Console.WriteLine("Ran handle game message method");
                }
            }
            catch (SocketException e)
            {
                Debug.WriteLine("Problem with the socket, " + e.Message);
                sock.Close();
            }
            Console.WriteLine("Ending run method");
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
        private void HandleGameMsg(string msg)
        {
            System.Windows.Forms.Application.Exit();
            string[] ar = msg.Split(',');
            int type = int.Parse(ar[0]);
            int playerNum, x, y, dir, scoreType, score;
            Console.WriteLine("In Handle Game Msg");
            switch (type)
            {
                case 0: // peg placement
                    x = int.Parse(ar[0]);
                    y = int.Parse(ar[1]);
                    Console.WriteLine("X: " + x + "Y: " + y);
                    gameArea.insertPieces(Brushes.Red, Game.grid[0, Game.columnHeight(0)]);
                    //playerID = Guid.Parse(ar[1]);
                    //playerNum = int.Parse(ar[2]);
                    //x = int.Parse(ar[3]);
                    //y = int.Parse(ar[4]);
                    //dir = int.Parse(ar[5]);
                    //form.MovePlayer(playerID, playerNum, x, y, dir);
                    break;
                case -1: // disconnect
                    //playerID = Guid.Parse(ar[1]);
                    //playerNum = int.Parse(ar[2]);
                    //form.RemovePlayer(playerID, playerNum);
                    break;
            }
        }
    }
}
