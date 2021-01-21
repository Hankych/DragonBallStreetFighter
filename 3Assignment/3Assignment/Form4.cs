/*Hank Yang
 * January 22, 2018
 * This is a dragon ball street fight game 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3Assignment
{
    public partial class Form4 : Form
    {
        // background information
        PointF backgroundLocation = new PointF(0, 0);
        SizeF backgroundSize = new SizeF(1063, 581);
        RectangleF backgroundBox;

        // text information
        const string LOSE_STRING = "You Lost!";
        PointF loseLocation = new PointF(120, 80);
        SizeF loseSize = new SizeF(4000, 4000);
        RectangleF loseBox;
        Font loseFont = new Font("Gill Sans Ultra Bold", 48);

        public Form4()
        {
            InitializeComponent();
            // update boxes information
            backgroundBox = new RectangleF(backgroundLocation, backgroundSize);
            loseBox = new RectangleF(loseLocation, loseSize);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // draw background
            e.Graphics.DrawImage(Properties.Resources.LosePage, backgroundBox);
            // show text
            e.Graphics.DrawString(LOSE_STRING, loseFont, Brushes.Black, loseBox);
        }
        // restart button
        private void btnRestart_Click(object sender, EventArgs e)
        {
            // go back to form 1
            Form1 frmInstruction = new Form1();
            frmInstruction.Show();
            this.Hide();
        }
    }
}
