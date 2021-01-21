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
    public partial class Form3 : Form
    {
        // background' information
        PointF backgroundLocation = new PointF(0, 0);
        SizeF backgroundSize = new SizeF(1063, 581);
        RectangleF backgroundBox;

        // text information
        const string WIN_STRING = "You Won!";
        PointF winLocation = new PointF (120,80);
        SizeF winSize = new SizeF(4000, 4000);
        RectangleF winBox;
        Font winFont = new Font("Gill Sans Ultra Bold", 48);
        public Form3()
        {
            InitializeComponent();
            // update boxes's information
            backgroundBox = new RectangleF(backgroundLocation, backgroundSize);
            winBox = new RectangleF(winLocation, winSize);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // draw background
            e.Graphics.DrawImage(Properties.Resources.WinPage, backgroundBox);
            // show text
            e.Graphics.DrawString(WIN_STRING, winFont, Brushes.White, winBox);
        }
        // play again button
        private void btnPlayAgain_Click(object sender, EventArgs e)
        {
            // go back to form 1
            Form1 frmInstruction = new Form1();
            frmInstruction.Show();
            this.Hide();
        }
    }
}
