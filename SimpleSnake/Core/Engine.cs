namespace SimpleSnake.Core
{
    using SimpleSnake.GameObjects;
    using System;
    using System.Threading;
    using SimpleSnake.Enums;
    using System.Collections.Generic;

    public class Engine
    {
        private const string SnakeSymbol = "\u25A0";
        private const string BoardSymbol = "\u25A0";
        private const int SuspensionTime = 100;
        private Snake snake;
        private Food currentFood;
        private DrawManager drawManager;
        private Coordinate boardCoordinate;
        private int gameScore;

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
                    this.gameScore += currentFood.FoodPoints;
                }

                if (HasBorderCollision())
                {
                    AskUserForRestart();
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

        private void InitializeBoard()
        {
            List<Coordinate> allCoordinates = new List<Coordinate>();

            this.InitializeHorizontalBorder(0, allCoordinates);
            this.InitializeHorizontalBorder(this.boardCoordinate.CoordinateY - 1, allCoordinates);
            this.InitializeVerticalBorder(0, allCoordinates);
            this.InitializeVerticalBorder(this.boardCoordinate.CoordinateX, allCoordinates);

            this.drawManager.Draw(BoardSymbol, allCoordinates);
        }

        private void InitializeVerticalBorder(int coordinateX, List<Coordinate> allCoordinates)
        {
            for (int i = 0; i < this.boardCoordinate.CoordinateY; i++)
            {
                allCoordinates.Add(new Coordinate(coordinateX, i));
            }
        }

        private void InitializeHorizontalBorder(int coordinateY, List<Coordinate> allCoordinates)
        {
            for (int coordinateX = 0; coordinateX < this.boardCoordinate.CoordinateX; coordinateX++)
            {
                allCoordinates.Add(new Coordinate(coordinateX, coordinateY));
            }
        }

        private bool HasBorderCollision()
        {
            int headCoordinateX = this.snake.Head.CoordinateX;
            int headCoordinateY = this.snake.Head.CoordinateY;

            bool hasLeftBorderCollision = headCoordinateY <= 0 || headCoordinateY >= this.boardCoordinate.CoordinateY - 1;
            bool hasTopBorderCollision = headCoordinateX <= 0 || headCoordinateX >= this.boardCoordinate.CoordinateX - 1;

            return hasLeftBorderCollision || hasTopBorderCollision;
        }

        private void AskUserForRestart()
        {
            int x = 45;
            int y = 20;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Would you like to continue? ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"Y/N");

            string input = Console.ReadLine();

            if (input?.ToLower() == "y")
            {
                Console.Clear();
                StartUp.Main();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void PlayerInfo()
        {
            Console.SetCursorPosition(this.boardCoordinate.CoordinateX + 10, 10);
            Console.Write($"Game score: {this.gameScore}");
        }

        public Engine(DrawManager drawManager, Snake snake, Coordinate boardCoordinate)
        {
            this.drawManager = drawManager;
            this.snake = snake;
            this.InitializeFood();
            this.boardCoordinate = boardCoordinate;
            this.InitializeBoard();
        }
    }
}
