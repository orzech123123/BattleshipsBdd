namespace Battleships
{
    public class Ship
    {
        public GridCellState?[,] OccupiedCells { get; }

        public Ship(GridCellState?[,] occupiedCells)
        {
            OccupiedCells = occupiedCells;
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
                if (cellOccupied.HasValue &&
                    other.OccupiedCells[row, col].HasValue)
                {
                    isColliding = true;
                }
            });

            return isColliding;
        }
    }
}