namespace SimpleSnake.GameObjects
{
    public class Coordinate
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }

        public Coordinate(int coordinateX, int coordinateY)
        {
            this.CoordinateX = coordinateX;
            this.CoordinateY = coordinateY;
        }
    }
}
