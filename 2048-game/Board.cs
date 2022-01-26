using _2048_game.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_game
{
    class Board
    {
        private int[,] board = new int[4, 4];
        private int[,] undoBoard = new int[4, 4];
        private int x_board;
        private int y_board;
        private Random rand = new Random();
        private int score;
        public Board()
        {
            x_board = 9;
            y_board = 10;
            score = 0;
            GenerateNumbers(2);
        }

        public void Draw(PaintEventArgs e)
        {
            string fileName;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    fileName = "";
                    if ( board[i,j] >= 2 )
                    {

                        fileName += board[i, j].ToString();
                        Bitmap image = (Bitmap)Image.FromFile(@"D:\Programowanie\_c#\2048\" + fileName + ".png");
                        e.Graphics.DrawImage(image, x_board + i * 80, y_board + j * 80, 70, 70);
                    }
                }
            }
        }

        private void SaveBoard()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    undoBoard[i, j] = board[i, j];
                }
            }
        }

        public void UndoTurn()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    board[i, j] = undoBoard[i, j];
                }
            }
        }

        public void TurnRight()
        {
            SaveBoard();
            for ( int k = 0; k < 4; k++ )
            {
                for (int i = 3; i >= 0; i--)
                {
                    for (int j = 3; j >= 0; j--)
                    {
                        if ( i >= 1 )
                        {
                            if(board[i, j] == 0 && board[i-1, j] != 0)
                            {
                                board[i, j] = board[i-1, j];
                                board[i-1, j] = 0;
                            }
                            else if(board[i, j] == board[i - 1, j] && board[i, j] != 0)
                            {
                                board[i, j] = board[i - 1, j] * 2;
                                board[i - 1, j] = 0;
                                score += board[i, j];
                            }
                        }
                    }
                }
            }
        }

        public void TurnLeft()
        {
            SaveBoard();
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 3; j >= 0; j--)
                    {
                        if ( i <= 2 )
                        {
                            if (board[i, j] == 0 && board[i+1, j] != 0)
                            {
                                board[i, j] = board[i+1, j];
                                board[i+1, j] = 0;
                            }
                            else if (board[i, j] == board[i + 1, j] && board[i, j] != 0)
                            {
                                board[i, j] = board[i + 1, j] * 2;
                                board[i + 1, j] = 0;
                                score += board[i, j];
                            }
                        }
                    }
                }
            }
        }

        public void TurnUp()
        {
            SaveBoard();
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if ( j <= 2 )
                        {
                            if (board[i, j] == 0 && board[i, j+1] != 0)
                            {
                                board[i, j] = board[i, j+1];
                                board[i, j+1] = 0;
                            }
                            else if (board[i, j] == board[i, j+1] && board[i, j] != 0)
                            {
                                board[i, j] = board[i, j+1]* 2;
                                board[i, j+1] = 0;
                                score += board[i, j];
                            }
                        }
                    }
                }
            }
        }

        public void TurnDown()
        {
            SaveBoard();
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 3; j >= 0; j--)
                    {
                        if (j >= 1)
                        {
                            if (board[i, j] == 0 && board[i, j - 1] != 0)
                            {
                                board[i, j] = board[i, j - 1];
                                board[i, j - 1] = 0;
                            }
                            else if (board[i, j] == board[i, j - 1] && board[i, j] != 0)
                            {
                                board[i, j] = board[i, j - 1] * 2;
                                board[i, j - 1] = 0;
                                score += board[i, j];
                            }
                        }
                    }
                }
            }
        }

        public bool GenerateNewNumber()
        {
            GenerateNumbers(1);

            if (IsThereSpace() == false && IsThereAnyMoveAvalible() == false)
                return true;
            else
                return false;
        }

        private bool IsThereSpace()
        {
            int temp = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] == 0)
                        temp++;
                }
            }

            if (temp == 0)
                return false;
            else
                return true;
        }

        public bool AreYaWiningSon()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] == 2048)
                        return true;
                }
            }
            return false;
        }

        private bool IsThereAnyMoveAvalible()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {

                    if ( i - 1 >= 0)
                    {
                        if( board[i,j] == board[i-1,j] && board[i, j] != 0)
                        {
                            return true;
                        }
                    }

                    if (j - 1 >= 0)
                    {
                        if (board[i, j] == board[i, j-1] && board[i, j] != 0)
                        {
                            return true;
                        }
                    }

                    if (i + 1 <= 3)
                    {
                        if (board[i, j] == board[i + 1, j] && board[i, j] != 0)
                        {
                            return true;
                        }
                    }

                    if( j + 1 <= 3)
                    {
                        if (board[i, j] == board[i , j + 1] && board[i, j] != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void GenerateNumbers( int n )
        {
            int t, p;
            int i = 0;
            do
            {
                t = RandomNumber(0, 3);
                p = RandomNumber(0, 3);

                if (board[t, p] == 0)
                {
                    board[t, p] = 2 + 2 * RandomNumber(0, 1);
                    i++;
                }

            } while ( i != n );
        }

        private int RandomNumber( int min, int max )
        {
            return rand.Next(min, max+1);
        }

        public int ReturnScore()
        {
            return score;
        }
    }
}
