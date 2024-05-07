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

        enum enPlayerTurn
        {
            Player1, Player2
        }
        enum enWinner
        {
            FirstPlayer, SecondPlayer, Draw
        }

        private enPlayerTurn playerTurn;
        private PictureBox[,] pictureBoxes;

        public TicTacToe()
        {
            InitializeComponent();
            InitializePictureBoxArray();
            playerTurn = enPlayerTurn.Player1;
        }

        private void InitializePictureBoxArray()
        {
            this.pictureBoxes = new PictureBox[3, 3]
            {
                { pictureBox1, pictureBox2, pictureBox3 },
                { pictureBox4, pictureBox5, pictureBox6 },
                { pictureBox7, pictureBox8, pictureBox9 }
            };
        }

        void ChangePlayerTurn()
        {
            if (playerTurn == enPlayerTurn.Player1)
            {
                playerTurn = enPlayerTurn.Player2;
                lblPlayerTurn.Text = "Player 2";
            }
            else
            {
                playerTurn = enPlayerTurn.Player1;
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

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox = sender as PictureBox;
            if (playerTurn == enPlayerTurn.Player1 && pictureBox.Enabled)
            {
                pictureBox.Image = Properties.Resources.x;
                pictureBox.BackgroundImage = null;
            }
            else
            {
                pictureBox.Image = Properties.Resources.o;
                pictureBox.BackgroundImage = null;
            }
            pictureBox.Enabled = false;

            if (IsThereAWinner())
            {
                switch (playerTurn)
                {
                    case enPlayerTurn.Player1:
                        lblWinner.Text = "Player 1!";
                        break;
                    case enPlayerTurn.Player2:
                        lblWinner.Text = "Player 2!";
                        break;
                    default:
                        break;
                }
                MessageBox.Show("Player " + (playerTurn == enPlayerTurn.Player1 ? "1" : "2") + " wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
            }
            else if (AllBoxesClicked())
            {
                lblWinner.Text = "Draw !";
                MessageBox.Show("Draw!","Winner!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
            }
            else
            {
                ChangePlayerTurn();
            }

        }

        private bool AllBoxesClicked()
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox)
                {
                    PictureBox pictureBox = (PictureBox)control;
                    if (pictureBox.Enabled)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        void ResetGame()
        {
            playerTurn = enPlayerTurn.Player1;
            lblPlayerTurn.Text = "Player 1";
            lblWinner.Text = "In Progress...";
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox)
                {
                    PictureBox pictureBox = (PictureBox)control;
                    pictureBox.Enabled = true;
                    pictureBox.Image = null;
                    pictureBox.BackgroundImage= Properties.Resources.question_mark2;

                }
            }
        }

        private bool IsThereAWinner()
        {
            // Horizontal check
            for (int row = 0; row < 3; row++)
            {
                if (CheckRowForWinner(row))
                {  
                    return true;
                }
            }

            // Vertical check
            for (int col = 0; col < 3; col++)
            {
                if (CheckColumnForWinner(col))
                {
                    return true;
                }
            }

            // Diagonal check
            if (CheckDiagonalForWinner())
            {
                return true;
            }

            // No winner found
            return false;
        }

        private bool CheckRowForWinner(int row)
        {

            return (AreImagesIdentical(pictureBoxes[row,0].Image, pictureBoxes[row, 1].Image) &&
                    AreImagesIdentical(pictureBoxes[row, 1].Image, pictureBoxes[row, 2].Image));
        }

        private bool CheckColumnForWinner(int col)
        {
           
            return (AreImagesIdentical(pictureBoxes[0, col].Image, pictureBoxes[1, col].Image) &&
                    AreImagesIdentical(pictureBoxes[1, col].Image, pictureBoxes[2, col].Image));
        }

        private bool CheckDiagonalForWinner()
        {
            // Check main diagonal
            if (AreImagesIdentical(pictureBoxes[0, 0].Image, pictureBoxes[1, 1].Image) &&
                AreImagesIdentical(pictureBoxes[1, 1].Image, pictureBoxes[2, 2].Image))
            {
                return true;
               
            }

            // Check anti-diagonal
            if (AreImagesIdentical(pictureBoxes[0, 2].Image, pictureBoxes[1, 1].Image) &&
                AreImagesIdentical(pictureBoxes[1, 1].Image, pictureBoxes[2, 0].Image))
            {
                return true;
               
            }

            return false;
        }


        private bool AreImagesIdentical(Image img1, Image img2)
        {
            if (img1 == null || img2 == null)
                return false;

            Bitmap bmp1 = new Bitmap(img1);
            Bitmap bmp2 = new Bitmap(img2);

            if (bmp1.Size != bmp2.Size)
                return false;

            for (int x = 0; x < bmp1.Width; x++)
            {
                for (int y = 0; y < bmp1.Height; y++)
                {
                    if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                        return false;
                }
            }

            return true;
        }
    }
}
