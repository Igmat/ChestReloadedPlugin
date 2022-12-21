using ChestReloaded.Util;
using HarmonyLib;

namespace ChestReloaded.Hooks
{
    static class Patch
    {
        internal static Harmony HarmonyInstance;

        public static void Init()
        {
            HarmonyInstance = new Harmony(ChestReloaded.PluginGUID);
            HarmonyInstance.PatchAll();
        }

        public static void Disable()
        {
            HarmonyInstance.UnpatchSelf();
        }
    }
}
