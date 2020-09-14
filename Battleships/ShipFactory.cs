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
            return CreateShip(5, ShipType.Battleship);
        }

        public Ship CreateDestroyer()
        {
            return CreateShip(4, ShipType.Destroyer);
        }

        private Ship CreateShip(int segmentsCount, ShipType shipType)
        {
            var rowStart = _rnd.Next(0, _gridWidth - segmentsCount);
            var colStart = _rnd.Next(0, _gridHeight - segmentsCount);
            var horizontal = _rnd.NextDouble() > 0.5;

            var shipSegments = new bool[10, 10];

            for (var i = 0; i < segmentsCount; i++)
            {
                shipSegments[rowStart + (horizontal ? 0 : i), colStart + (horizontal ? i : 0)] = true;
            }

            return new Ship(shipSegments, shipType);
        }
    }
}