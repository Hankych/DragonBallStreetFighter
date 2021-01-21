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
    public partial class Form1 : Form
    {
        // background information
        PointF backgroundLocation = new PointF (0,0);
        SizeF backgroundSize = new SizeF (1063, 581);
        RectangleF backgroundBox;

        // logo information
        PointF logoLocation = new PointF (320,10);
        SizeF logoSize = new SizeF (450, 100);
        RectangleF logoBox;

        // text in string type
        const string TXT1_STRING = "W,A,S,D for movements";
        const string TXT2_STRING = "J: Punch";
        const string TXT3_STRING = "K: Block";
        const string TXT4_STRING = "Space: Ki blast";

        // text information
        PointF txt1Location = new PointF (160,160), txt2Location = new PointF (160,220), txt3Location = new PointF (160,280), txt4Location = new PointF (160, 340);
        SizeF txtSize = new SizeF(400, 400);
        RectangleF txt1Box, txt2Box, txt3Box, txt4Box;
        Font txtFont = new Font("Garamond", 24);

        // Start the game
        private void btnStart_Click(object sender, EventArgs e)
        {
            // go to form 2
            Form2 frmGame = new Form2();
            frmGame.Show();
            this.Hide();
        }

        public Form1()
        {
            InitializeComponent();
            // update box information
            backgroundBox = new RectangleF(backgroundLocation, backgroundSize);
            logoBox = new RectangleF(logoLocation, logoSize);
            txt1Box = new RectangleF(txt1Location, txtSize);
            txt2Box = new RectangleF(txt2Location, txtSize);
            txt3Box = new RectangleF(txt3Location, txtSize);
            txt4Box = new RectangleF(txt4Location, txtSize);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // draw background
            e.Graphics.DrawImage(Properties.Resources.InstructionPage, backgroundBox);
            // draw dragonball logo
            e.Graphics.DrawImage(Properties.Resources.DragonBallLogo, logoBox);
            // show text
            e.Graphics.DrawString(TXT1_STRING, txtFont, Brushes.Black, txt1Box);
            e.Graphics.DrawString(TXT2_STRING, txtFont, Brushes.Black, txt2Box);
            e.Graphics.DrawString(TXT3_STRING, txtFont, Brushes.Black, txt3Box);
            e.Graphics.DrawString(TXT4_STRING, txtFont, Brushes.Black, txt4Box);
        }
       
    }
    }

        

