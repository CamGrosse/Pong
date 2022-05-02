using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Pong
{
    public partial class Form1 : Form
    {

        Rectangle player1 = new Rectangle(10, 180, 10, 60);
        Rectangle player2 = new Rectangle(580, 180, 10, 60);
        Rectangle ball = new Rectangle(295, 195, 10, 10);
        Rectangle player1Net = new Rectangle(0, 170, 10, 90);
        Rectangle player2Net = new Rectangle(590, 170, 10, 90);

        Pen whitePen = new Pen(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);

        int player1Score = 0;
        int player2Score = 0;
        
        int playerSpeed = 4;
        int ballXSpeed = 6;
        int ballYSpeed = -6;
        

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush purpleBrush = new SolidBrush(Color.Purple);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        SoundPlayer deathPlayer = new SoundPlayer(Properties.Resources.Hl2_Rebel_Ragdoll485_573931361);
        SoundPlayer rightHook = new SoundPlayer(Properties.Resources.Right_Cross_SoundBible_com_1721311663);
        SoundPlayer upperCut = new SoundPlayer(Properties.Resources.Upper_Cut_SoundBible_com_1272257235);
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;


                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;

            }

        }

        private void gameEngine_Tick(object sender, EventArgs e)
        {

            //move ball
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;
            //move player 1
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }

            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += playerSpeed;
            }


            //move player 2
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }

            if (leftArrowDown == true && player2.X > 0)
            {
                player2.X -= playerSpeed;
            }

            if (rightArrowDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X += playerSpeed;
            }

          //check if ball hit top or bottom wall and change direction if it
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
            }
            if (ball.X < 0 || ball.X > this.Width - ball.Width)
            {
                ballXSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
            }

            //check if ball hits either player. If it does change the direction
            //and place the ball in front of the player hit
            if (player1.IntersectsWith(ball))
            {
                ballXSpeed *= -1;
                //ballXSpeed *= +1;


                rightHook.Play();
                // if the ball is moving left
                if (ballXSpeed > 0)
                {
                    ball.X = player1.X + ball.Width;
                }
                // else
                // place the ball on the left side of the paddle.
                else 
                    { ball.X = player1.X - ball.Width;
                }
            }
            else if (player2.IntersectsWith(ball))
            {
                ballXSpeed *= -1;
                upperCut.Play();

                //ballXSpeed *= +1;
                if (ballXSpeed > 0)
                {
                    ball.X = player2.X + ball.Width;
                }
                // else
                // place the ball on the left side of the paddle.
                else
                {
                    ball.X = player2.X - ball.Width;
                }
            }

            //check if a player missed the ball and if true add 1 to score of other player 
            if (player1Net.IntersectsWith(ball))
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";

                ball.X = 295;
                ball.Y = 195;

                player1.Y = 180;
                player1.X = 10;
                player2.Y = 180;
                player2.X = 580;
                deathPlayer.Play();

            }
            else if (player2Net.IntersectsWith(ball))
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                ball.X = 295;
                ball.Y = 195;

                player1.Y = 180;
                player1.X = 10;
                player2.Y = 180;
                player2.X = 580;
                deathPlayer.Play(); 
            }

               //body check
              if(player1.IntersectsWith(player2))
            {
                player2.X = 580;
                player2.Y = 180;
                player1.X = 10;
                player1.Y = 180;
            }
              //goailes crease
              if(player1.IntersectsWith(player2Net))
            {
                player1.X = 10;
                player1.Y = 180;
            }
            else if (player2.IntersectsWith(player1Net))
            {
                player2.X = 580;
                player2.Y = 180;
            }
              if (player1.IntersectsWith(player1Net))
              
                {
                    player1.X = 10;
                    player1.Y = 180;
                }
            if (player2.IntersectsWith(player2Net))

            {
                player2.X = 580;
                player2.Y = 180;
            }
            // check score and stop game if either player is at 3
            if (player1Score == 3)
            {
                gameEngine.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";
            }
            else if (player2Score == 3)
            {
                gameEngine.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";

            }


            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
            e.Graphics.FillRectangle(redBrush, player1Net);
            e.Graphics.FillRectangle(redBrush, player2Net);
            e.Graphics.FillEllipse(whiteBrush, ball);

        }
    }
}
