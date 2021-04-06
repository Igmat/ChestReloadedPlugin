using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;
using ChestReloaded.Util;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ChestReloaded.Pieces
{
    static class Lockers
    {
        private static List<AbstractLocker> lockers = new List<AbstractLocker>();
        public static void Init()
        {
            ObjectDBHelper.OnAfterInit += AddPieces;
        }

        private static void AddPieces()
        {
            AssetBundle assetBundle = AssetBundleHelper.GetFromResources("signed_chest");
            lockers.Add(new BigWoodenLocker(assetBundle));
            lockers.Add(new SignedLocker(assetBundle));

            assetBundle.Unload(false);
        }
    }
}
