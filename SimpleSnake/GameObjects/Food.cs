namespace SimpleSnake.GameObjects
{
    public abstract class Food
    {
        public int FoodPoints { get; set; }
        public string FoodSymbol { get; set; }
        public Coordinate FoodCoordinate { get; set; }

        public Food(string symbol, int foodPoints, Coordinate coordinate)
        {
            this.FoodSymbol = symbol;
            this.FoodPoints = foodPoints;
            this.FoodCoordinate = coordinate;
        }
    }
}
