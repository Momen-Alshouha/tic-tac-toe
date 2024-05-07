using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tic_tac_toe
{
    public partial class TicTacToe : Form
    {

        enum PlayerTurn
        {
            Player1 , Player2
        }

        private PlayerTurn playerTurn;
        private string Player1Image = "x.png";
        private string Player2Image = "o.png";
        public TicTacToe()
        {
            InitializeComponent();
            playerTurn = PlayerTurn.Player1;
        }

        void ChangePlayerTurn()
        {
            if (playerTurn == PlayerTurn.Player1)
            {
                playerTurn = PlayerTurn.Player2;
                lblPlayerTurn.Text = "Player 2";
            } else
            {
                playerTurn = PlayerTurn.Player1;
                lblPlayerTurn.Text = "Player 1";
            }
        }
        private void TicTacToe_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.FromArgb(255, 255, 255, 255);
            Pen pen = new Pen(white);
            pen.Width = 10;

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            // Draw vertical lines
            e.Graphics.DrawLine(pen, 170, 100, 170, 550);
            e.Graphics.DrawLine(pen, 350, 100, 350, 550);

            // Draw horizontal lines
            e.Graphics.DrawLine(pen, 50, 230, 470, 230);
            e.Graphics.DrawLine(pen, 50, 400, 470, 400);
        }

        private void TicTacToe_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox = sender as PictureBox;
            if (playerTurn == PlayerTurn.Player1 && pictureBox.Enabled)
            {
                pictureBox.Image = Properties.Resources.x;
                
            } else
            {
                pictureBox.Image = Properties.Resources.o;
            }
            pictureBox.Enabled = false;
            ChangePlayerTurn();

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        void ResetGame()
        {
            playerTurn = PlayerTurn.Player1;
            lblPlayerTurn.Text = "Player 1";

            foreach (Control control in this.Controls)
            {
                if (control is PictureBox)
                {
                    PictureBox pictureBox = (PictureBox)control;
                    pictureBox.Enabled = true;
                    pictureBox.Image = Properties.Resources.question_mark2;
                }
            }

        }
    }
}
