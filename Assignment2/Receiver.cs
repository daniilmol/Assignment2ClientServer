﻿using System;
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
        bool locked = false;
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
            IPAddress localIP = IPAddress.Any;
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint test = new IPEndPoint(localIP, Program.PORT);

            try
            {
                isHost = true;
                sock.MulticastLoopback = true;
                //sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse("239.50.50.51")));
                sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                sock.Bind(test);
                MulticastOption mo = new MulticastOption(IPAddress.Parse("239.50.50.51"), localIP);
                sock.SetSocketOption(SocketOptionLevel.IP,
                                        SocketOptionName.AddMembership,
                                        mo);
                //gameArea.freezeBoard(false);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            //bTest = true;
            //Console.WriteLine($"Already opened, {e}");
            //isHost = false;
            //sock.MulticastLoopback = false;



            //gameArea.freezeBoard(true);
            }



            Debug.WriteLine("Listening on port " + Program.PORT);
            this.gameArea = gameArea;
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
                    try
                    {
                        byte[] data = new byte[1024];
                        Console.WriteLine("Ran handle game message method");

                        //Console.WriteLine(sock.Available);
                        sock.MulticastLoopback = true;
                        int recv = sock.ReceiveFrom(data, ref ep);
                        string stringData = Encoding.ASCII.GetString(data, 0, recv);
                        StringReader reader = new StringReader(stringData);
                        sock.MulticastLoopback = false;

                        HandleGameMsg(reader.ReadLine());
                        Console.WriteLine("Ran handle game message method");
                    }
                    catch (SocketException e) { sock.Close(); }
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
            //Debug.WriteLine("inside EnterGame()");
            sock.MulticastLoopback = true;
            try
            {

                byte[] data = new byte[1024];
                int recv = sock.ReceiveFrom(data, ref ep);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                StringReader reader = new StringReader(stringData);
                sock.MulticastLoopback = false;
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: " + e.Message);
            }

        }
        private void HandleGameMsg(string msg)
        {
            
            string[] ar = msg.Split(',');
            int type = int.Parse(ar[0]);
            int playerNum, x, y, dir, scoreType, score;
            Console.WriteLine("In Handle Game Msg");
            sock.MulticastLoopback = false;
            Debug.Write("TYPE: " + type);
                switch (type)
                {
                    case 0: // peg placement
                    x = int.Parse(ar[1]);
                    int player = int.Parse(ar[3]);
                    if (int.Parse(ar[4]) == 0){
                            Console.WriteLine("Player is: " + player);
                            if (player == 0)
                            {
                                gameArea.insertPieces(Brushes.Red, Game.grid[x, Game.columnHeight(x)]);
                                Game.board[x, Game.HEIGHT - 1 - Game.columnHeight(x)].setColor(Color.Red);
                                Game.currentPlayer = 1;
                                gameArea.freezeBoard(true);
                            }
                            else if (player == 1)
                            {
                                gameArea.insertPieces(Brushes.Yellow, Game.grid[x, Game.columnHeight(x)]);
                                Game.board[x, Game.HEIGHT - 1 - Game.columnHeight(x)].setColor(Color.Yellow);
                                Game.currentPlayer = 0;
                                gameArea.freezeBoard(true);
                            }
                            gameArea.freezeBoard(true);
                        } else {
                        if (player == 0)
                        {
                            gameArea.freezeBoard(false);
                            gameArea.insertPieces(Brushes.Red, Game.grid[x, Game.columnHeight(x)]);
                            Game.board[x, Game.HEIGHT - 1 - Game.columnHeight(x)].setColor(Color.Red);
                            createText("Red");
                        }
                        else if (player == 1) {
                            gameArea.freezeBoard(false);
                            gameArea.insertPieces(Brushes.Yellow, Game.grid[x, Game.columnHeight(x)]);
                            Game.board[x, Game.HEIGHT - 1 - Game.columnHeight(x)].setColor(Color.Yellow);
                            createText("Yellow");
                        }
                        }
                        
                        break;
                    case 1:
                        break;
                }
        }
        private void createText(string winner) {
            Point newLoc = new Point(1000, 300);
            TextBox b = new TextBox();
            b.Text = winner + " is the winner!";
            b.Size = new Size(300, 50);
            b.Location = newLoc;
            b.Enabled = false;
            //newLoc.Offset(0, b.Height + 5);
            gameArea.Invoke(new Action(() => { gameArea.Controls.Add(b); }));
        }
    }
}
