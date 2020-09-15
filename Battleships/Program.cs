using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid(10, 10);
            grid.PlaceShips(1, 0);
            grid.Draw();

            while (!grid.GameIsOver)
            {
                var row = Convert.ToInt32(Console.ReadLine());
                var col = Convert.ToInt32(Console.ReadLine());
                var shotResult = grid.Shot(row, col);

                grid.Draw();
                Console.WriteLine(shotResult);
            }

            Console.WriteLine("Game is over. All ships have sunk!");
        }
    }
}
