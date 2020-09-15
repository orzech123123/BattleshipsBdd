using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid(10, 10);
            grid.PlaceShips(1, 2);
            grid.Draw();

            Console.ReadKey();
        }
    }
}
