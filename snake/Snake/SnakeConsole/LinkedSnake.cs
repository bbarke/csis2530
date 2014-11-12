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
        public int Col { get; private set; }
        public int Row { get; private set; }

        public LinkedSnake Previous { get; private set; }

        public LinkedSnake(int x, int y)
        {
            this.Col = x;
            this.Row = y;
        }

        private LinkedSnake(int x, int y, LinkedSnake prev)
        {
            this.Col = x;
            this.Row = y;
            this.Previous = prev;
        }

        public void Eat(int x, int y)
        {
            this.Previous = new LinkedSnake(this.Col, this.Row, Previous);

            this.Col = x;
            this.Row = y;

        }

        private void Move(int x, int y)
        {
            //move previous node to whatever this node was currently at.
            if (Previous != null)
            {
                Previous.Move(this.Col, this.Row);
            }

            this.Col = x;
            this.Row = y;
        }

        public void Move(Direction dir)
        {
            if (Previous != null)
                Previous.Move(Col, Row);

            switch (dir)
            {
                case Direction.Up:
                    Row = Row - 1;
                    break;
                case Direction.Down:
                    Row = Row + 1;
                    break;
                case Direction.Left:
                    Col = Col - 1;
                    break;
                case Direction.Right:
                    Col = Col + 1;
                    break;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(String.Format("[{0}, {1}]", Col, Row));

            if (Previous != null)
            {
                sb.Append(Previous.ToString());
            }
            return sb.ToString();
        }
    }
}
