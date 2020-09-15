using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleships
{
    public class Grid
    {
        public int Width { get; }
        public int Height { get; }
        public IEnumerable<Ship> Ships => _ships;

        private readonly ShipFactory _shipFactory;
        private readonly IList<Ship> _ships = new List<Ship>();

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            _shipFactory = new ShipFactory(width, height);
        }

        public void PlaceShips(int battleshipsCount, int destroyersCount)
        {
            var shipsToPlace = new List<Ship>();
            bool areShipsColliding;

            do
            {
                shipsToPlace.Clear();
                areShipsColliding = false;

                for (var i = 0; i < battleshipsCount; i++)
                {
                    shipsToPlace.Add(_shipFactory.CreateBattleship());
                }
                for (var i = 0; i < destroyersCount; i++)
                {
                    shipsToPlace.Add(_shipFactory.CreateDestroyer());
                }

                CollectionsHelper.IterateThrough(shipsToPlace, (ship, other) =>
                {
                    if (ship.IsColliding(other))
                    {
                        areShipsColliding = true;
                    }
                });
            } while (areShipsColliding);

            shipsToPlace.ForEach(ship => _ships.Add(ship));
        }

        public void Draw()
        {
            var builder = new StringBuilder();

            for (var row = 0; row < Width; row++)
            {
                for (var col = 0; col < Height; col++)
                {
                    var gridCell = _ships
                        .FirstOrDefault(ship => ship.OccupiedCells[row, col] != null)?
                        .OccupiedCells[row, col];

                    builder.Append(!gridCell.HasValue ? "-" : (gridCell == GridCellState.ShipSegment ? "o" : "x"));
                }
                builder.AppendLine();
            }

            Console.WriteLine(builder.ToString());
        }

        public ShotResult Shot()
        {
            return ShotResult.Hit;
        }
    }

    public enum GridCellState
    {
        ShipSegment,
        WreckSegment
    }

    public enum ShotResult
    {
        Hit,
        Miss,
        Sink
    }
}