using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    class LinkedSnake
    {

        public enum Direction { Up, Down, Left, Right }
        public int X { get; private set; }
        public int Y { get; private set; }

        private LinkedSnake previous { get; set; }

        public LinkedSnake(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        private LinkedSnake(int x, int y, LinkedSnake prev)
        {
            this.X = x;
            this.Y = y;
            this.previous = prev;
        }

        public void Eat(int x, int y)
        {
            this.previous = new LinkedSnake(this.X, this.Y, previous);

            this.X = x;
            this.Y = y;

        }

        private void Move(int x, int y)
        {
            //move previous node to whatever this node was currently at.
            if (previous != null)
            {
                previous.Move(this.X, this.Y);
            }

            this.X = x;
            this.Y = y;
        }

        public void Move(Direction dir)
        {
            previous.Move(X, Y);

            switch (dir)
            {
                case Direction.Up:
                    Y = Y - 1;
                    break;
                case Direction.Down:
                    Y = Y + 1;
                    break;
                case Direction.Left:
                    X = X - 1;
                    break;
                case Direction.Right:
                    X = X + 1;
                    break;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(String.Format("[{0}, {1}]", X, Y));

            if (previous != null)
            {
                sb.Append(previous.ToString());
            }
            return sb.ToString();
        }
    }
}
