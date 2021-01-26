using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class Form1 : Form
    {
        private Game game;
        const int GameAreaWidth = 1280;
        const int GameAreaHeight = 720;
        public const int Scale = 100;
        private List<Rectangle> storedPieces = new List<Rectangle>();
        private List<Brush> storedPiecesColors = new List<Brush>();
        private Receiver receiver;
        private Thread server;
        private Thread client;


        public Form1()
        {
            InitializeComponent();
            this.Height = GameAreaHeight;
            this.Width = GameAreaWidth;
            this.StartPosition = FormStartPosition.CenterScreen;
            receiver = new Receiver(this);
            game = new Game(this);
            game.drawBoard();
            for(int i = 0; i < 7; i++)
            {
                Point newLoc = new Point(i * Scale + 290, 10);
                Button b = new Button();
                b.Text = "V";
                b.Size = new Size(90, 30);
                b.Location = newLoc;
                newLoc.Offset(0, b.Height + 5);
                Controls.Add(b);
                b.Click += new EventHandler(game.dropPeg);
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            for (int i = 0; i < game.getBorders().Length; i++)
            {
                graphics.DrawRectangle(new Pen(Color.Black, 10), game.getBorders()[i]);
            }
            
            for (int i = 0; i < storedPieces.Count; i++)
            {
                graphics.FillEllipse(storedPiecesColors[i], storedPieces[i]);
            }
            
        }

        public void insertPieces(Brush playerColor, Rectangle rec)
        {
            Graphics graphics = this.CreateGraphics();
            graphics.FillEllipse(playerColor, rec);
            storedPieces.Add(rec);
            storedPiecesColors.Add(playerColor);
        }

        private void GameArea_Shown(object sender, EventArgs e)
        {/**
            Thread t = new Thread(new ThreadStart(receiver.EnterGame));
            t.IsBackground = true;
            t.Start();
            Thread.Sleep(1800);
            t.Abort();
            if (gameID == Guid.Empty)
            {
                CreateNewGame();
                MulticastSender.SendGameMsg(-9, "game created");
                // the line above is for when there aren't any packets flowing in the port
                // if there are no packets, Thread t will be blocked on Socket.ReceiveFrom()
                // and calling Abort won't cancel the blocking call
            }
            recv.SetMulticastLoopback(false);
            System.Diagnostics.Debug.WriteLine("started game");
            if (playerNum % 2 == 0)
            {
                me = new Tank(MulticastSender.ID,
                    rnd.Next(0, this.ClientRectangle.Width - Tank.SIZE.Width),
                    rnd.Next((int)(this.ClientRectangle.Height * 0.55), this.ClientRectangle.Height - Tank.SIZE.Height));
                vehicles[playerNum] = me;
                tanks.Add((Tank)me);
            }
            else
            {
                me = new Plane(MulticastSender.ID,
                    rnd.Next(0, this.ClientRectangle.Width - Plane.SIZE.Width),
                    rnd.Next(0, (int)(this.ClientRectangle.Height * 0.45) - Plane.SIZE.Height));
                vehicles[playerNum] = me;
                planes.Add((Plane)me);
            }
            System.Diagnostics.Debug.WriteLine("my vehicle instatiated");
            if (playerNum == 0)
            {
                SetNextPlayer();
                hostThread = new Thread(new ThreadStart(MulticastSender.SendInvitations));
                hostThread.IsBackground = true;
                hostThread.Start();
                System.Diagnostics.Debug.WriteLine("hostThread started");
            }
            recv.IsHost = (playerNum == 0);
            receiverThread = new Thread(new ThreadStart(recv.run));
            receiverThread.IsBackground = true; // thread becomes zombie if this is not explicitly set to true
            receiverThread.Start();*/
        }


    }
}
