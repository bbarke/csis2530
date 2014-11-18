using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    // Snake enums
    public enum SnakePiece { Space = 0, Wall, Body, Apple, Bomb }

    class Board : Player  // Why is Board inheriting from Player? Board is NOT a Player
    {
        private SnakePiece[,] board;
        private SnakePiece[,] tempBoard;
        private LinkedSnake snake;
        private int totalRow;
        private int totalCol;
        private Random rand = new Random();
        public int SnakeMoves { get; private set; }
        public int ApplesEaten { get; private set; }  // Why is ApplesEaten a property?
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
            board[3, 6] = SnakePiece.Apple;
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
            for (int row = 1; row < board.GetLength(0) - 1; row++)
            {
                // move the board 10 spaces to the left
                Console.CursorLeft = 11;

                for (int col = 1; col < board.GetLength(1) - 1; col++)
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
            //if (rand.Next(75) == 5)
            if (rand.Next(5) == 4)
            {
                SetRandomPiece(SnakePiece.Bomb, newboard);
            }
        }

        private void CreateApple(SnakePiece[,] newboard)
        {

            //if (rand.Next(20) == 10)
            if (true)
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

        private void RemoveSnakeFromBoard()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == SnakePiece.Body)
                    {
                        board[i, j] = SnakePiece.Space;
                    }
                }
            }

        }


        private void UpdateBoard(LinkedSnake.Direction direction)
        {

            //SnakePiece[,] newboard = CopyBoard();


            LinkedSnake lookAhead = new LinkedSnake(snake.Row, snake.Col);
            lookAhead.Move(direction);
            SnakePiece nextPiece = board[lookAhead.Row, lookAhead.Col];
            if (nextPiece == SnakePiece.Wall || nextPiece == SnakePiece.Bomb || nextPiece == SnakePiece.Body)
            {
                HasCrashed = true;
                return;
            }

            if (nextPiece == SnakePiece.Apple)
            {
                snake.Eat(lookAhead.Row, lookAhead.Col);
                ApplesEaten++;
                board[snake.Row, snake.Col] = SnakePiece.Wall;
            }
            else
            {
                //Console.WriteLine(snake);
                snake.Move(direction);
                //Console.WriteLine(snake);

            }

            RemoveSnakeFromBoard();

            LinkedSnake tempSnake = snake;
            while (tempSnake != null)
            {
                board[tempSnake.Row, tempSnake.Col] = SnakePiece.Body;
                tempSnake = tempSnake.Previous;
            }

            SnakeMoves++;
            CreateApple(board);
            CreateBomb(board);
        }

        public void Move(LinkedSnake.Direction direction)
        {
            UpdateBoard(direction);
        }
    }
}
