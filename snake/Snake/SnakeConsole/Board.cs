using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    // Snake enums
    public enum SnakePiece { Space = 0, Wall, Head, Apple, Bomb }

    class Board
    {
        private SnakePiece[,] board;
        private int totalRow;
        private int totalCol;
        public bool HasCrashed { get; private set; }
        private LinkedSnake snake;

        // sets game board to any numbers of row and column
        public Board(int totalRow, int totalCol)
        {
            board = new SnakePiece[totalRow, totalCol];
            this.totalRow = totalRow;
            this.totalCol = totalCol;
            snake = new LinkedSnake(3,3);
        }

        // method for printing the snake game board
        public void PrintBoard()
        {
            // center the console window around the game board
            Console.SetWindowSize(totalRow + 20, totalCol + 5);
            // move the board 2 spaces down from the top
            Console.CursorTop = 2;
            // method for building the board wall
            BuildWall();

            // nested for loop to print the board; by default any 2d array position not set to value is set to an enum SnakePiece.Space
            for (int row = 0; row < board.GetLength(0); row++)
            {
                // move the board 10 spaces to the left
                Console.CursorLeft = 10;

                for (int col = 0; col < board.GetLength(1); col++)
                {
                    // if the board contains a wall then color it in
                    if (board[row, col] == SnakePiece.Wall)
                    {
                        // color in the wall blue
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    // if the board contains a space then color it in
                    if (board[row, col] == SnakePiece.Space)
                    {
                        // color in the spaces to default color, black
                        Console.ResetColor();
                    }
                    // ternary operator used to translate the enums to spaces
                    Console.Write(board[row, col] == SnakePiece.Space ? " " : " ");
                }
                // gives a new row in the 2d array
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        // method for building the snake game board wall
        private void BuildWall()
        {
            // builds a wall on the west and east side of the board
            for (int row = 0; row < board.GetLength(0); row++)
            {
                // west
                board[row, 0] = SnakePiece.Wall;
                // east
                board[row, board.GetLength(1) - 1] = SnakePiece.Wall;
            }
            // builds a wall on the north and south side of the board
            for (int col = 0; col < board.GetLength(1); col++)
            {
                // north
                board[0, col] = SnakePiece.Wall;
                // south
                board[board.GetLength(0) - 1, col] = SnakePiece.Wall;
            }
        }

        public bool SnakeHasCrash()
        {
            return false;
        }
    }
}
