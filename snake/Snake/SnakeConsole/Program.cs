using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // fork a project
            // type: git clone <url of project>
            Board b1 = new Board(50, 50);
            b1.PrintBoard();

        }
    }
}
