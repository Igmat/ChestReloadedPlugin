using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;
using ChestReloaded.Util;


namespace ChestReloaded.Pieces
{
    internal static class SignedLocker
    {
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

            GameObject hammerPrefab = Prefab.Cache.GetPrefab<GameObject>("_HammerPieceTable");
            PieceTable hammerTable = hammerPrefab.GetComponent<PieceTable>();
            hammerTable.m_pieces.Add(cloned.gameObject);
            Log.LogInfo("Register Signed Chest");
        }

    }
}
