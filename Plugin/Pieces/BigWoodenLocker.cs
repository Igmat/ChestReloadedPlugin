using BepInEx.Configuration;
using ChestReloaded.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ChestReloaded.Pieces
{
    class BigWoodenLocker : AbstractLocker
    {
        public override string Name => "Big Wooden Locker";

        public override string PrefabName => "piece_big_wooden_locker";

        protected override ConfigEntry<int> height => ConfigData.bigWoodenLockerHeight;

        protected override ConfigEntry<int> width => ConfigData.bigWoodenLockerWidth;

        protected override ContainerSize CalculateBalancedSize()
        {
            var woodChestPrefab = Helpers.GetPiece("piece_chest_wood").GetComponent<Container>();
            var doubleWoodChestSize = woodChestPrefab.m_width * woodChestPrefab.m_height * 2;
            return ContainerSize.PossibleContainerSizes.First((ContainerSize container) => container.Size == doubleWoodChestSize);
        }

        public BigWoodenLocker(AssetBundle assetBundle) : base(assetBundle) { }
    }
}
