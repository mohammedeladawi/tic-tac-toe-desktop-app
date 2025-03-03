using mySolution.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mySolution
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color White = Color.FromArgb(255, 255, 255, 255);
            Pen Pen = new Pen(White);
            Pen.Width = 10;

            Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            // Vertical Lines
            e.Graphics.DrawLine(Pen, 400, 150, 400, 350);
            e.Graphics.DrawLine(Pen, 500, 150, 500, 350);

            // Horizontal Lines
            e.Graphics.DrawLine(Pen, 350, 200, 550, 200);
            e.Graphics.DrawLine(Pen, 350, 300, 550, 300);

        }

        string currentPlayer;
        string winner;

        Dictionary<string, short> scores = new Dictionary<string, short>
        {
            { "Player1", 0 },
            { "Player2", 0 }
        };

        bool isClickedBefore(PictureBox sender)
        {
            return sender.Tag.ToString() == "clicked";
        }

        void addScoreToPlayer(short score)
        {
            scores[currentPlayer] += score;
        }

        void changeImageAndTag(PictureBox sender)
        {
            switch(currentPlayer)
            {
                case "Player1":
                    sender.Image = Resources.X;
                    sender.Tag = "clicked";
                    break;

                case "Player2":
                    sender.Image = Resources.O;
                    sender.Tag = "clicked";
                    break;
            }
        }

        bool isThereAWinner()
        {
            short[] winnerProbabilities = { 73, 146, 292, 7, 56, 448, 273, 84 };

            for (byte i = 0; i < winnerProbabilities.Length; i++)
            {
                if ((scores[currentPlayer] & winnerProbabilities[i]) == winnerProbabilities[i])
                {
                    // end game 
                    winner = currentPlayer;
                    return true;
                }
            }
            return false;
        }
        
        bool isDraw()
        {
            
            if ((scores["Player1"] + scores["Player2"]) == 511)
            {
                winner = "Draw";
                return true;
            }

            return false;
        }

        bool isGameFinished()
        {
            if (isThereAWinner() || isDraw())
                return true;
            
            return false;
        }
       
        void disableAllPBs()
        {
            pictureBox1.Enabled = false;
            pictureBox2.Enabled = false;
            pictureBox3.Enabled = false;
            pictureBox4.Enabled = false;
            pictureBox5.Enabled = false;
            pictureBox6.Enabled = false;
            pictureBox7.Enabled = false;
            pictureBox8.Enabled = false;
            pictureBox9.Enabled = false;

        }

        void endGame()
        {
            currentPlayer = "Game Over";
            lblGameStatus.Text = winner;
            lblPlayerTurn.Text = currentPlayer;
            disableAllPBs();
            MessageBox.Show("Game Over", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void handleClickedPB(PictureBox sender)
        {
            // check if it is clicked before
            if (isClickedBefore(sender))
            {
                MessageBox.Show("Already Clicked", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // add its score to player
            short score = Convert.ToInt16(sender.Tag);
            addScoreToPlayer(score);

            // change states
            changeImageAndTag(sender);

            // check if there is a winner
            if (isGameFinished())
            {
                endGame();
            }
            else
            {
                // change turn
                currentPlayer = (currentPlayer == "Player1") ? "Player2" : "Player1";
                lblPlayerTurn.Text = currentPlayer;
            }

        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            handleClickedPB((PictureBox)sender);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            handleClickedPB((PictureBox)sender);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            handleClickedPB((PictureBox)sender);

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            handleClickedPB((PictureBox)sender);

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            handleClickedPB((PictureBox)sender);

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            handleClickedPB((PictureBox)sender);

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            handleClickedPB((PictureBox)sender);

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            handleClickedPB((PictureBox)sender);

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            handleClickedPB((PictureBox)sender);

        }

        void pbInitialization(PictureBox pb, short tagVal)
        {
            pb.Tag = tagVal;
            pb.Enabled = true;
            pb.Image = Resources.question_mark_96;
        }

        void initializePb()
        {
            pbInitialization(pictureBox1, 1);
            pbInitialization(pictureBox2, 2);
            pbInitialization(pictureBox3, 4);
            pbInitialization(pictureBox4, 8);
            pbInitialization(pictureBox5, 16);
            pbInitialization(pictureBox6, 32);
            pbInitialization(pictureBox7, 64);
            pbInitialization(pictureBox8, 128);
            pbInitialization(pictureBox9, 256);

        }
        
        void startGame()
        {
            scores["Player1"] = 0;
            scores["Player2"] = 0;
            currentPlayer = "Player1";
            lblPlayerTurn.Text = currentPlayer;

            winner = "In Progress";
            lblGameStatus.Text = winner;
            initializePb();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            startGame();
        }
      
        private void btnRestart_Click(object sender, EventArgs e)
        {
            startGame();
        }
    }
}
