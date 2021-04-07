using BepInEx.Configuration;
using ChestReloaded.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ChestReloaded.Pieces
{
    class IronLocker : AbstractLocker
    {
        public override string Name => "Iron Locker";

        public override string PrefabName => "piece_iron_locker";

        protected override ConfigEntry<int> height => ConfigData.bigWoodenLockerHeight;

        protected override ConfigEntry<int> width => ConfigData.bigWoodenLockerWidth;

        protected override ContainerSize CalculateBalancedSize()
        {
            var ironChestPrefab = Helpers.GetPiece("piece_chest").GetComponent<Container>();
            var ironChestSize = ironChestPrefab.m_width * ironChestPrefab.m_height;
            var ironChestIndex = Array.IndexOf(
                ContainerSize.PossibleContainerSizes,
                ContainerSize.PossibleContainerSizes.First((ContainerSize container) => container.Size == ironChestSize));
            return ContainerSize.PossibleContainerSizes[ironChestIndex + 2];
        }

        public IronLocker(AssetBundle assetBundle) : base(assetBundle) { }
    }
}
