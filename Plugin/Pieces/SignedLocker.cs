using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;
using ChestReloaded.Util;
using System.Collections.Generic;
using System.Linq;
using System;

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
            var size = CalculateLockerSize();
            var container = cloned.GetComponent<Container>();
            container.m_width = size.Width;
            container.m_height = size.Height;
            PieceTable hammerTable = GetHammerTable();
            hammerTable.m_pieces.Add(cloned.gameObject);
            Log.LogInfo("Register Signed Chest");
        }

        private static PieceTable GetHammerTable()
        {
            GameObject hammerPrefab = Prefab.Cache.GetPrefab<GameObject>("_HammerPieceTable");
            PieceTable hammerTable = hammerPrefab.GetComponent<PieceTable>();
            return hammerTable;
        }

        private static GameObject GetPiece(string name)
        {
            var hammerTable = GetHammerTable();
            return hammerTable.m_pieces.First((GameObject piece) => piece.name == name);
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
        private static ContainerSize CalculateLockerSize()
        {
            var ironChestPrefab = GetPiece("piece_chest").GetComponent<Container>();
            var ironChestSize = ironChestPrefab.m_width * ironChestPrefab.m_height;
            var ironChestIndex = Array.IndexOf(
                possibleContainerSizes,
                possibleContainerSizes.First((ContainerSize container) => container.Size == ironChestSize));
            var woodChestPrefab = GetPiece("piece_chest_wood").GetComponent<Container>();
            var woodChestSize = woodChestPrefab.m_width * woodChestPrefab.m_height;
            var woodChestIndex = Array.IndexOf(
                possibleContainerSizes,
                possibleContainerSizes.First((ContainerSize container) => container.Size == woodChestSize));

            double middleSizeIndex = (woodChestIndex + ironChestIndex) / 2;

            var result = possibleContainerSizes[(int)Math.Ceiling(middleSizeIndex)];

            return result;
        }


        private struct ContainerSize
        {
            public int Size { get { return Width * Height; } }
            public int Width;
            public int Height;
        }

        // this comparer used to filter out equal sizes with different Width/Height
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
