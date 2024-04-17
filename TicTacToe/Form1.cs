using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        // If turn = true, it is X's turn; If turn = false, it is O's turn
        int turnCount = 0;
        Random random = new Random();
        bool playerTurn = true;
        string playerSign, cpuSign;

        public Form1()
        {
            InitializeComponent();
            decidePlayer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void decidePlayer()
        {
            // Randomly decide who starts: 0 for player, 1 for computer
            int firstPlayer = random.Next(2);
            playerTurn = firstPlayer == 0;


            // Assign markers based on who goes first
            playerSign = playerTurn ? "X" : "O";

            cpuSign = playerTurn ? "O" : "X";


            if (!playerTurn)
            {
                cpuMove(); // If computer is decided to move first, it makes a move
            }
        }

        private void btnClick(object sender, EventArgs e)
        {
            if (!playerTurn) return;
            Button square = (Button)sender;
            square.Text = playerSign;
            turnCount++;
            square.Enabled = false;
            // square.BackColor = Color.FromArgb(240, 240, 240); // Change background color
            // square.ForeColor = Color.Black;
            checkWinner();
            playerTurn = false;

            if (turnCount < 9)
            {
                cpuMove();
            }
        }

        private void cpuMove()
        {
            List<Button> buttons = new List<Button> {btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };
            List<Button> availableButtons = buttons.Where(btn => btn.Enabled).ToList();
            if (availableButtons.Count > 0)
            {
                int index = random.Next(availableButtons.Count);
                Button b = availableButtons[index];
                b.Text = cpuSign; // Assign marker based on computer's turn
                b.Enabled = false;
                turnCount++;
                checkWinner();
                playerTurn = true; // Switch turn back to player
            }
        }

        private void checkWinner()
        {
            bool winnerStatus = false;
            
            // Horizontal
            if ((btn1.Text == btn2.Text) && (btn2.Text == btn3.Text) && !btn1.Enabled)
            {
                winnerStatus = true;
            }
            else if ((btn4.Text == btn5.Text) && (btn5.Text == btn6.Text) && !btn4.Enabled)
            {
                winnerStatus = true;
            }
            else if ((btn7.Text == btn8.Text) && (btn8.Text == btn9.Text) && !btn7.Enabled)
            {
                winnerStatus = true;
            }

            // Vertical
            else if ((btn1.Text == btn4.Text) && (btn4.Text == btn7.Text) && !btn1.Enabled)
            {
                winnerStatus = true;
            }
            else if ((btn2.Text == btn5.Text) && (btn5.Text == btn8.Text) && !btn2.Enabled)
            {
                winnerStatus = true;
            }
            else if ((btn3.Text == btn6.Text) && (btn6.Text == btn9.Text) && !btn3.Enabled)
            {
                winnerStatus = true;
            }
            // Diagonal
            else if ((btn1.Text == btn5.Text) && (btn5.Text == btn9.Text) && !btn1.Enabled)
            {
                winnerStatus = true;
            }
            else if ((btn3.Text == btn5.Text) && (btn5.Text == btn7.Text) && !btn3.Enabled)
            {
                winnerStatus = true;
            }

            if (winnerStatus)
            {
                disableButtons();
                string winner = !playerTurn ? cpuSign : playerSign;
                status.Text = ($"{winner} Wins!");
                btn1.Enabled = false;
                btn2.Enabled = false;
                btn3.Enabled = false;
                btn4.Enabled = false;
                btn5.Enabled = false;
                btn6.Enabled = false;
                btn7.Enabled = false;
                btn8.Enabled = false;
                btn9.Enabled = false;
            }
            else if (turnCount == 9)
            {
                status.Text = "Tie!";
            }
        }

        private void disableButtons()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button square = (Button)c;
                    square.Enabled = false;
                }
            }
            catch { }
        }

        private void newGameBtn_Click(object sender, EventArgs e)
        {
            playerTurn = true;
            turnCount = 0;

            List<Button> buttons = new List<Button> {btn1, btn2, btn3,
                btn4, btn5, btn6, btn7, btn8, btn9 };
            foreach (Button b in buttons)
            {
                b.Enabled = true;
                b.Text = "";
                status.Text = "";
            }
            decidePlayer();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
