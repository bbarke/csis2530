using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    // Snake enums
    public enum SnakePiece { Space = 0, Wall, Body, Apple, Bomb }

    class Board : Player
    {
        private SnakePiece[,] board;
        private LinkedSnake snake;
        private int totalRow;
        private int totalCol;
        private Random rand = new Random();
        public int SnakeMoves { get; private set; }
        public int ApplesEaten { get; private set; }
        public int Score { get; private set; }
        public bool HasCrashed { get; private set; }


        // sets game board to any numbers of row and column
        public Board(int totalRow, int totalCol)
        {
            board = new SnakePiece[totalRow, totalCol];
            this.totalRow = totalRow;
            this.totalCol = totalCol;
            // starts the snake on row 2 and col 2
            snake = new LinkedSnake(2, 2);
            BuildWall();
            SeedBoard();
            Console.SetWindowSize(totalRow + 20, totalCol + 5);
            PrintBoard(2, 10);
        }

        private void SeedBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                SetRandomPiece(SnakePiece.Apple, board);
            }

        }
        private void PrintBoard(int top, int left)
        {
            // move the board 2 spaces down from the top
            Console.CursorTop = top;
            // method for building the board wall

            // nested for loop to print the board; by default any 2d array position not set to value is set to an enum SnakePiece.Space
            for (int row = 0; row < board.GetLength(0); row++)
            {
                // move the board 10 spaces to the left
                Console.CursorLeft = left;

                for (int col = 0; col < board.GetLength(1); col++)
                {


                    // calls the method GetColor to color in each piece on the board

                    Console.BackgroundColor = GetColor(board[row, col]);
                    Console.Write(" ");


                }
                // gives a new row in the 2d array
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        public void PrintWall()
        {
            PrintBoard(2, 10);
        }


        // method for printing the snake game board
        public void PrintBoard()
        {
       
            // move the board 2 spaces down from the top
            Console.CursorTop = 3;
            // method for building the board wall

            // nested for loop to print the board; by default any 2d array position not set to value is set to an enum SnakePiece.Space
            for (int row = 1; row < board.GetLength(0)-1; row++)
            {
                // move the board 10 spaces to the left
                Console.CursorLeft = 11;

                for (int col = 1; col < board.GetLength(1)-1; col++)
                {


                    // calls the method GetColor to color in each piece on the board

                    Console.BackgroundColor = GetColor(board[row, col]);
                    Console.Write(" ");


                }
                // gives a new row in the 2d array
                Console.WriteLine();
            }
            Console.ResetColor();
           
        }

        // GetColor method used for coloring in the game board
        private ConsoleColor GetColor(SnakePiece piece)
        {
            switch (piece)
            {
                case SnakePiece.Space:
                    return ConsoleColor.Black;

                case SnakePiece.Wall:
                    return ConsoleColor.Blue;

                case SnakePiece.Body:
                    return ConsoleColor.Green;

                case SnakePiece.Apple:
                    return ConsoleColor.Red;

                case SnakePiece.Bomb:
                    return ConsoleColor.Yellow;

                default:
                    return ConsoleColor.Black;
            }
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

        // method for keeping score of the game
        public int KeepScore()
        {
            return Score = ApplesEaten * SnakeMoves;
        }

        public void CreateBomb(SnakePiece[,] newboard)
        {
            if (rand.Next(75) == 5)
            {
                SetRandomPiece(SnakePiece.Bomb, newboard);
            }
        }

        private void CreateApple(SnakePiece[,] newboard)
        {

            if (rand.Next(20) == 10)
            {
                SetRandomPiece(SnakePiece.Apple, newboard);
            }
        }

        private void SetRandomPiece(SnakePiece piece, SnakePiece[,] newboard)
        {

            bool set = false;

            do
            {
                int randRow = rand.Next(1, totalRow - 1);
                int randCol = rand.Next(1, totalCol - 1);

                if (newboard[randRow, randCol] == SnakePiece.Space)
                {


                    newboard[randRow, randCol] = piece;
                    set = true;
                }
            }
            while (!set);
        }

        private SnakePiece[,] CopyBoard()
        {
            SnakePiece[,] newBoard = new SnakePiece[board.GetLength(0), board.GetLength(1)];

            for (int i = 0; i < newBoard.GetLength(0); i++)
            {
                for (int j = 0; j < newBoard.GetLength(1); j++)
                {
                    if (board[i, j] != SnakePiece.Body)
                    {
                        newBoard[i, j] = board[i, j];
                    }
                }
            }

            return newBoard;
        }


        private void UpdateBoard()
        {

            SnakePiece[,] newboard = CopyBoard();

            CreateApple(newboard);
            CreateBomb(newboard);

            if (board[snake.Row, snake.Col] == SnakePiece.Apple)
            {
                snake.Eat(snake.Col, snake.Row);
                ApplesEaten++;
            }

            LinkedSnake tempSnake = snake;
            SnakePiece piece = board[tempSnake.Row, tempSnake.Col];

            if (piece == SnakePiece.Wall || piece == SnakePiece.Bomb || piece == SnakePiece.Body)
            {
                HasCrashed = true;
                return;
            }

            while (tempSnake != null)
            {
                newboard[tempSnake.Row, tempSnake.Col] = SnakePiece.Body;
                tempSnake = tempSnake.Previous;
            }
            SnakeMoves++;
            board = newboard;
        }

        public void Move(LinkedSnake.Direction direction)
        {
            snake.Move(direction);
            UpdateBoard();
        }
    }
}
