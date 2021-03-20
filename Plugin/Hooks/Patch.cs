using ChestReloaded.Util;
using HarmonyLib;

namespace ChestReloaded.Hooks
{
    internal static class Patch
    {
        internal static Harmony HarmonyInstance;

        public static void Init()
        {
            HarmonyInstance = new Harmony(Plugin.ModGuid);
            HarmonyInstance.PatchAll();
        }

        public static void Disable()
        {
            HarmonyInstance.UnpatchSelf();
        }
    }
}
