namespace SimpleSnake.Core
{
    using SimpleSnake.GameObjects;
    using SimpleSnake.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class FoodFactory
    {
        private static Random Random;

        public static Food GenerateRandomFood(int boardWidth, int boardHeight)
        {
            var randX = Random.Next(1, boardWidth-1);
            var randY = Random.Next(1, boardHeight-1);
            Coordinate foodCoordinate = new Coordinate(randX,randY);

            List<Type> foodTypes = typeof(StartUp)
                .Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Food)))
                .ToList();

            Type foodType = foodTypes[Random.Next(0, foodTypes.Count)];

            return Activator.CreateInstance(foodType, foodCoordinate) as Food;
        }

        static FoodFactory()
        {
            Random = new Random();
        }
    }
}
