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
        private Board game = new Board(50, 50);
        
        // default direction for the snake going right
        private LinkedSnake.Direction direction = LinkedSnake.Direction.Right;
        private ConsoleKeyInfo key;
        private string name;

        static void Main(string[] args)
        {
            MessageBox.Show("To Play:  Use keyboard arrows in order to move the snake.  Eat lots of red squares to gain points and increase snake size.  Avoid yellow squares. Have fun!");
         
            SnakeApp app = new SnakeApp();
            app.StartGame();
            
            Console.ReadLine();
        }


        public void StartGame()
        {
            do
            {
                Console.WriteLine("Enter your name and press enter to play");
                name = Console.ReadLine();
                game.PrintBoard();
                key = Console.ReadKey();
            }
            while (key.Key != ConsoleKey.Enter);

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
                Console.WriteLine("\n Apples eaten: {0} Moves: {1}", game.ApplesEaten, game.SnakeMoves);
                Console.WriteLine("\n Score: {0}", game.KeepScore());
                Thread.Sleep(5);
            }
            while (!game.HasCrashed);

            game.SavePlayerScore(name, game.Score);
            Console.WriteLine("\n( HIGH SCORE ---- TOP 10 )");
            game.PrintScoreBoard();
        }
    }
}
