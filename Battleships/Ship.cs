namespace Battleships
{
    public class Ship
    {
        public bool[,] OccupiedCells { get; }
        public ShipType Type { get; }

        public Ship(bool[,] occupiedCells, ShipType type)
        {
            OccupiedCells = occupiedCells;
            Type = type;
        }

        public bool IsColliding(Ship other)
        {
            if (other == this)
            {
                return false;
            }

            var isColliding = false;

            CollectionsHelper.IterateThrough(OccupiedCells, (cellOccupied, row, col) =>
            {
                if (cellOccupied && other.OccupiedCells[row, col])
                {
                    isColliding = true;
                }
            });

            return isColliding;
        }
    }

    public enum ShipType
    {
        Battleship,
        Destroyer
    }
}