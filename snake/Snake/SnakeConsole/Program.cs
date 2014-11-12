using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    class Program
    {
        private static Board game = new Board(30, 30);

        static void Main(string[] args)
        {


            do
            {
                game.PrintBoard();
            } 
            
            while (!game.HasCrashed);
        }
    }
}
