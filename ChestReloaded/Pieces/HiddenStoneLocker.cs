using BepInEx.Configuration;
using ChestReloaded.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ChestReloaded.Pieces
{
    class HiddenStoneLocker: AbstractLocker
    {
        public override string Name => "Hidden Stone Locker";

        public override string PrefabName => "piece_hidden_stone_locker";

        protected override ConfigEntry<int> height => ConfigData.hiddenStoneLockerHeight;

        protected override ConfigEntry<int> width => ConfigData.hiddenStoneLockerWidth;

        protected override ContainerSize CalculateBalancedSize()
        {
            var ironChestPrefab = Helpers.GetPiece("piece_chest").GetComponent<Container>();
            var ironChestSize = ironChestPrefab.m_width * ironChestPrefab.m_height;
            var ironChestIndex = Array.IndexOf(
                ContainerSize.PossibleContainerSizes,
                ContainerSize.PossibleContainerSizes.First((ContainerSize container) => container.Size == ironChestSize));
            return ContainerSize.PossibleContainerSizes[ironChestIndex + 6];
        }

        public HiddenStoneLocker(AssetBundle assetBundle) : base(assetBundle) {
            LockerName = GameObjectName;
        }

        public static string LockerName { get; private set; }
    }
}
