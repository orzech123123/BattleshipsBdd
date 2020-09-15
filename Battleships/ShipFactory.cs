using System;

namespace Battleships
{
    public class ShipFactory
    {
        private readonly Random _rnd;
        private readonly int _gridWidth;
        private readonly int _gridHeight;

        public ShipFactory(int gridWidth, int gridHeight)
        {
            _gridWidth = gridWidth;
            _gridHeight = gridHeight;
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
            var rowStart = _rnd.Next(0, _gridWidth - segmentsCount);
            var colStart = _rnd.Next(0, _gridHeight - segmentsCount);
            var horizontal = _rnd.NextDouble() > 0.5;

            var shipOccupiedCells = new GridCellState?[10, 10];

            for (var i = 0; i < segmentsCount; i++)
            {
                shipOccupiedCells[rowStart + (horizontal ? 0 : i), colStart + (horizontal ? i : 0)] = GridCellState.ShipSegment;
            }

            return new Ship(shipOccupiedCells);
        }
    }
}