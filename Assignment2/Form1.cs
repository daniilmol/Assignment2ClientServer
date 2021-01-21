using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public Form1()
        {
            //InitializeComponent();
            this.Height = GameAreaHeight;
            this.Width = GameAreaWidth;
            this.StartPosition = FormStartPosition.CenterScreen;
            game = new Game(this);
            game.drawBoard();
        }

        private void drawBoard() { 
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
