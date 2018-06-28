using System;
using System.Drawing;
using System.Windows.Forms;

namespace ASQueen
{
    public class AStarBoard
    {
        public int NumberOfQueens;
        private readonly int[] _board;
        private int _counter;

        public AStarBoard(int numberOfQueens)
        {
            NumberOfQueens = numberOfQueens;
            _board = new int[NumberOfQueens];
            InitializeBoard();
            _counter = 0;
        }

        private void InitializeBoard()
        {
            for (var i = 0; i < NumberOfQueens; i++)
                _board[i] = 0;
        }

        public bool IsSafe(int column)
        {
            for (var i = 0; i < _counter; i++)
            {
                if ((_board[i] == column) || Math.Abs(column - _board[i]) == (_counter - i))
                    return false;
            }
            return true;
        }
        public void Place(int column)
        {
            if (column >= 0 && column < NumberOfQueens)
            {
                _board[_counter] = column;
                _counter++;
            }
            else
            {
                MessageBox.Show("Bad Column");
            }
        }
        public bool IsGoal()
        {
            return _counter == NumberOfQueens;
        }
        public void UnPlace()
        {
            if (_counter > 0)
            {
                _counter--;
            }
        }
        public void ShowBoard(Panel panel)
        {
            panel.Controls.Clear();
            var chess = new PictureBox[NumberOfQueens, NumberOfQueens];
            int width = 0, height = 0;
            width = panel.Width;
            height = panel.Height;
            var xaxis = (int)(width / (double)NumberOfQueens);
            var yaxis = (int)(height / (double)NumberOfQueens);
            for (var row = 0; row < NumberOfQueens; row++)
            {
                for (var column = 0; column < NumberOfQueens; column++)
                {
                    chess[row, column] = new PictureBox
                    {
                        BorderStyle = BorderStyle.FixedSingle,
                        Parent = panel,
                        Location = new Point(column * xaxis + 1, row * yaxis + 1),
                        Size = new Size(xaxis, yaxis),
                        BackColor = (row + column) % 2 == 0 ? Color.White : Color.Black
                    };
                }
            }
            for (var i = 0; i < _counter; i++)
            {
                chess[i, _board[i]].Load(Application.StartupPath + "\\queenCapture.png");
                chess[i, _board[i]].SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}