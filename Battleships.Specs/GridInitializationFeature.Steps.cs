using FluentAssertions;
using LightBDD.NUnit3;
using System.Linq;

namespace Battleships.Specs
{
    public partial class GridInitializationFeature : FeatureFixture
    {
        private Grid _grid;

        private void Given_New_Grid()
        {
            _grid = new Grid(10, 10);
        }

        private void When_Ships_Are_Placed_On_Grid()
        {
            _grid.PlaceShips(1, 2);
        }

        private void Then_On_The_Grid_There_Are_One_Battleship_And_Two_Destroyers()
        {
            _grid.Ships.Should().HaveCount(3);
            _grid.Ships.Where(ship => ship.Type == ShipType.Battleship).Should().HaveCount(1);
            _grid.Ships.Where(ship => ship.Type == ShipType.Destroyer).Should().HaveCount(2);
        }
    }
}