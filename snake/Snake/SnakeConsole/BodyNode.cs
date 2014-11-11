using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    class BodyNode
    {

        public enum Direction { Up, Down, Left, Right }
        public int X { get; private set; }
        public int Y { get; private set; }

        private BodyNode prev;

        public BodyNode(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        private BodyNode(int x, int y, BodyNode prev)
        {
            this.X = x;
            this.Y = y;
            this.prev = prev;
        }

        public void push(int x, int y)
        {
            this.prev = new BodyNode(this.X, this.Y, prev);

            this.X = x;
            this.Y = y;

        }

        public BodyNode getPrev()
        {
            return prev;
        }

        private void move(int x, int y)
        {
            //move previous node to whatever this node was currently at.
            if (prev != null)
            {
                prev.move(this.X, this.Y);
            }

            this.X = x;
            this.Y = y;
        }

        public void move(Direction dir)
        {
            prev.move(X, Y);

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

            if (prev != null)
            {
                sb.Append(prev.ToString());
            }
            return sb.ToString();
        }
    }
}
