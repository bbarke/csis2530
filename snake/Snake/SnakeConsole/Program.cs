using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeConsole
{
    class Program
    {
        private static Board game = new Board(30, 30);
        // default direction for the snake going right
        private static LinkedSnake.Direction direction = LinkedSnake.Direction.Right;

        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Press enter to play");
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
                //Console.Clear(); 
                game.PrintBoard();
                Console.WriteLine("\n Apples eaten: {0}", game.Score);
                Thread.Sleep(100);
            }
            while (!game.HasCrashed);
        }
    }
}
