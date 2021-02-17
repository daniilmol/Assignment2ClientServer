using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using Assignment2.Properties;

namespace Assignment2
{
    class Game
    {
        private const int WIDTH = 7;
        public const int HEIGHT = 6;
        private Brush player1Fill;
        private Brush player2Fill;
        public static Slot[,] board;
        private Form1 form;
        private int Scale = Form1.Scale;
        private Rectangle[] borders;
        public static int currentPlayer;
        public static Rectangle[,] grid;
        public Game(Form1 gameArea)
        {
            form = gameArea;
            player1Fill = Brushes.Red;
            player2Fill = Brushes.Yellow;
            currentPlayer = 0;

        }
        public void drawBoard()
        {
            board = new Slot[WIDTH, HEIGHT];
            createSlots();
            borders = new Rectangle[WIDTH * HEIGHT];
            grid = new Rectangle[WIDTH, HEIGHT];
            int i = 0;
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    borders[i] = new Rectangle(x * Scale + 290, y * Scale + 50, Scale, Scale);
                    i++;
                    grid[x, y] = new Rectangle(x * Scale + 290, y * Scale + 50, Scale, Scale);
                }
            }
        }
        public void freezeBoard(bool enabled)
        {
            foreach (Control c in form.Controls)
            {
                Button b = c as Button;
                if (b != null)
                {
                    form.Invoke(new Action(() => { b.Enabled = enabled; }));
                }
            }
        }
        public void dropPeg(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Point p = b.Location;
            switch (p.X)
            {
                case 290:
                    if (columnHeight(0) != -1)
                    {
                        switch (currentPlayer)
                        {
                            case 0:
                                form.insertPieces(player1Fill, grid[0, columnHeight(0)]);
                                board[0, HEIGHT - 1 - columnHeight(0)].setColor(Color.Red);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[0, HEIGHT - 2 - columnHeight(0)]));
                                form.loopBack(false);
                                int winningIndex = 0;
                                if (checkWinState(board[0, HEIGHT - 2 - columnHeight(0)])){
                                    winningIndex = 1;
                                    createText("Red");
                                }
                                Sender.SendGameMsg(0, 0 + "," + (HEIGHT - 2 - columnHeight(0)) + "," + currentPlayer + "," + winningIndex);
                                
                                form.loopBack(true);

                                currentPlayer = 1;
                                Console.WriteLine("Y: " + (HEIGHT - 1 - columnHeight(0)));
                                //Sender.SendGameMsg(1, "Unfreeze");
                                break;
                            case 1:
                                form.insertPieces(player2Fill, grid[0, columnHeight(0)]);
                                board[0, HEIGHT - 1 - columnHeight(0)].setColor(Color.Yellow);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[0, HEIGHT - 2 - columnHeight(0)]));
                                form.loopBack(false);
                                winningIndex = 0;
                                if (checkWinState(board[0, HEIGHT - 2 - columnHeight(0)]))
                                {
                                    createText("Yellow");

                                    winningIndex = 1;
                                }
                                Sender.SendGameMsg(0, 0 + "," + (HEIGHT - 2 - columnHeight(0)) + "," + currentPlayer + "," + winningIndex);

                                form.loopBack(true);

                                currentPlayer = 0;
                                break;
                        }

                    }
                    break;
                case 390:
                    if (columnHeight(1) != -1)
                    {
                        switch (currentPlayer)
                        {
                            case 0:
                                form.insertPieces(player1Fill, grid[1, columnHeight(1)]);
                                board[1, HEIGHT - 1 - columnHeight(1)].setColor(Color.Red);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[1, HEIGHT - 2 - columnHeight(1)]));
                                form.loopBack(false);
                                int winningIndex = 0;
                                if (checkWinState(board[1, HEIGHT - 2 - columnHeight(1)]))
                                {
                                    createText("Red");

                                    winningIndex = 1;
                                }
                                Sender.SendGameMsg(0, 1 + "," + (HEIGHT - 2 - columnHeight(1)) + "," + currentPlayer + "," + winningIndex);

                                form.loopBack(true);
                                currentPlayer = 1;
                                break;
                            case 1:
                                form.insertPieces(player2Fill, grid[1, columnHeight(1)]);
                                board[1, HEIGHT - 1 - columnHeight(1)].setColor(Color.Yellow);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[1, HEIGHT - 2 - columnHeight(1)]));
                                form.loopBack(false);
                                winningIndex = 0;
                                if (checkWinState(board[1, HEIGHT - 2 - columnHeight(1)]))
                                {
                                    createText("Yellow");

                                    winningIndex = 1;
                                }
                                Sender.SendGameMsg(0, 1 + "," + (HEIGHT - 2 - columnHeight(1)) + "," + currentPlayer + "," + winningIndex);


                                form.loopBack(true);

                                currentPlayer = 0;
                                break;
                        }
                    }
                    break;
                case 490:
                    if (columnHeight(2) != -1)
                    {
                        switch (currentPlayer)
                        {
                            case 0:
                                form.insertPieces(player1Fill, grid[2, columnHeight(2)]);
                                board[2, HEIGHT - 1 - columnHeight(2)].setColor(Color.Red);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[2, HEIGHT - 2 - columnHeight(2)]));
                                form.loopBack(false);
                                int winningIndex = 0;
                                if (checkWinState(board[2, HEIGHT - 2 - columnHeight(2)]))
                                {
                                    createText("Red");

                                    winningIndex = 1;
                                }
                                Sender.SendGameMsg(0, 2 + "," + (HEIGHT - 2 - columnHeight(2)) + "," + currentPlayer + "," + winningIndex);


                                form.loopBack(true);

                                currentPlayer = 1;
                                break;
                            case 1:
                                form.insertPieces(player2Fill, grid[2, columnHeight(2)]);
                                board[2, HEIGHT - 1 - columnHeight(2)].setColor(Color.Yellow);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[2, HEIGHT - 2 - columnHeight(2)]));
                                form.loopBack(false);
                                winningIndex = 0;
                                if (checkWinState(board[2, HEIGHT - 2 - columnHeight(2)]))
                                {
                                    createText("Yellow");

                                    winningIndex = 1;
                                }
                                Sender.SendGameMsg(0, 2 + "," + (HEIGHT - 2 - columnHeight(2)) + "," + currentPlayer + "," + winningIndex);


                                form.loopBack(true);

                                currentPlayer = 0;
                                break;
                        }
                    }
                    break;
                case 590:
                    if (columnHeight(3) != -1)
                    {
                        switch (currentPlayer)
                        {
                            case 0:
                                form.insertPieces(player1Fill, grid[3, columnHeight(3)]);
                                board[3, HEIGHT - 1 - columnHeight(3)].setColor(Color.Red);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[3, HEIGHT - 2 - columnHeight(3)]));
                                form.loopBack(false);
                                int winningIndex = 0;
                                if (checkWinState(board[3, HEIGHT - 2 - columnHeight(3)]))
                                {
                                    createText("Red");

                                    winningIndex = 1;
                                }
                                Sender.SendGameMsg(0, 3 + "," + (HEIGHT - 2 - columnHeight(3)) + "," + currentPlayer + "," + winningIndex);


                                form.loopBack(true);

                                currentPlayer = 1;
                                break;
                            case 1:
                                form.insertPieces(player2Fill, grid[3, columnHeight(3)]);
                                board[3, HEIGHT - 1 - columnHeight(3)].setColor(Color.Yellow);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[3, HEIGHT - 2 - columnHeight(3)]));
                                form.loopBack(false);
                                winningIndex = 0;
                                if (checkWinState(board[3, HEIGHT - 2 - columnHeight(3)]))
                                {
                                    createText("Yellow");

                                    winningIndex = 1;
                                }
                                Sender.SendGameMsg(0, 3 + "," + (HEIGHT - 2 - columnHeight(3)) + "," + currentPlayer + "," + winningIndex);


                                form.loopBack(true);

                                currentPlayer = 0;
                                break;
                        }
                    }
                    break;
                case 690:
                    if (columnHeight(4) != -1)
                    {
                        switch (currentPlayer)
                        {
                            case 0:
                                form.insertPieces(player1Fill, grid[4, columnHeight(4)]);
                                board[4, HEIGHT - 1 - columnHeight(4)].setColor(Color.Red);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[4, HEIGHT - 2 - columnHeight(4)]));
                                form.loopBack(false);
                                int winningIndex = 0;
                                if (checkWinState(board[4, HEIGHT - 2 - columnHeight(4)]))
                                {
                                    createText("Red");

                                    winningIndex = 1;
                                }
                                Sender.SendGameMsg(0, 4 + "," + (HEIGHT - 2 - columnHeight(4)) + "," + currentPlayer + "," + winningIndex);


                                form.loopBack(true);

                                currentPlayer = 1;
                                break;
                            case 1:
                                form.insertPieces(player2Fill, grid[4, columnHeight(4)]);
                                board[4, HEIGHT - 1 - columnHeight(4)].setColor(Color.Yellow);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[4, HEIGHT - 2 - columnHeight(4)]));
                                form.loopBack(false);
                                winningIndex = 0;
                                if (checkWinState(board[4, HEIGHT - 2 - columnHeight(4)]))
                                {
                                    createText("Yellow");

                                    winningIndex = 1;
                                }
                                Sender.SendGameMsg(0, 4 + "," + (HEIGHT - 2 - columnHeight(4)) + "," + currentPlayer + "," + winningIndex);


                                form.loopBack(true);

                                currentPlayer = 0;
                                break;
                        }
                    }
                    break;
                case 790:
                    if (columnHeight(5) != -1)
                    {
                        switch (currentPlayer)
                        {
                            case 0:
                                form.insertPieces(player1Fill, grid[5, columnHeight(5)]);
                                board[5, HEIGHT - 1 - columnHeight(5)].setColor(Color.Red);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[5, HEIGHT - 2 - columnHeight(5)]));
                                form.loopBack(false);
                                int winningIndex = 0;
                                if (checkWinState(board[5, HEIGHT - 2 - columnHeight(5)]))
                                {
                                    createText("Red");

                                    winningIndex = 1;
                                }
                                Sender.SendGameMsg(0, 5 + "," + (HEIGHT - 2 - columnHeight(5)) + "," + currentPlayer + "," + winningIndex);


                                form.loopBack(true);

                                currentPlayer = 1;
                                break;
                            case 1:
                                form.insertPieces(player2Fill, grid[5, columnHeight(5)]);
                                board[5, HEIGHT - 1 - columnHeight(5)].setColor(Color.Yellow);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[5, HEIGHT - 2 - columnHeight(5)]));
                                form.loopBack(false);
                                winningIndex = 0;
                                if (checkWinState(board[5, HEIGHT - 2 - columnHeight(5)]))
                                {
                                    createText("Yellow");

                                    winningIndex = 1;
                                }
                                Sender.SendGameMsg(0, 5 + "," + (HEIGHT - 2 - columnHeight(5)) + "," + currentPlayer + "," + winningIndex);


                                form.loopBack(true);

                                currentPlayer = 0;
                                break;
                        }
                    }
                    break;
                case 890:
                    if (columnHeight(6) != -1)
                    {
                        switch (currentPlayer)
                        {
                            case 0:
                                form.insertPieces(player1Fill, grid[6, columnHeight(6)]);
                                board[6, HEIGHT - 1 - columnHeight(6)].setColor(Color.Red);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[6, HEIGHT - 2 - columnHeight(6)]));
                                form.loopBack(false);
                                int winningIndex = 0;
                                if (checkWinState(board[6, HEIGHT - 2 - columnHeight(6)]))
                                {
                                    winningIndex = 1;
                                    createText("Red");
                                }
                                Sender.SendGameMsg(0, 6 + "," + (HEIGHT - 2 - columnHeight(6)) + "," + currentPlayer + "," + winningIndex);


                                form.loopBack(true);

                                currentPlayer = 1;
                                break;
                            case 1:
                                form.insertPieces(player2Fill, grid[6, columnHeight(6)]);
                                board[6, HEIGHT - 1 - columnHeight(6)].setColor(Color.Yellow);
                                System.Diagnostics.Debug.WriteLine(checkWinState(board[6, HEIGHT - 2 - columnHeight(6)]));
                                form.loopBack(false);
                                winningIndex = 0;
                                if (checkWinState(board[6, HEIGHT - 2 - columnHeight(6)]))
                                {
                                    createText("Yellow");

                                    winningIndex = 1;
                                }
                                Sender.SendGameMsg(0, 6 + "," + (HEIGHT - 2 - columnHeight(6)) + "," + currentPlayer + "," + winningIndex);


                                form.loopBack(true);

                                currentPlayer = 0;
                                break;
                        }
                    }
                    break;
            }
            freezeBoard(false);
        }

        public static int columnHeight(int columnNo)
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                if (board[columnNo, i].getColor() == Color.White)
                {
                    return HEIGHT - 1 - i;
                }
            }
            return -1;
        }
        private void createSlots()
        {
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    board[x, y] = new Slot(x, y);
                }
            }
        }

        private bool checkWinState(Slot center)
        {
            int[] directions1 = new int[2] { 0, -1 };
            int[] directions2 = new int[2] { 1, -1 };
            int[] directions3 = new int[2] { 1, 0 };
            int[] directions4 = new int[2] { 1, 1 };
            int[][] directionSteps = new int[][] { directions1, directions2, directions3, directions4 };
            Color centerColor = center.getColor();
            System.Diagnostics.Debug.WriteLine("Center: " + center.getColor() + " CenterX: " + center.getX() + " CenterY: " + center.getY());
            int centerX = center.getX();
            int centerY = center.getY();
            for (int i = 0; i < directionSteps.Length; i++)
            {
                int slotCount = 0;
                for (int verticality = -1; verticality <= 1; verticality += 2)
                {
                    int xStep = directionSteps[i][0] * verticality;
                    int yStep = directionSteps[i][1] * verticality;
                    for (int distance = 0; distance < 6; distance++)
                    {
                        int x = centerX + xStep * distance;
                        int y = centerY + yStep * distance;
                        if(!outOfBounds(x,y))
                            System.Diagnostics.Debug.WriteLine("x: " + x + " y: " + y + " " + board[x, y].getColor());
                        if (outOfBounds(x, y))
                            break;
                        if (board[x, y].getColor() == centerColor)
                            slotCount++;
                        else
                            break;
                    }
                }
                if (slotCount >= 4)
                    return true;
            }

            return false;
        }

        private bool outOfBounds(int x, int y)
        {
 //           System.Diagnostics.Debug.WriteLine("x: " + x + " y: " + y + " WIDTH: " + WIDTH + " HEIGHT: " + HEIGHT);
            if (x < 0 || x > WIDTH - 1 || y < 0 || y > HEIGHT - 1)
            {
                return true;
            }
            return false;
        }

        public Rectangle[] getBorders() { return this.borders; }
        private void createText(string winner)
        {
            Point newLoc = new Point(1000, 300);
            TextBox b = new TextBox();
            b.Text = winner + " is the winner!";
            b.Size = new Size(300, 50);
            b.Location = newLoc;
            b.Enabled = false;
            //newLoc.Offset(0, b.Height + 5);
            form.Invoke(new Action(() => { form.Controls.Add(b); }));
        }
    }
}
