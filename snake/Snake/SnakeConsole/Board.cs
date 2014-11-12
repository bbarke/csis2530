using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    // Snake enums
    public enum SnakePiece { Space = 0, Wall, Body, Apple, Bomb }

    class Board
    {
        private SnakePiece[,] board;
        private int totalRow;
        private int totalCol;
        public bool HasCrashed { get; private set; }
        private bool HasEaten = false;
        private LinkedSnake snake;

        // sets game board to any numbers of row and column
        public Board(int totalRow, int totalCol)
        {
            board = new SnakePiece[totalRow, totalCol];
            this.totalRow = totalRow;
            this.totalCol = totalCol;
            snake = new LinkedSnake(3, 3);

            board[4, 4] = SnakePiece.Apple;
            board[5, 15] = SnakePiece.Apple;
            board[6, 22] = SnakePiece.Apple;
            board[7, 7] = SnakePiece.Apple;
            board[8, 3] = SnakePiece.Apple;
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
                    Console.BackgroundColor = GetColor(board[row, col]);
                    Console.Write(" ");
                }
                // gives a new row in the 2d array
                Console.WriteLine();
            }
            Console.ResetColor();
        }

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
        public void CreateBomb(SnakePiece[,] newboard)
        {
            Random rand = new Random(50);
            if (rand.Next() % 2 == 0)
            {
                SetRandomPiece(SnakePiece.Bomb, newboard);
            }
        }

        private void CreateApple(SnakePiece[,] newboard)
        {
            Random rand = new Random(50);
            if (rand.Next() % 2 == 0)
            {
                SetRandomPiece(SnakePiece.Apple, newboard);
            }
        }

        private void SetRandomPiece(SnakePiece piece, SnakePiece[,] newboard)
        {

            Random rand = new Random();
            int randRow = rand.Next(totalRow - 2);
            int randCol = rand.Next(totalCol - 2);
            bool set = false;
            do
            {
                if (newboard[randRow, randCol] == SnakePiece.Space)
                {
                    newboard[rand.Next(totalRow - 2), rand.Next(totalCol - 2)] = piece;
                    set = true;
                }

            }
            while (!set);
        }

        public void RemoveSnakeFromBoard(SnakePiece[,] newBoard)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {

                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (newBoard[row, col] == SnakePiece.Body)
                        newBoard[row, col] = SnakePiece.Space;
                }
            }
        }


        public void UpdateBoard()
        {

            SnakePiece[,] newboard = new SnakePiece[totalRow, totalCol];
            Array.Copy(board, newboard, board.Length);
            CreateApple(newboard);
            CreateBomb(newboard);
            RemoveSnakeFromBoard(newboard);
            

            if (board[snake.Row, snake.Col] == SnakePiece.Apple)
            {
                snake.Eat(snake.Col, snake.Row);
            }
            LinkedSnake tempSnake = snake;
            SnakePiece piece = board[tempSnake.Row, tempSnake.Col];
            if (piece == SnakePiece.Wall || piece == SnakePiece.Bomb || piece == SnakePiece.Body)
            {
                //Console.WriteLine("Crashed");
                HasCrashed = true;
                return;
            }

            while (tempSnake != null)
            {
                newboard[tempSnake.Row, tempSnake.Col] = SnakePiece.Body;
                tempSnake = tempSnake.Previous;
            }

            board = newboard;
        }

        public void Move(LinkedSnake.Direction direction)
        {
            snake.Move(direction);
            UpdateBoard();
        }
    }
}
