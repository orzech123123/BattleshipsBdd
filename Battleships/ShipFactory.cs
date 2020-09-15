using System;

namespace Battleships
{
    public class ShipFactory
    {
        private readonly Random _rnd;
        private readonly int _rows;
        private readonly int _cols;

        public ShipFactory(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _rnd = new Random(Guid.NewGuid().GetHashCode());
        }

        public Ship CreateBattleship()
        {
            return CreateShip(5);
        }

        public Ship CreateDestroyer()
        {
            return CreateShip(4);
        }

        private Ship CreateShip(int segmentsCount)
        {
            var rowStart = _rnd.Next(0, _rows - segmentsCount);
            var colStart = _rnd.Next(0, _cols - segmentsCount);
            var horizontal = _rnd.NextDouble() > 0.5;

            var shipOccupiedCells = new GridCellState?[_rows, _cols];

            for (var i = 0; i < segmentsCount; i++)
            {
                shipOccupiedCells[rowStart + (horizontal ? 0 : i), colStart + (horizontal ? i : 0)] = GridCellState.ShipSegment;
            }

            return new Ship(shipOccupiedCells);
        }
    }
}