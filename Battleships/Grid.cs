using System.Collections.Generic;

namespace Battleships
{
    public class Grid
    {
        public int Width { get; }
        public int Height { get; }
        public IEnumerable<Ship> Ships => _ships;

        private readonly ShipFactory _shipFactory;
        private readonly IList<Ship> _ships = new List<Ship>();

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            _shipFactory = new ShipFactory(width, height);
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

        }

        public ShotResult Shot()
        {
            return ShotResult.Hit;
        }
    }

    public enum ShotResult
    {
        Hit,
        Miss,
        Sink
    }
}