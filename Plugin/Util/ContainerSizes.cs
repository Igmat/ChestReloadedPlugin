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
    }

    // this comparer used to filter out equal sizes with different Width/Height
    class ContainerSizeComparer : IEqualityComparer<ContainerSize>
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
    
    static class ContainerSizes
    {
        public static Action OnSignedLockerSizeChange;
        static ContainerSizes()
        {
            ConfigData.isBalanced.SettingChanged += IsBalanced_SettingChanged;
            ConfigData.signedLockerWidth.SettingChanged += SignedLockerSize_SettingChanged;
            ConfigData.signedLockerHeight.SettingChanged += SignedLockerSize_SettingChanged;
        }
        private static void IsBalanced_SettingChanged(object sender, EventArgs e)
        {
            OnSignedLockerSizeChange.Invoke();
        }

        private static void SignedLockerSize_SettingChanged(object sender, EventArgs e)
        {
            if (!ConfigData.isBalanced.Value) OnSignedLockerSizeChange.Invoke();
        }


        private static ContainerSize[] possibleContainerSizes =
           (new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })
           .SelectMany((int i) =>
               (new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })
               .Select((int j) =>
               new ContainerSize() { Height = i, Width = j }))
           .Distinct(new ContainerSizeComparer())
           .OrderBy((ContainerSize container) => container.Size)
           .ToArray();

        public static ContainerSize SignedLockerSize
        {
            get
            {
                if (!ConfigData.isBalanced.Value)
                {
                    return new ContainerSize()
                    {
                        Height = ConfigData.signedLockerHeight.Value,
                        Width = ConfigData.signedLockerWidth.Value
                    };
                }
                var ironChestPrefab = Helpers.GetPiece("piece_chest").GetComponent<Container>();
                var ironChestSize = ironChestPrefab.m_width * ironChestPrefab.m_height;
                var ironChestIndex = Array.IndexOf(
                    possibleContainerSizes,
                    possibleContainerSizes.First((ContainerSize container) => container.Size == ironChestSize));
                var woodChestPrefab = Helpers.GetPiece("piece_chest_wood").GetComponent<Container>();
                var woodChestSize = woodChestPrefab.m_width * woodChestPrefab.m_height;
                var woodChestIndex = Array.IndexOf(
                    possibleContainerSizes,
                    possibleContainerSizes.First((ContainerSize container) => container.Size == woodChestSize));

                double middleSizeIndex = (woodChestIndex + ironChestIndex) / 2;

                var result = possibleContainerSizes[(int)Math.Ceiling(middleSizeIndex)];

                return result;
            }
        }
    }
}
