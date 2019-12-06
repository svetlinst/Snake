namespace SimpleSnake.Core
{
    using SimpleSnake.GameObjects;
    using System;
    using System.Threading;
    using SimpleSnake.Enums;
    using System.Collections.Generic;

    public class Engine
    {
        private const string SnakeSymbol = "\\u25CF";
        private const int SuspensionTime = 1000;
        private Snake snake;
        private Food currentFood;
        private DrawManager drawManager;

        public void Run()
        {
            while (true)
            {
                this.drawManager.Draw(SnakeSymbol, snake.Body);

                if (Console.KeyAvailable)
                {
                    this.SetNewDirection(Console.ReadKey());
                }

                this.drawManager.Draw(currentFood.FoodSymbol, new List<Coordinate> { currentFood.FoodCoordinate });
                               
                this.snake.Move();
                
                this.drawManager.UndoDrawn();

                if (HasEatCollision())
                {
                    this.snake.Eat(currentFood);
                    this.InitializeFood();
                }

                Thread.Sleep(SuspensionTime);

            }
        }

        private void InitializeFood()
        {
            this.currentFood = FoodFactory.GenerateRandomFood(20, 20);
        }

        private void SetNewDirection(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (this.snake.CurrentDirection != Direction.Right)
                    {
                        this.snake.CurrentDirection = Direction.Left;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (this.snake.CurrentDirection != Direction.Left)
                    {
                        this.snake.CurrentDirection = Direction.Right;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (this.snake.CurrentDirection != Direction.Down)
                    {
                        this.snake.CurrentDirection = Direction.Up;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (this.snake.CurrentDirection != Direction.Up)
                    {
                        this.snake.CurrentDirection = Direction.Down;
                    }
                    break;
            }
        }

        private bool HasEatCollision()
        {
            int headCoordinateX = this.snake.Head.CoordinateX;
            int headCoordinateY = this.snake.Head.CoordinateY;

            int foodCoordinateX = this.currentFood.FoodCoordinate.CoordinateX;
            int foodCoordinateY = this.currentFood.FoodCoordinate.CoordinateY;

            return headCoordinateX == foodCoordinateX && headCoordinateY == foodCoordinateY;
        }
    }
}
