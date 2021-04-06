using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChestReloaded.Util
{
    struct ContainerSize
    {
        public int Size { get { return Width * Height; } }
        public int Width;
        public int Height;

        public static readonly ContainerSize[] PossibleContainerSizes =
           (new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })
           .SelectMany((int i) =>
               (new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })
               .Select((int j) =>
               new ContainerSize() { Height = i, Width = j }))
           .Distinct(new ContainerSizeComparer())
           .OrderBy((ContainerSize container) => container.Size)
           .ToArray();

        /// <summary>
        /// this comparer used to filter out equal sizes with different Width/Height
        /// </summary>
        private class ContainerSizeComparer : IEqualityComparer<ContainerSize>
        {
            public bool Equals(ContainerSize x, ContainerSize y)
            {
                return x.Size == y.Size;
            }

            public int GetHashCode(ContainerSize obj)
            {
                return obj.Size;
            }
        }
    }
}
