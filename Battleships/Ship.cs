namespace Battleships
{
    public class Ship
    {
        private readonly GridCellState?[,] _occupiedCells;

        public Ship(GridCellState?[,] occupiedCells)
        {
            _occupiedCells = occupiedCells;
        }

        public GridCellState?[,] OccupiedCells => _occupiedCells.Clone() as GridCellState?[,];

        public void Damage(int row, int col)
        {
            _occupiedCells[row, col] = GridCellState.WreckSegment;
        }

        public bool HasSunk()
        {
            var hasSunk = true;

            CollectionsHelper.IterateThrough(_occupiedCells, (occupiedCell, row, col) =>
            {
                if (occupiedCell.HasValue && occupiedCell != GridCellState.WreckSegment)
                {
                    hasSunk = false;
                }
            });

            return hasSunk;
        }

        public bool IsColliding(Ship other)
        {
            if (other == this)
            {
                return false;
            }

            var isColliding = false;

            CollectionsHelper.IterateThrough(_occupiedCells, (occupiedCell, row, col) =>
            {
                if (occupiedCell.HasValue &&
                    other._occupiedCells[row, col].HasValue)
                {
                    isColliding = true;
                }
            });

            return isColliding;
        }
    }
}