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
        private int playerNum = 0;
        private bool freeze = true;
        private bool frozen;
        private bool running = true;

        public Game getGame(){return game;}

        public Form1()
        {
            InitializeComponent();
            Console.WriteLine("1");
            this.Height = GameAreaHeight;

            this.Width = GameAreaWidth;

            this.StartPosition = FormStartPosition.CenterScreen;
            game = new Game(this);

            for (int i = 0; i < 7; i++)
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
            receiver = new Receiver(this);
            Console.WriteLine("2");

            GameArea_Shown();
            Console.WriteLine("3");
            Console.WriteLine("4");
            game.drawBoard();
            Console.WriteLine("5");
            
        }
        public void freezeBoard(bool enabled)
        {
            Console.WriteLine("Freezing board: " + !enabled);
            foreach (Control c in this.Controls)
            {
                Button b = c as Button;
                if (b != null)
                {
                    Invoke(new Action(() => { b.Enabled = enabled; }));

                    //b.Enabled = enabled;
                }
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

        public void insertPieces(Brush playerColor, Rectangle rec, bool freeze)
        {
            Graphics graphics = this.CreateGraphics();
            graphics.FillEllipse(playerColor, rec);
            storedPieces.Add(rec);
            storedPiecesColors.Add(playerColor);
            this.freeze = freeze;
        }

        private void GameArea_Shown()
        {
            if (!receiver.bTest)
            {
                receiver.SetMulticastLoopback(false);
                System.Diagnostics.Debug.WriteLine("started game");

                //receiver.isHost = (playerNum == 0);
                server = new Thread(new ThreadStart(receiver.run));
                server.IsBackground = true;
                server.Start();
            }

        }


        public void N() {
            
        }

        public void loopBack(bool loopback) {
            receiver.SetMulticastLoopback(loopback);
        }

    }
}
