using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;

namespace Battleships.Specs
{
    [Label("Gameplay shooting")]
    [FeatureDescription(@"In order to have ability to win game I want to shoot grid cells and get shooting results")]
    public partial class GameplayShootingFeature
    {
        [Label("Shot result is mishit")]
        [Scenario]
        public void Shot_Result_Is_Mishit()
        {
            Runner.RunScenario(
                Given_Initialized_Grid,
                When_Player_Shoot_At_Empty_Cell,
                Then_It_Result_Is_Mishit);
        }

        [Label("Shot result is hit")]
        [Scenario]
        public void Shot_Result_Is_Hit()
        {
            Runner.RunScenario(
                Given_Initialized_Grid,
                When_Player_Shoot_At_Cell_With_Ship,
                Then_It_Result_Is_Hit);
        }

        [Label("Shots result is sunk")]
        [Scenario]
        public void Shots_Result_Is_Sunk()
        {
            Runner.RunScenario(
                Given_Initialized_Grid,
                When_Player_Shoot_At_All_Ships_Segments,
                Then_It_Result_Is_Sunk);
        }

        [Label("Shots result is game over")]
        [Scenario]
        public void Shots_Result_Is_Game_Over()
        {
            Runner.RunScenario(
                Given_Initialized_Grid,
                When_Player_Sunk_All_Ships,
                Then_It_Result_Is_Game_Over);
        }
    }
}