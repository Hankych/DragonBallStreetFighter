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
    public partial class Form2 : Form
    {
        // loop conditions
        bool isGameRunning = false, gameEnded = false;
        // movements
        bool stand = true, moveRight, moveLeft, punch, moveUp, moveDown, block, kiBlast;
        // Check who won
        bool enemyAlive = true, playerAlive = true, enemyStand = true;
        // player locaiton
        int x, y;
        // Hp information
        int playerHp = 100, playerMaxHp = 100, enemyHp = 100, enemyMaxHp = 100;
        // damage for different moves
        int enemyKiDamage = 10, playerKiDamage = 10, playerPunchDamage = 20;
        
        // player information
        PointF playerLocation, playerDeadLocation, backgroundLocation;
        SizeF playerSize, playerDeadSize, backgroundSize;
        RectangleF playerBox,playerDeadBox, backgroundBox;

        // enemy information
        PointF enemyLocation, enemyDeadLocation;
        SizeF enemySize, enemyDeadSize;
        RectangleF enemyBox, enemyDeadBox;
        int enemySpeedY = 30; // enemy moving speed

        // Goku's icon information
        PointF GokuIconLocation;
        SizeF GokuIconSize;
        RectangleF GokuIconBox;

        // Buu's icon information
        PointF BuuIconLocation;
        SizeF BuuIconSize;
        RectangleF BuuIconBox;

        // Obstacle information
        PointF dragonBallLocation;
        SizeF dragonBallSize;
        RectangleF dragonBallBox;

        // timer information
        PointF timerLocation = new PointF (500, 60);
        SizeF timerSize = new SizeF (400, 400);
        RectangleF timerBox;
        Font timerFont;

        // dragonball logo information
        PointF logoLocation = new PointF (426, 12);
        SizeF logoSize = new SizeF (226, 50);
        RectangleF logoBox;
        

        // the projectile's information
        RectangleF projectileBox;
        SizeF projectileSize;
        PointF projectileLocation;

        // text information
        PointF playerNameLocation = new PointF(193, 28), enemyNameLocation = new PointF(736, 28);
        SizeF playerNameSize = new SizeF (400, 400), enemyNameSize = new SizeF(400, 400);
        RectangleF playerNameBox, enemyNameBox;
        Font nameFont = new Font("Impact", 24);
        const string PLAYERNAME_STRING = "Son Goku";
        const string ENEMYNAME_STRING = "Majin Buu";

        // Player's projectile information
        float projectileX, projectileY;
        float projectileXSpeed, projectileYSpeed;
        float rise, run;
        float hypotenuse;
        const int PROJECTILE_SPEED = 30;
        bool isProjectileInMotion = false;

        // the enemy's projectile's information
        RectangleF projectile2Box;
        SizeF projectile2Size;
        PointF projectile2Location;
        float projectile2X, projectile2Y;
        float projectile2XSpeed, projectile2YSpeed;
        float rise2, run2;
        float hypotenuse2;
        const int PROJECTILE2_SPEED = 30;
        bool isProjectile2InMotion = false;

        // for loop's time
        int lastRunTime, lastRunTime2, lastRunTime3;
        int timerInterval = 25, timerInterval2 = 25, timerInterval3 = 1000;
        // animation frame count
        int animationFrameCount = 0;
        int animationFrameCount2 = 0;
        int animationFrameCount3 = 0;
        int animationFrameCount4 = 0;
        int timer = 99;
        public Form2()
        {
            InitializeComponent();
            // set up all the pics
            SetupGraphics();
        }
        void GameTimer()
        {
            isGameRunning = true;
            lastRunTime = Environment.TickCount;

            // run every 25 ms
            while (isGameRunning == true)
            {
                // control the interval
                if (Environment.TickCount - lastRunTime >= timerInterval)
                {
                    lastRunTime = Environment.TickCount;
                    // move the player and check boundaries
                    if (moveRight == true)
                    {
                        x = x + 30;
                        playerBox.Location = new PointF(x, y);
                        CheckRightBoundary();
                        CheckBallLeftBoundary();
                    }
                    else if (moveLeft == true)
                    {
                        x = x - 30;
                        playerBox.Location = new PointF(x, y);
                        CheckLeftBoundary();
                        CheckBallRightBoundary();
                    }
                    else if (moveUp == true)
                    {
                        y = y - 30;
                        playerBox.Location = new PointF(x, y);
                        CheckTopBoundary();
                        CheckBallBottomBoundary();
                    }
                    else if (moveDown == true)
                    {
                        y = y + 30;
                        playerBox.Location = new PointF(x, y);
                        CheckBottomBoundary();
                        CheckBallTopBoundary();
                    }
                    // calculate player punch damage
                    else if (punch == true && playerBox.IntersectsWith(enemyBox))
                    {
                        enemyHp = enemyHp - playerPunchDamage;
                        progressBar2.Value = (int)((double)enemyHp / enemyMaxHp * 100);

                    }
                    // The programs that need to be running reapeatily
                    CheckEnemyHp();
                    CheckPlayerHp();
                    ControlAnimation();
                    MovePrjectile();
                    CreateNewProjectile2();
                    MoveProjectile2();
                    MoveEnemy();
                    enemyKiDamage = 10;
                    playerLocation = new PointF(x, y);
                    Refresh();
                }
                // loop for timer
                if (Environment.TickCount - lastRunTime3 >= timerInterval3)
                {
                    lastRunTime3 = Environment.TickCount;
                    if (timer >= 1)
                    {
                        timer--;
                    }
                    else if (timer <= 0)
                    {
                        // stop everything
                        isGameRunning = false;
                        gameEnded = true;
                        isProjectile2InMotion = false;
                        isProjectile2InMotion = false;
                        enemyStand = true;
                        kiBlast = false;
                        // change form
                        Form4 frmLose = new Form4();
                        frmLose.Show();
                        this.Hide();
                    }
                    Refresh();
                }
                // stop program from freezing
                Application.DoEvents();
            }
        }
        // to show the death animation after the game ended
        void GameTimer2()
        {
            lastRunTime2 = Environment.TickCount;
            while (gameEnded == true)
            {
                if (Environment.TickCount - lastRunTime2 >= timerInterval2)
                {
                    lastRunTime2 = Environment.TickCount;
                    ControlAnimation();
                    
                    Refresh();
                }
                // stop program from freezing
                Application.DoEvents();
            }
        }
      
        void ControlAnimation()
        {
            animationFrameCount++; // animationFrameCount = animationFrameCount +1;
            animationFrameCount2++;
            animationFrameCount3++;
            animationFrameCount4++;
            // reset the animation frame count when it reaches the end of the animation
            if (animationFrameCount == 20)
            {
                animationFrameCount = 0;
            }
            if (animationFrameCount2 == 18)
            {
                animationFrameCount2 = 0;
            }
            if (animationFrameCount3 == 30)
            {
                animationFrameCount3 = 0;
            }
            if (animationFrameCount4 == 500)
            {
                animationFrameCount4 = 0;
            }
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                if (isGameRunning == true)
                {
                    // shows stand animation after releasing keys
                    stand = true;
                    moveRight = false;
                    moveLeft = false;
                    punch = false;
                    moveUp = false;
                    moveDown = false;
                    block = false;
                    kiBlast = false;
                }
            }
            else if (e.KeyCode == Keys.A)
            {
                if (isGameRunning == true)
                {
                    // shows stand animation after releasing keys
                    stand = true;
                    moveRight = false;
                    moveLeft = false;
                    punch = false;
                    moveUp = false;
                    moveDown = false;
                    block = false;
                    kiBlast = false;
                }
            }
            else if (e.KeyCode == Keys.J)
            {
                if (isGameRunning == true)
                {
                    // shows stand animation after releasing keys
                    stand = true;
                    moveRight = false;
                    moveLeft = false;
                    punch = false;
                    moveUp = false;
                    moveDown = false;
                    block = false;
                    kiBlast = false;
                }
            }
           
            else if (e.KeyCode == Keys.W)
            {
                if (isGameRunning == true)
                {
                    // shows stand animation after releasing keys
                    stand = true;
                    moveRight = false;
                    moveLeft = false;
                    punch = false;
                    moveUp = false;
                    moveDown = false;
                    block = false;
                    kiBlast = false;

                }
            }
            else if (e.KeyCode == Keys.S)
            {
                // shows stand animation after releasing keys
                stand = true;
                moveRight = false;
                moveLeft = false;
                punch = false;
                moveUp = false;
                moveDown = false;
                block = false;
                kiBlast = false;
            }
            else if (e.KeyCode == Keys.K)
            {
                // shows stand animation after releasing keys
                stand = true;
                moveRight = false;
                moveLeft = false;
                punch = false;
                moveUp = false;
                moveDown = false;
                block = false;
                kiBlast = false;
            }
            else if (e.KeyCode == Keys.Space)
            {
                // shows stand animation after releasing keys
                stand = true;
                moveRight = false;
                moveLeft = false;
                punch = false;
                moveUp = false;
                moveDown = false;
                block = false;
                kiBlast = false;
                
            }
        }

        void SetupGraphics()
        {
            // update the information of these boxes
            playerSize = new SizeF(80, 100);
            playerLocation = new PointF(120, 400);
            playerBox = new RectangleF(playerLocation, playerSize);
            x = 120;
            y = 430;

            backgroundSize = new SizeF(1063, 625);
            backgroundLocation = new PointF(0,0);
            backgroundBox = new RectangleF(backgroundLocation, backgroundSize);

            enemySize = new SizeF(70, 100);
            enemyLocation = new PointF(800, 400);
            enemyBox = new RectangleF(enemyLocation, enemySize);

            timerBox = new RectangleF(timerLocation, timerSize);
            timerFont = new Font ("Garamond", 36);

            projectileSize = new SizeF(100, 60);
            projectileBox = new RectangleF();
            projectileBox.Size = projectileSize;

            projectile2Size = new SizeF(100, 100);
            projectile2Box = new RectangleF();
            projectile2Box.Size = projectile2Size;

            GokuIconSize = new SizeF(130, 140);
            GokuIconLocation = new PointF(10, 10);
            GokuIconBox = new RectangleF(GokuIconLocation, GokuIconSize);

            BuuIconSize = new SizeF(120, 140);
            BuuIconLocation = new PointF(920, 10);
            BuuIconBox = new RectangleF(BuuIconLocation, BuuIconSize);

            dragonBallSize = new SizeF(120, 120);
            dragonBallLocation = new PointF(470, 300);
            dragonBallBox = new RectangleF(dragonBallLocation, dragonBallSize);

            logoBox = new RectangleF(logoLocation, logoSize);
            playerNameBox = new RectangleF(playerNameLocation, playerNameSize);
            enemyNameBox = new RectangleF(enemyNameLocation, enemyNameSize);
            
        }
        void CheckRightBoundary()
        {
            // check right boundary
            if (x > this.ClientSize.Width - playerSize.Width)
            {
                x = this.ClientSize.Width -(int)playerSize.Width;
            }

        }
        void CheckLeftBoundary()
        {
            // check left boundary
            if (x < 0)
            {
                x = 0;
            }
        }

        void CheckTopBoundary()
        {
            // check top boundary
            if (y < 0)
            {
                y = 0;
            }
        }

        void CheckBottomBoundary()
        {
            // check bottom boundary
            if (y > this.ClientSize.Height - playerSize.Height)
            {
                y = this.ClientSize.Height - (int)playerSize.Height;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // draw background
            e.Graphics.DrawImage(Properties.Resources.DragonBallBackground, backgroundBox);
            // draw logo
            e.Graphics.DrawImage(Properties.Resources.DragonBallLogo, logoBox);
            // draw goku icon
            e.Graphics.DrawImage(Properties.Resources.GokuIcon, GokuIconBox);
            // draw buu icon
            e.Graphics.DrawImage(Properties.Resources.BuuIcon, BuuIconBox);
            // draw timer
            e.Graphics.DrawString(timer.ToString(), timerFont, Brushes.Black, timerBox);
            // draw obstacle
            e.Graphics.DrawImage(Properties.Resources.DragonBallBall, dragonBallBox);
            // show player name
            e.Graphics.DrawString(PLAYERNAME_STRING , nameFont, Brushes.Black, playerNameBox);
            // show enemy name
            e.Graphics.DrawString(ENEMYNAME_STRING, nameFont, Brushes.Black, enemyNameBox);
            // draw enemy's standing pic
            if (enemyStand == true)
            {
                e.Graphics.DrawImage(Properties.Resources.BuuStand, enemyBox);
            }
            // draw player's standing pic
            if (stand == true && playerAlive == true)
            {
                e.Graphics.DrawImage(Properties.Resources.GokuStand, playerBox);
                moveRight = false;
                moveLeft = false;
                punch = false;
                moveUp = false;
                moveDown = false;
                block = false;
                kiBlast = false;

            }
            // move right animation
            else if (moveRight == true)
            {
                
                if (animationFrameCount >= 0 && animationFrameCount < 5)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunRight1, playerBox);
                }
                else if (animationFrameCount >= 5 && animationFrameCount < 10)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunRight2, playerBox);
                }
                else if (animationFrameCount >= 10 && animationFrameCount < 15)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunRight3, playerBox);
                }
                else if (animationFrameCount >= 15 && animationFrameCount < 20)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunRight4, playerBox);
                }
                
            }
            // move left animatoin
            else if (moveLeft == true)
            {
                if (animationFrameCount >= 0 && animationFrameCount < 5)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunLeft1, playerBox);
                }
                else if (animationFrameCount >= 5 && animationFrameCount < 10)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunLeft2, playerBox);
                }
                else if (animationFrameCount >= 10 && animationFrameCount < 15)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunLeft3, playerBox);
                }
                else if (animationFrameCount >= 15 && animationFrameCount < 20)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunLeft4, playerBox);
                }
                
            }
            // punch animation
            else if (punch == true)
            {
                if (animationFrameCount2 >= 0 && animationFrameCount2 < 3)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuPunch1, playerBox);
                }
                else if (animationFrameCount2 >= 3 && animationFrameCount2 < 6)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuPunch2, playerBox);
                }
                else if (animationFrameCount2 >= 6 && animationFrameCount2 < 9)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuPunch3, playerBox);
                }
                else if (animationFrameCount2 >= 9 && animationFrameCount2 < 12)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuPunch4, playerBox);
                }
                else if (animationFrameCount2 >= 12 && animationFrameCount2 < 15)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuPunch5, playerBox);
                }
                else if (animationFrameCount2 >= 15 && animationFrameCount2 < 18)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuPunch6, playerBox);
                }
            }
            // move up animation
            else if (moveUp == true)
            {

                if (animationFrameCount >= 0 && animationFrameCount < 5)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunRight1, playerBox);
                }
                else if (animationFrameCount >= 5 && animationFrameCount < 10)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunRight2, playerBox);
                }
                else if (animationFrameCount >= 10 && animationFrameCount < 15)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunRight3, playerBox);
                }
                else if (animationFrameCount >= 15 && animationFrameCount < 20)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunRight4, playerBox);
                }
            }
            // move down animation
            else if (moveDown == true)
            {
                if (animationFrameCount >= 0 && animationFrameCount < 5)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunRight1, playerBox);
                }
                else if (animationFrameCount >= 5 && animationFrameCount < 10)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunRight2, playerBox);
                }
                else if (animationFrameCount >= 10 && animationFrameCount < 15)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunRight3, playerBox);
                }
                else if (animationFrameCount >= 15 && animationFrameCount < 20)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuRunRight4, playerBox);
                }
            }
            //  block animation
            else if (block == true)
            {
                if (animationFrameCount2 >= 0 && animationFrameCount2 < 6)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuBlock1, playerBox);
                }
                else if (animationFrameCount2 >= 6 && animationFrameCount2 < 12)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuBlock2, playerBox);
                }
                else if (animationFrameCount2 >= 12 && animationFrameCount2 < 18)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuBlock3, playerBox);
                }
            }
            // ki blast animation
            else if (kiBlast == true)
            {
                if (animationFrameCount2 >= 0 && animationFrameCount2 < 3)
                {
                    e.Graphics.DrawImage(Properties.Resources.KiBlast1, playerBox);
                }
                else if (animationFrameCount2 >= 3 && animationFrameCount2 < 6)
                {
                    e.Graphics.DrawImage(Properties.Resources.KiBlast2, playerBox);
                }
                else if (animationFrameCount2 >= 6 && animationFrameCount2 < 9)
                {
                    e.Graphics.DrawImage(Properties.Resources.KiBlast3, playerBox);
                }
                else if (animationFrameCount2 >= 9 && animationFrameCount2 < 12)
                {
                    e.Graphics.DrawImage(Properties.Resources.KiBlast4, playerBox);
                }
                else if (animationFrameCount2 >= 12 && animationFrameCount2 < 15)
                {
                    e.Graphics.DrawImage(Properties.Resources.KiBlast5, playerBox);
                }
                else if (animationFrameCount2 >= 15 && animationFrameCount2 < 18)
                {
                    e.Graphics.DrawImage(Properties.Resources.KiBlast6, playerBox);
                }

            }
            // show enemy death animation
            if (enemyAlive == false)
            {

                if (animationFrameCount4 >= 0 && animationFrameCount4 < 150)
                {
                    e.Graphics.DrawImage(Properties.Resources.BuuDead1, enemyBox);
                }
                else if (animationFrameCount4 >= 150 && animationFrameCount4 < 300)
                {
                    enemyDeadSize = new SizeF(125, 87);
                    enemyDeadLocation = new PointF(enemyLocation.X, enemyLocation.Y + 38);
                    enemyDeadBox = new RectangleF(enemyDeadLocation, enemyDeadSize);
                    e.Graphics.DrawImage(Properties.Resources.BuuDead2, enemyDeadBox);
                }
                else if(animationFrameCount4 >= 300 && animationFrameCount4 < 500)
                {
                    // change form
                    Form3 frmWin = new Form3();
                    frmWin.Show();
                    this.Hide();
                }
            }
            // show player death animation
            if (playerAlive == false)
            {
                if (animationFrameCount4 >= 0 && animationFrameCount4 < 150)
                {
                    e.Graphics.DrawImage(Properties.Resources.GokuDead1, playerBox);
                }
                else if (animationFrameCount4 >= 150 && animationFrameCount4 < 300)
                {
                    playerDeadSize = new SizeF(125, 100);
                    playerDeadLocation = new PointF(playerLocation.X, playerLocation.Y + 25);
                    playerDeadBox = new RectangleF(playerDeadLocation, playerDeadSize);
                    e.Graphics.DrawImage(Properties.Resources.GokuDead2, playerDeadBox);
                }
                else if(animationFrameCount4 >= 300 && animationFrameCount4 < 500)
                {
                    // change form
                    Form4 frmLose = new Form4();
                    frmLose.Show();
                    this.Hide();
                }
            }
            // draw player's projectile
            if (isProjectileInMotion == true)
            {
                e.Graphics.DrawImage(Properties.Resources.Ki, projectileBox);
            }

            if (isProjectile2InMotion == true)
            {
                enemyStand = false;
                // draw enemy's projectile
                e.Graphics.DrawImage(Properties.Resources.BuuKi, projectile2Box);
                // enemy shoot projectile animation
                if (animationFrameCount >= 0 && animationFrameCount < 10)
                {
                    e.Graphics.DrawImage(Properties.Resources.BuuBlast1, enemyBox);
                }
                else if (animationFrameCount >= 10 && animationFrameCount < 20)
                {
                    e.Graphics.DrawImage(Properties.Resources.BuuBlast2, enemyBox);
                }
            }


        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            // starts game timer loop if any key is pressed
            if (isGameRunning == false && gameEnded == false)
            {
                GameTimer();
            }

            if (isGameRunning == true)
            {
                // pause game
                if (e.KeyCode == Keys.Escape)
                {
                    isGameRunning = false;
                }
                // move right
                else if (e.KeyCode == Keys.D)
                {
                    stand = false;
                    moveRight = true;
                    

                }
                // move left
                else if (e.KeyCode == Keys.A)
                {
                    stand = false;
                    moveLeft = true;
                }
                // punch
                else if (e.KeyCode == Keys.J)
                {
                    stand = false;
                    punch = true;

                }
                // move up
                else if (e.KeyCode == Keys.W)
                {
                    stand = false;
                    moveUp = true;

                }
                // move down
                else if (e.KeyCode == Keys.S)
                {
                    stand = false;
                    moveDown = true;
                }
                // block
                else if (e.KeyCode == Keys.K)
                {
                    stand = false;
                    block = true;

                }
                // shoot projectile
                else if (e.KeyCode == Keys.Space && isProjectileInMotion == false)
                {
                    stand = false;
                    kiBlast = true;
                    CreateNewProjectile();

                }
            }
        }

        void CreateNewProjectile()
        {
            if (isProjectileInMotion == false)
            {
                // get the starting location for the projectile to be the shotter's location
                projectileX = playerBox.X + 80;
                projectileY = playerBox.Y + 30;
                // create the start location for the projectile
                projectileLocation = new PointF(projectileX, projectileY);
                // move the projectile box to its start location
                projectileBox.Location = projectileLocation;
                isProjectileInMotion = true;

                // calculate the slope from the shooter to the target
                rise = enemyBox.Y - playerBox.Y;
                run = enemyBox.X - playerBox.X;
                hypotenuse = (float)Math.Sqrt(rise * rise + run * run);

                // calculate the speeds for the projectile using the slope vector
                projectileXSpeed = run / hypotenuse * PROJECTILE_SPEED;
                projectileYSpeed = rise / hypotenuse * PROJECTILE_SPEED;
            }
        }
        void MovePrjectile()
        {
            if (isProjectileInMotion == true)
            {
                // update projectile location
                projectileBox.X = projectileBox.X + projectileXSpeed;
                projectileBox.Y = projectileBox.Y + projectileYSpeed;
                projectileLocation = new PointF(projectileX, projectileY);
                // make projectile disapears if intersects with enemy, obstacle and the boundaries
                if (projectileBox.IntersectsWith(enemyBox))
                {
                    // stop projectile from showing
                    isProjectileInMotion = false;
                    // calculate enemy hp
                    enemyHp = enemyHp - playerKiDamage;
                    progressBar2.Value = (int)((double)enemyHp / enemyMaxHp * 100);
                }
                // checks if projectile intersects with boundaries
                if ( projectileBox.Y < 0 || projectileBox.X < 0 ||
                    projectileBox.Y > ClientSize.Height - projectileBox.Height ||
                    projectileBox.X > ClientSize.Width || projectileBox.IntersectsWith(dragonBallBox))
                {
                    isProjectileInMotion = false;
                }
                
            }
        }
        // create enemy's projectile
        void CreateNewProjectile2()
        {
            if (isProjectile2InMotion == false)
            {
                // projectile's location when shot
                projectile2X = enemyBox.X - 80;
                projectile2Y = enemyBox.Y + 30;
                // update enemy projectile's location
                projectile2Location = new PointF(projectile2X, projectile2Y);

                projectile2Box.Location = projectile2Location;
                isProjectile2InMotion = true;
                // calculate enemy projectile's speed
                rise2 = playerBox.Y - enemyBox.Y;
                run2 = playerBox.X - enemyBox.X;
                hypotenuse2 = (float)Math.Sqrt(rise2 * rise2 + run2 * run2);

                projectile2XSpeed = run2 / hypotenuse2 * PROJECTILE2_SPEED / 2  ;
                projectile2YSpeed = rise2 / hypotenuse2 * PROJECTILE2_SPEED / 2  ;
                // increases enemy projectile speed if enemy hp is less than 50%
                if (enemyHp <= 50)
                {
                    projectile2XSpeed = run2 / hypotenuse2 * PROJECTILE2_SPEED / 2 * 3;
                    projectile2YSpeed = rise2 / hypotenuse2 * PROJECTILE2_SPEED / 2 * 3;
                }
            }
        }

        void MoveProjectile2()
        {
            if (isProjectile2InMotion == true)
            {
                // update projectile location
                projectile2Box.X = projectile2Box.X + projectile2XSpeed;
                projectile2Box.Y = projectile2Box.Y + projectile2YSpeed;
                projectile2Location = new PointF(projectile2X, projectile2Y);
                if (projectile2Box.IntersectsWith(playerBox))
                {
                    // do no damage to player if player is blocking
                    if (block == true && projectile2Box.IntersectsWith(playerBox))
                    {
                        isProjectile2InMotion = false;
                        enemyKiDamage = 0;
                        playerHp = playerHp - enemyKiDamage;
                        
                    }
                    // calculate player hp
                    isProjectile2InMotion = false;
                    playerHp = playerHp - enemyKiDamage;
                    progressBar1.Value = (int)((double)playerHp / playerMaxHp * 100);
                }
                // enemy projectile disapears if intersects with boundaries
                if (projectile2Box.IntersectsWith(playerBox) || projectile2Box.Y < 0 ||
                    projectile2Box.Y > ClientSize.Height - projectile2Box.Height ||
                    projectile2Box.X > projectile2Box.Width + ClientSize.Width ||projectile2Box.X < 0
                    || projectile2Box.IntersectsWith(dragonBallBox))
                {
                    isProjectile2InMotion = false;
                }
                
            }
        }

        void MoveEnemy()
        {
            // moves enemy
            enemyLocation.Y = enemyLocation.Y + enemySpeedY;
            enemyLocation = new PointF (enemyLocation.X, enemyLocation.Y);
            enemyBox = new RectangleF(enemyLocation, enemySize);
            // check if enemy hits the right boundary -- trun around
            if (enemyLocation.Y >= this.ClientSize.Height - enemySize.Height || enemyLocation.Y <= 0)
            {
               enemySpeedY = -enemySpeedY;
            }
        }
        void CheckEnemyHp()
        {
            // enemy dies
            if (enemyHp <= 0)
            {
                // end games if enemy dies
                gameEnded = true;
                isGameRunning = false;
                enemyAlive = false;
                // stop other animation
                isProjectileInMotion = false;
                isProjectile2InMotion = false;
                enemyStand = false;
                stand = true;
                // show death animation
                GameTimer2();
            }
        }

        void CheckPlayerHp()
        {
            // player dies
            if (playerHp <= 0)
            {
                // end game
                gameEnded = true;
                isGameRunning = false;
                playerAlive = false;
                // stop other animation
                isProjectileInMotion = false;
                isProjectile2InMotion = false;
                enemyStand = true;
                // make sure other pics don't show during death animation
                stand = false;
                moveRight = false;
                moveLeft = false;
                moveUp = false;
                moveDown = false;
                punch = false;
                block = false;
                kiBlast = false;
                GameTimer2();
            }
        }
        // check obstacle's left boundary
        void CheckBallLeftBoundary()
        {
            // set all the boundary conditions
            if ((playerLocation.X + playerSize.Width  >= dragonBallLocation.X  
                && playerLocation.X + playerSize.Width <= dragonBallLocation.X + dragonBallSize.Width
                &&  dragonBallLocation.Y <= playerLocation.Y + playerSize.Height
                 && dragonBallLocation.Y >= playerLocation.Y) 
                 || (playerLocation.X + playerSize.Width >= dragonBallLocation.X
                 && playerLocation.X + playerSize.Width <= dragonBallLocation.X + dragonBallSize.Width
                 && playerLocation.Y >= dragonBallLocation.Y 
                 && playerLocation.Y  <= dragonBallLocation.Y + dragonBallSize.Height ))
            {
                // make player not pass through obstacle
                playerLocation.X = dragonBallLocation.X - playerSize.Width;
                x = (int)playerLocation.X;
                playerBox.Location = new PointF(x, y);
            }
        }
        // check obstacle's right boundary
        void CheckBallRightBoundary()
        {
            // set all the boundary conditions
            if ((playerLocation.X >= dragonBallLocation.X 
                && playerLocation.X <= dragonBallLocation.X + dragonBallSize.Width
                && dragonBallLocation.Y <= playerLocation.Y + playerSize.Height
                && dragonBallLocation.Y >= playerLocation.Y)
                || (playerLocation.X >= dragonBallLocation.X
                && playerLocation.X <= dragonBallLocation.X + dragonBallSize.Width
                && playerLocation.Y >= dragonBallLocation.Y
                && playerLocation.Y <= dragonBallLocation.Y + dragonBallSize.Height))
            {
                // make player not pass through obstacle
                playerLocation.X = dragonBallLocation.X + dragonBallSize.Width;
                x = (int)playerLocation.X;
                playerBox.Location = new PointF(x, y);
            }
        }
        // check obstacle's top boundary
        void CheckBallTopBoundary()
        {
            // set all the boundary conditions
            if ((playerLocation.Y + playerSize.Height >= dragonBallLocation.Y
                && playerLocation.Y + playerSize.Height <= dragonBallLocation.Y + dragonBallSize.Height
                && playerLocation.X <= dragonBallLocation.X + dragonBallSize.Width
                && playerLocation.X >= dragonBallLocation.X)
                || (playerLocation.Y + playerSize.Height >= dragonBallLocation.Y
                && playerLocation.Y + playerSize.Height <= dragonBallLocation.Y + dragonBallSize.Height
                && playerLocation.X + playerSize.Width >= dragonBallLocation.X
                && playerLocation.X + playerSize.Width <= dragonBallLocation.X + dragonBallSize.Width))
            {
                // make player not pass through obstacle
                playerLocation.Y = dragonBallLocation.Y - playerSize.Height;
                y = (int)playerLocation.Y;
                playerBox.Location = new PointF(x, y);
            }
        }
        // check obstacle's bottom boundary
        void CheckBallBottomBoundary()
        {
            // set all the boundary conditions
            if ((playerLocation.Y <= dragonBallLocation.Y + dragonBallSize.Height
                && playerLocation.Y >= dragonBallLocation.Y
                 && playerLocation.X <= dragonBallLocation.X + dragonBallSize.Width
                && playerLocation.X >= dragonBallLocation.X)
                || (playerLocation.Y <= dragonBallLocation.Y + dragonBallSize.Height
                && playerLocation.Y >= dragonBallLocation.Y
                && playerLocation.X + playerSize.Width >= dragonBallLocation.X
                && playerLocation.X + playerSize.Width <= dragonBallLocation.X + dragonBallSize.Width))
            {
                // make player not pass through obstacle
                playerLocation.Y = dragonBallLocation.Y + playerSize.Height;
                y = (int)playerLocation.Y;
                playerBox.Location = new PointF(x, y);
            }
        }
    }
}
