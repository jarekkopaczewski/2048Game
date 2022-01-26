using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_game
{
    public partial class MainForm : Form
    {
        private Board board = new Board();
        private bool wannaContinue = false;
        private bool mouseDown;
        private Point lastLocation;
        public MainForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            board.Draw(e);
            labelScore.Text = board.ReturnScore().ToString();
        }

        private void GameStandard()
        {
            if (board.GenerateNewNumber() == true)
            {
                MessageBox.Show("You lose\r\nWanna play again?", "ok");
                board = new Board();
                pictureBox1.Refresh();
                labelScore.Text = "0";
            }
            pictureBox1.Refresh();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                board.TurnUp();
                GameStandard();
            }
            if (e.KeyCode == Keys.Left)
            {
                board.TurnLeft();
                GameStandard();
            }
            else if (e.KeyCode == Keys.Right)
            {
                board.TurnRight();
                GameStandard();
            }
            else if (e.KeyCode == Keys.Down)
            {
                board.TurnDown();
                GameStandard();
            }


            if (Int32.Parse(labelScore.Text) > Int32.Parse(labelBest.Text))
                labelBest.Text = labelScore.Text;

            if( board.AreYaWiningSon() == true && wannaContinue != true)
            {

                string message = "You won\r\nWanna continue?";
                string title = "2048";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);

                if (result == DialogResult.Yes)
                {
                    wannaContinue = true;
                }
                else
                {
                    board = new Board();
                    pictureBox1.Refresh();
                    labelScore.Text = "0";
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string message = "Are you sure that you would like to close the game?";
            const string caption = "Game exit";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void pictureBoxUndo_Click(object sender, EventArgs e)
        {
            board.UndoTurn();
            pictureBox1.Refresh();
        }

        private void pictureBoxNewGame_Click(object sender, EventArgs e)
        {
            board = new Board();
            pictureBox1.Refresh();

            if (Int32.Parse(labelScore.Text) > Int32.Parse(labelBest.Text))
                labelBest.Text = labelScore.Text;

            labelScore.Text = "0";
        }

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
    }
}
