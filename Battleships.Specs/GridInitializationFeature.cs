using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;

namespace Battleships.Specs
{
    [Label("Grid initialization")]
    [FeatureDescription(@"In order to have ability to play game I want to initialize grid with randomly placed warships")]
    public partial class GridInitializationFeature
    {
        [Label("One Battleship and Two Destroyers On Grid")]
        [Scenario]
        public void One_Battleship_And_Two_Destroyers_On_Grid()
        {
            Runner.RunScenario(
                Given_New_Grid,
                When_Ships_Are_Placed_On_Grid,
                Then_On_The_Grid_There_Are_One_Battleship_And_Two_Destroyers);
        }
    }
}