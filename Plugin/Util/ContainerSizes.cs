﻿using System;
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
            // group by all available sizes
            .GroupBy(container => container.Size)
            // select closest to square shape in each group, because it's most usable one
            .Select(containers => containers
                .OrderBy(container => Math.Abs(container.Height - container.Width))
                .First())
            .OrderBy(container => container.Size)
            // exclude thin and long containers, because they don't look good in UI
            .Where(container => !(container.Width < 5 && container.Height - container.Width > 5))
            // prefer width over heigh if possible
            .Select(container => (container.Height > container.Width && container.Height <= 8)
                ? new ContainerSize() { Height = container.Width, Width = container.Height }
                : container)
            .ToArray();
    }
}
