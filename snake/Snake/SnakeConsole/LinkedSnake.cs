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

        public LinkedSnake(int row, int col)
        {
            this.Col = col;
            this.Row = row;
        }

        private LinkedSnake(int row, int col, LinkedSnake prev)
        {
            this.Row = row;
            this.Col = col;
            this.Previous = prev;
        }

        //public void Eat(Direction dir)
        //{
        //    this.Previous = new LinkedSnake(this.Col, this.Row, this.Previous);
        //    MoveDirection(dir);

        //}

        public void Eat(int row, int col)
        {
            this.Previous = new LinkedSnake(this.Row, this.Col, this.Previous);

            this.Row = row;
            this.Col = col;
        }

        private void Move(int row, int col)
        {
            //move previous node to whatever this node was currently at.
            if (Previous != null)
            {
                Previous.Move(this.Row, this.Col);
            }

            this.Row = row;
            this.Col = col;
        }

        public void Move(Direction dir)
        {
            if (Previous != null)
            { 
                Previous.Move(this.Row, this.Col);
            }
            MoveDirection(dir);
        }


        private void MoveDirection(Direction dir)
        {
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

            sb.Append(String.Format("[{0}, {1}]", Row, Col));

            if (Previous != null)
            {
                sb.Append(Previous.ToString());
            }
            return sb.ToString();
        }
    }
}
