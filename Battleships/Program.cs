using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid(8, 12);
            grid.PlaceShips(1, 1);
            Console.Write(grid);

            while (!grid.GameIsOver)
            {
                Console.Write("Shot coords: ");
                var shotCoords = Console.ReadLine();

                try
                {
                    var shotResult = grid.Shot(shotCoords);
                    Console.Write(grid);
                    Console.WriteLine(shotResult);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Invalid coords provided. Enter eg. A5 for first column and fifth row on grid");
                }
            }

            Console.WriteLine("Game is over. All ships have sunk!");
        }
    }
}
