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
        private readonly bool[,] _mishitCells;

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            _shipFactory = new ShipFactory(width, height);
            _mishitCells = new bool[width, height];
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

            CollectionsHelper.IterateFromZeroTo(Width, Height, (row, col) =>
            {
                var gridCell = _ships
                    .SingleOrDefault(ship => ship.OccupiedCells[row, col] != null)
                    ?.OccupiedCells[row, col];

                builder.Append(gridCell.HasValue ? (gridCell == GridCellState.ShipSegment ? "o" : "x") :
                                _mishitCells[row, col] ? "*" :
                                    "-");

                if (col == Width - 1)
                {
                    builder.AppendLine();
                }
            });

            Console.WriteLine(builder.ToString());
        }

        public bool GameIsOver => _ships.All(ship => ship.HasSunk());

        public ShotResult Shot(int row, int col)
        {
            var shotShip = _ships
                .SingleOrDefault(ship => ship.OccupiedCells[row, col] != null);

            var gridCell = shotShip
                ?.OccupiedCells[row, col];

            if (!gridCell.HasValue || gridCell == GridCellState.WreckSegment)
            {
                _mishitCells[row, col] = true;
                return ShotResult.Miss;
            }

            shotShip.Damage(row, col);

            if (shotShip.HasSunk())
            {
                return ShotResult.Sunk;
            }

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
        Sunk
    }
}