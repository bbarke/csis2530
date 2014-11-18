using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeConsole
{
    class SnakeApp
    {
        private int row = 50;
        private int col = 50;
        private Board game;

        // default direction for the snake going right
        private LinkedSnake.Direction direction = LinkedSnake.Direction.Right;
        private string name;

        static void Main(string[] args)
        {
            //MessageBox.Show("To Play:  Use keyboard arrows in order to move the snake.  Eat lots of red squares to gain points and increase snake size.  Avoid yellow squares. Have fun!");

            SnakeApp app = new SnakeApp();
            app.StartGame();
            //app.DebugSnake();

            Console.ReadLine();
        }

        public void StartGame()
        {

            Console.WriteLine("Enter your name and press enter to play:\n");
            name = Console.ReadLine();
            Console.Clear();
            game = new Board(50, 50);

            do
            {

                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.UpArrow:
                            direction = LinkedSnake.Direction.Up;
                            break;

                        case ConsoleKey.DownArrow:
                            direction = LinkedSnake.Direction.Down;
                            break;

                        case ConsoleKey.LeftArrow:
                            direction = LinkedSnake.Direction.Left;
                            break;

                        case ConsoleKey.RightArrow:
                            direction = LinkedSnake.Direction.Right;
                            break;

                        default:
                            break;
                    }
                }

                game.Move(direction);
                game.PrintBoard();

                Console.CursorTop = row + 3;
                Console.CursorLeft = 0;
                Console.WriteLine("\n Apples eaten: {0} Moves: {1}", game.ApplesEaten, game.SnakeMoves);
                Console.WriteLine("\n Score: {0}", game.KeepScore());
                Thread.Sleep(85);
            }
            while (!game.HasCrashed);

            //game.SavePlayerScore(name, game.Score);
            Console.WriteLine("\n( HIGH SCORE ---- TOP 10 )");
            game.PrintScoreBoard();
        }

        private void DebugSnake()
        {
            //Snake test:
            //snake data structure
            LinkedSnake head = new LinkedSnake(3, 3);
            //Eat some apples...
            //the x/y is the point of the apple, replacing the apple with a body part.
            head.Eat(3, 4);
            head.Eat(3, 5);
            head.Eat(4, 5);
            head.Eat(4, 6);
            head.Eat(4, 7);
            head.Eat(4, 8);
            head.Eat(5, 8);
            head.Eat(6, 8);
            Console.WriteLine(head);
            head.Move(LinkedSnake.Direction.Down);
            Console.WriteLine(head);
            head.Move(LinkedSnake.Direction.Right);
            Console.WriteLine(head);
        }
    }
}
