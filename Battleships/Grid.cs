using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Battleships
{
    public class Grid
    {
        private readonly int _rows;
        private readonly int _cols;
        private readonly ShipFactory _shipFactory;
        private readonly IList<Ship> _ships = new List<Ship>();
        private readonly bool[,] _mishitCells;

        public IEnumerable<Ship> Ships => _ships.ToArray();

        public Grid(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _shipFactory = new ShipFactory(rows, cols);
            _mishitCells = new bool[rows, cols];
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

        public override string ToString()
        {
            var builder = new StringBuilder();

            CollectionsHelper.IterateFromZeroTo(_rows, _cols, (row, col) =>
            {
                var gridCell = _ships
                    .SingleOrDefault(ship => ship.OccupiedCells[row, col] != null)
                    ?.OccupiedCells[row, col];

                builder.Append(gridCell.HasValue ? (gridCell == GridCellState.ShipSegment ? "o" : "x") :
                                _mishitCells[row, col] ? "*" :
                                    "-");

                if (col == _cols - 1)
                {
                    builder.AppendLine();
                }
            });

            return builder.ToString();
        }

        public bool GameIsOver => _ships.All(ship => ship.HasSunk());

        public ShotResult Shot(string coords)
        {
            var (row, col) = ParseCoords(coords);

            var hitShip = _ships
                .SingleOrDefault(ship => ship.OccupiedCells[row, col] != null);

            var hitGridCell = hitShip
                ?.OccupiedCells[row, col];

            if (!hitGridCell.HasValue || hitGridCell == GridCellState.WreckSegment)
            {
                _mishitCells[row, col] = true;
                return ShotResult.Mishit;
            }

            hitShip.Damage(row, col);

            return hitShip.HasSunk() ? ShotResult.Sunk : ShotResult.Hit;
        }

        private (int row, int col) ParseCoords(string coords)
        {
            const string pattern = "^[A-Za-z]\\d+$";

            if (!Regex.IsMatch(coords, pattern))
            {
                throw new ArgumentException(nameof(coords));
            }

            var row = int.Parse(coords.Remove(0, 1));
            var col = char.ToUpper(coords[0]) - 64;

            row--;
            col--;

            if (row >= _rows || col >= _cols)
            {
                throw new ArgumentException(nameof(coords));
            }

            return (row, col);
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
        Mishit,
        Sunk
    }
}