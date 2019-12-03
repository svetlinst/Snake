namespace SimpleSnake.GameObjects
{
    using SimpleSnake.Enums;
    using System.Collections.Generic;
    using System.Linq;

    public class Snake
    {
        private List<Coordinate> snakeBody;
        public Direction CurrentDirection { get; set; }
        public Coordinate Head { get => snakeBody.Last(); }

        public Snake()
        {
            this.snakeBody = new List<Coordinate>();
            this.InitializeDefaultSnake();
            this.CurrentDirection = Direction.Right;
        }


        private void InitializeDefaultSnake()
        {
            int x = 5;
            int y = 6;

            for (int i = 1; i <= 6; i++)
            {
                this.snakeBody.Add(new Coordinate(x++, y));
            }
        }

        private Coordinate CalculateNewCoordinate(Coordinate newCoordinate)
        {
            switch (this.CurrentDirection)
            {
                case Direction.Right:
                    newCoordinate.CoordinateX += 1;
                    break;
                case Direction.Left:
                    newCoordinate.CoordinateX -= 1;
                    break;
                case Direction.Down:
                    newCoordinate.CoordinateY += 1;
                    break;
                case Direction.Up:
                    newCoordinate.CoordinateY -= 1;
                    break;
                default:
                    break;
            }
            return newCoordinate;
        }

        public void Coordinate()
        {
            Coordinate currentHead = this.snakeBody.Last();

            Coordinate newHeadCoordinate = this.CalculateNewCoordinate(new Coordinate(currentHead.CoordinateX, currentHead.CoordinateY));

            this.snakeBody.Add(newHeadCoordinate);
            this.snakeBody.RemoveAt(0);
        }

        public void Eat(Food food)
        {
            for (int i = 0; i < food.FoodPoints; i++)
            {
                Coordinate coordinate = new Coordinate(this.Head.CoordinateX, this.Head.CoordinateY);
                this.snakeBody.Add(this.CalculateNewCoordinate(coordinate));
            }
        }
    }
}
