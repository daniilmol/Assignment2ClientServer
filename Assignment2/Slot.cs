using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class Slot
    {
        private Color pieceColor;
        private int x;
        private int y;
        public Slot(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.pieceColor = Color.White;
        }
        public void setColor(Color pieceColor)
        {
            this.pieceColor = pieceColor;
        }
        public Color getColor()
        {
            return this.pieceColor;
        }
        public int getX() { return this.x; }
        public int getY() { return this.y; }
    }
}
