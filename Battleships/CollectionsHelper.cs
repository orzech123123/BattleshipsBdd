using System;
using System.Collections.Generic;

namespace Battleships
{
    public static class CollectionsHelper
    {
        public static void IterateThrough<T>(T[,] collection, Action<T, int, int> action)
        {
            for (var row = 0; row < collection.GetLength(1); row++)
            for (var col = 0; col < collection.GetLength(0); col++)
                action(collection[row, col], row, col);
        }

        public static void IterateThrough<T>(ICollection<T> collection, Action<T, T> action)
        {
            foreach (var element in collection)
            foreach (var other in collection)
                action(element, other);
        }

        public static void IterateFromZeroTo(int firstDimensionMax, int secondDimensionMax, Action<int, int> action)
        {
            for (var dim1 = 0; dim1 < firstDimensionMax; dim1++)
            for (var dim2 = 0; dim2 < secondDimensionMax; dim2++)
                action(dim1, dim2);
        }
    }
}