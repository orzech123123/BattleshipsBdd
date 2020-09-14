using Battleships.Specs;
using LightBDD.Core.Configuration;
using LightBDD.NUnit3;
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: ConfiguredLightBddScope]

namespace Battleships.Specs
{
    internal class ConfiguredLightBddScopeAttribute : LightBddScopeAttribute
    {
    }
}
