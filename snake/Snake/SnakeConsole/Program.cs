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

            // To commit and push to git hub:
            // View --> Team Explorer
            // click the top tap where you see csis2530(Local)
            // Select Changes
            // Expand
            // git pull upstream master --> type this in to fetch changes
            // git status
            // git log


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
