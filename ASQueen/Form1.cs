using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace ASQueen
{
    public partial class NQueen : Form
    {
        public NQueen()
        {
            InitializeComponent();
            pnlChess.Visible = false; 
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            Solve(); 
        }

        private void Solve()
        {
            try
            {
                pnlChess.Visible = false;
                pnlChess.Controls.Clear();
                if (txtN.Text == "")
                {
                    MessageBox.Show("Enter a valid number");
                    return;
                }
                var numberOfQueens = int.Parse(txtN.Text);
                if (numberOfQueens < 4)
                {
                    MessageBox.Show("Enter a number greater than or equals to 4");
                    return;
                }
                var board = new AStarBoard(numberOfQueens);
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                var result = AStarSearch(board);
                stopWatch.Stop();
                if (result != null)
                {
                    pnlChess.Visible = true;
                    result.ShowBoard(pnlChess);
                }
                else
                {
                    MessageBox.Show("No Result");
                }
            }
            catch
            {
                MessageBox.Show("Error occured while trying to solve the NQueen Problem.");
            }
        }
        AStarBoard AStarSearch(AStarBoard board)
        {
            if (board.IsGoal())
            {
                return board;
            }
            else
            {
                for (var i = 0; i < board.NumberOfQueens; i++)
                {
                    if (board.IsSafe(i))
                    {
                        board.Place(i);
                        var result = AStarSearch(board);
                        if (result != null)
                            return result;
                        board.UnPlace();
                    }
                }

            }
            return null;
        }

        private void txtN_MouseClick(object sender, MouseEventArgs e)
        {
            var textbox = (TextBox) sender;
            if(textbox.Text.Equals("Enter Number of Queens"))
                textbox.Clear();
            else
            {
                return;
            }
        }

        private void NQueen_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int) Keys.Enter)
            {
                Solve();
            }
        }
    }
}
