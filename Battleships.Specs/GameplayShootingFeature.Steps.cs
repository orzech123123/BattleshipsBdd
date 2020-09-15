using System;
using System.Collections.Generic;
using FluentAssertions;
using LightBDD.NUnit3;
using System.Linq;

namespace Battleships.Specs
{
    public partial class GameplayShootingFeature : FeatureFixture
    {
        private int _rows = 10;
        private int _cols = 10;
        private Grid _grid;
        private ShotResult _shotResult;

        private void Given_Initialized_Grid()
        {
            _grid = new Grid(_rows, _cols);
            _grid.PlaceShips(1, 2);
        }

        private void When_Player_Shoot_At_Empty_Cell()
        {
            var coordsString = GetAnyCoordsStringByState(null);

            _shotResult = _grid.Shot(coordsString);
        }

        private void When_Player_Shoot_At_Cell_With_Ship()
        {
            var coordsString = GetAnyCoordsStringByState(GridCellState.ShipSegment);

            _shotResult = _grid.Shot(coordsString);
        }

        private void When_Player_Shoot_At_All_Ships_Segments()
        {
            var shipCoords = new List<(int row, int col)>();
            CollectionsHelper.IterateThrough(_grid.Ships.First(ship => !ship.HasSunk()).OccupiedCells, (state, row, col) =>
            {
                if (state == GridCellState.ShipSegment)
                {
                    shipCoords.Add((row, col));
                }
            });

            foreach (var shipCoord in shipCoords)
            {
                _shotResult = _grid.Shot(GetCoordsString(shipCoord));
            }
        }

        private void When_Player_Sunk_All_Ships()
        {
            foreach (var _ in _grid.Ships)
            {
                When_Player_Shoot_At_All_Ships_Segments();
            }
        }

        private void Then_It_Result_Is_Mishit()
        {
            _shotResult.Should().Be(ShotResult.Mishit);
        }

        private void Then_It_Result_Is_Hit()
        {
            _shotResult.Should().Be(ShotResult.Hit);
        }

        private void Then_It_Result_Is_Sunk()
        {
            _shotResult.Should().Be(ShotResult.Sunk);
        }

        private void Then_It_Result_Is_Game_Over()
        {
            _grid.GameIsOver.Should().BeTrue();
        }

        private string GetAnyCoordsStringByState(GridCellState? state)
        {
            (int row, int col) coords = default;

            CollectionsHelper.IterateFromZeroTo(_rows, _cols, (row, col) =>
            {
                var isCellMatch = state == null
                    ? _grid.Ships.All(ship => ship.OccupiedCells[row, col] == null)
                    : _grid.Ships.Any(ship => ship.OccupiedCells[row, col] == state);

                if (isCellMatch)
                {
                    coords = (row, col);
                }
            });

            var coordsString = GetCoordsString(coords);

            return coordsString;
        }

        private static string GetCoordsString((int row, int col) coords)
        {
            var colLetter = (Char) ((97) + (coords.col));
            var coordsString = $"{colLetter}{coords.row + 1}";

            return coordsString;
        }
    }
}