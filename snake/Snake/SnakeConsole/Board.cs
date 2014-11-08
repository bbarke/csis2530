using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    class Board
    {
        
        private int[,] board;

        public Board(int totalRow, int totalCol)
        {
            board = new int[totalRow, totalCol];
        }

        public void PrintBoard()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    Console.Write(board[row,col]);
                }
                Console.WriteLine();
            }
        }
    }
}
