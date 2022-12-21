using UnityEngine;
using ChestReloaded.Util;
using System.Collections.Generic;
using System;
using Jotunn.Utils;
using Jotunn.Managers;

namespace ChestReloaded.Pieces
{
    static class Lockers
    {
        public static List<AbstractLocker> lockers = new List<AbstractLocker>();
        public static void Init()
        {
            PrefabManager.OnVanillaPrefabsAvailable += AddPieces;
        }

        private static void AddPieces()
        {
            AssetBundle assetBundle = AssetUtils.LoadAssetBundleFromResources("signed_chest");
            Jotunn.Logger.LogInfo("Pieces assets loaded");
            lockers.Add(new BigWoodenLocker(assetBundle));
            Jotunn.Logger.LogInfo("BigWoodenLocker loaded");
            lockers.Add(new SignedLocker(assetBundle));
            Jotunn.Logger.LogInfo("SignedLocker loaded");
            lockers.Add(new IronLocker(assetBundle));
            Jotunn.Logger.LogInfo("IronLocker loaded");
            lockers.Add(new HiddenStoneLocker(assetBundle));
            Jotunn.Logger.LogInfo("HiddenStoneLocker loaded");

            assetBundle.Unload(false);
            PrefabManager.OnVanillaPrefabsAvailable -= AddPieces;
        }
    }
}
