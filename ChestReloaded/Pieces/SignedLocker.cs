using BepInEx.Configuration;
using ChestReloaded.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ChestReloaded.Pieces
{
    class SignedLocker : AbstractLocker
    {
        public override string Name => "Signed Locker";
        public override string PrefabName => "piece_signed_locker";

        /// <summary>
        /// Don't change this value. Otherwise already placed lockers will dissapear since this name is used in savegame data to identify items/pieces.
        /// Override here is intentional for backwards compatibility purposes.
        /// </summary>
        public override string GameObjectName => "SignedChest_ChestReloaded_SignedLocker_AddSignedChestPiece";

        protected override ConfigEntry<int> height => ConfigData.signedLockerHeight;

        protected override ConfigEntry<int> width => ConfigData.signedLockerWidth;

        protected override ContainerSize CalculateBalancedSize()
        {
            var ironChestPrefab = Helpers.GetPiece("piece_chest").GetComponent<Container>();
            var ironChestSize = ironChestPrefab.m_width * ironChestPrefab.m_height;
            var ironChestIndex = Array.IndexOf(
                ContainerSize.PossibleContainerSizes,
                ContainerSize.PossibleContainerSizes.First((ContainerSize container) => container.Size == ironChestSize));
            var woodChestPrefab = Helpers.GetPiece("piece_chest_wood").GetComponent<Container>();
            var woodChestSize = woodChestPrefab.m_width * woodChestPrefab.m_height;
            var woodChestIndex = Array.IndexOf(
                ContainerSize.PossibleContainerSizes,
                ContainerSize.PossibleContainerSizes.First((ContainerSize container) => container.Size == woodChestSize));

            double middleSizeIndex = (woodChestIndex + ironChestIndex) / 2;

            return ContainerSize.PossibleContainerSizes[(int)Math.Ceiling(middleSizeIndex)];
        }

        public SignedLocker(AssetBundle assetBundle) : base(assetBundle) { }
    }
}
