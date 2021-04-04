using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;
using ChestReloaded.Util;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ChestReloaded.Pieces
{
    static class SignedLocker
    {
        private static Container signedLockerContainer;

        public static void Init()
        {
            ObjectDBHelper.OnAfterInit += AddPieces;
        }

        private static void AddPieces()
        {
            AssetBundle assetBundle = AssetBundleHelper.GetFromResources("signed_chest");
            AddSignedChestPiece(assetBundle);

            assetBundle.Unload(false);
        }

        private static void AddSignedChestPiece(AssetBundle assetBundle)
        {
            GameObject signedLocker = assetBundle.LoadAsset<GameObject>("Assets/Pieces/SignedChest.prefab");
            signedLocker.FixReferences();
            GameObject cloned = signedLocker.InstantiateClone("SignedChest");
            signedLockerContainer = cloned.GetComponent<Container>();
            ChangeSize();
            ContainerSizes.OnSignedLockerSizeChange += ChangeSize;

            Helpers.HammerTable.m_pieces.Add(cloned.gameObject);
            Log.LogInfo("Register Signed Chest");
        }

        private static void ChangeSize()
        {
            signedLockerContainer.m_width = ContainerSizes.SignedLockerSize.Width;
            signedLockerContainer.m_height = ContainerSizes.SignedLockerSize.Height;

        }

    }
}
