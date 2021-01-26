using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public Form1()
        {
            InitializeComponent();
            this.Height = GameAreaHeight;
            this.Width = GameAreaWidth;
            this.StartPosition = FormStartPosition.CenterScreen;
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

        
    }
}
