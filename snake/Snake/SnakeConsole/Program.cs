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

            Console.WriteLine("Entered");

            do
            {
                            
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.UpArrow:
                            direction = LinkedSnake.Direction.Up;
                            Console.WriteLine("Up");
                            break;

                        case ConsoleKey.DownArrow:
                            direction = LinkedSnake.Direction.Down;
                            Console.WriteLine("down");
                            break;

                        case ConsoleKey.LeftArrow:
                            direction = LinkedSnake.Direction.Left;
                            Console.WriteLine("Left");
                            break;

                        case ConsoleKey.RightArrow:
                            direction = LinkedSnake.Direction.Right;
                            Console.WriteLine("Right");
                            break;

                        default:
                            break;
                    }
                }
                
                game.Move(direction); 
                //Console.Clear(); 
                game.PrintBoard();
                Thread.Sleep(500);
            }
            while (!game.HasCrashed);
        }
    }
}
