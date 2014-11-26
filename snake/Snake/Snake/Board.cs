using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum SnakePiece { Space = 0, Wall, Body, Apple, Bomb }

    class Board
    {
        public SnakePiece[,] GameBoard {get; private set;}

        private SnakePiece[,] previousBoard;

        private int totalRow;
        private int totalCol;
        private LinkedSnake snake;
        private Random rand = new Random();

        public int ApplesEaten {get; private set;}
        public int SnakeMoves { get; private set; }
        public bool HasCrashed { get; private set; }
       
        // sets game board to any numbers of row and column
        public Board(int totalRow, int totalCol)
        {

            previousBoard = new SnakePiece[totalRow, totalCol];
            GameBoard = new SnakePiece[totalRow, totalCol];

            this.totalRow = totalRow;
            this.totalCol = totalCol;
            // starts the snake on row 2 and col 2
            //snake = new LinkedSnake(totalRow / 2, totalCol / 2);
            snake = new LinkedSnake( 2, 2 );

            BuildWall();
            //Console.SetWindowSize(totalRow + 20, totalCol + 5);
            GameBoard[3, 6] = SnakePiece.Apple;

            //reset
            ApplesEaten = 0;
            SnakeMoves = 0;
            HasCrashed = false;
        }

        /*
         * Creates a new LinkedSnake and moves it in the appropriate direction. It uses the 
         * location of that lookAhead snake to check for walls, bombs, body, or apples.
         * 
         * If the snake eats a apple, then it grows. If the snake runs into itself, a wall, or 
         * a bomb, then the snake crashes and the game is over.
         * 
         * The board is stripped of all snake pieces, and the updated snake is then added back 
         * into to the board.
         */
        public void UpdateBoard(Direction direction)
        {
            LinkedSnake lookAhead = new LinkedSnake(snake.Row, snake.Col);
            lookAhead.Move(direction);

            SnakePiece nextPiece = GameBoard[lookAhead.Row, lookAhead.Col];
            if (nextPiece == SnakePiece.Wall || nextPiece == SnakePiece.Bomb || nextPiece == SnakePiece.Body)
            {
                HasCrashed = true;
                //System.Windows.MessageBox.Show(string.Format("Game Over! Crash: {0}", nextPiece));
                return;
            }

            if (nextPiece == SnakePiece.Apple)
            {
                snake.Eat(lookAhead.Row, lookAhead.Col);
                ApplesEaten++;           
            }
            else
            {
                snake.Move(direction);
            }

            RemoveSnakeFromBoard();

            LinkedSnake tempSnake = snake;
            while (tempSnake != null)
            {
                GameBoard[tempSnake.Row, tempSnake.Col] = SnakePiece.Body;
                tempSnake = tempSnake.Previous;
            }

            SnakeMoves++;
            CreateApple();
            CreateBomb();
        }

        // Places an apple on the board sometimes
        private void CreateApple()
        {

            if (rand.Next(100) % 10 == 0)
            {
                SetRandomPiece(SnakePiece.Apple);
            }
        }

        // places a bomb on the board sometimes
        public void CreateBomb()
        {
            if (rand.Next(100) == 75)
            {
                SetRandomPiece(SnakePiece.Bomb);
            }
        }

        /**
         * This method simply strips the board of snake pieces so the new updated
         * snake can be placed back into the board.
         */
        private void RemoveSnakeFromBoard()
        {
            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    if (GameBoard[i, j] == SnakePiece.Body)
                    {
                        GameBoard[i, j] = SnakePiece.Space;
                    }
                }
            }
        }

        // Creates a border of SnakePiece.Wall around the board
        private void BuildWall()
        {
            // builds a wall on the west and east side of the board
            for (int row = 0; row < GameBoard.GetLength(0); row++)
            {
                // west
                GameBoard[row, 0] = SnakePiece.Wall;
                // east
                GameBoard[row, GameBoard.GetLength(1) - 1] = SnakePiece.Wall;
            }
            // builds a wall on the north and south side of the board
            for (int col = 0; col < GameBoard.GetLength(1); col++)
            {
                // north
                GameBoard[0, col] = SnakePiece.Wall;
                // south
                GameBoard[GameBoard.GetLength(0) - 1, col] = SnakePiece.Wall;
            }
        }


        // puts game piece on the board. This method is used for randomly 
        // placing an apple or a bomb on the board.
        private void SetRandomPiece(SnakePiece piece)
        {

            bool set = false;

            do
            {
                int randRow = rand.Next(1, totalRow - 1);
                int randCol = rand.Next(1, totalCol- 1);

                if (GameBoard[randRow, randCol] == SnakePiece.Space)
                {
                    GameBoard[randRow, randCol] = piece;
                    set = true;
                }
            }
            while (!set);
        }

        public int ComputeScore()
        {
            return ApplesEaten * SnakeMoves;
        }
    }
}
