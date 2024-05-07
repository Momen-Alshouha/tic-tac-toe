using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tic_tac_toe
{
    public partial class TicTacToe : Form
    {
        public TicTacToe()
        {
            InitializeComponent();
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

    }
}
