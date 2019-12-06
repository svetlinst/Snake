namespace SimpleSnake.Core
{
    using SimpleSnake.GameObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DrawManager
    {
        private List<Coordinate> lastDrawnElements = new List<Coordinate>();

        public void Draw(string symbol, IReadOnlyCollection<Coordinate> coordinates)
        {
            foreach (Coordinate coordinate in coordinates)
            {
                if (symbol == GameObjects.Snake.Symbol)
                {
                    this.lastDrawnElements.Add(new Coordinate(coordinate.CoordinateX, coordinate.CoordinateY));
                }

                this.DrawOperation(symbol, coordinate);
            }
        }

        private void DrawOperation(string symbol, Coordinate coordinate)
        {
            Console.SetCursorPosition(coordinate.CoordinateX, coordinate.CoordinateY);
            Console.Write(symbol);
        }

        public void UndoDrawn()
        {
            if (this.lastDrawnElements.Any())
            {
                this.DrawOperation(" ", this.lastDrawnElements.First());
                this.lastDrawnElements.Clear();
            }
        }
    }
}
