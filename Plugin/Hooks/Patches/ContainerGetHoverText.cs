using HarmonyLib;

namespace ChestReloaded.Hooks.Patches
{
    [HarmonyPatch(typeof(Container), "GetHoverText")]
    static class ContainerGetHoverText
    {
        public static string Postfix(string __result, Container __instance)
        {
            var playerID = Game.instance.GetPlayerProfile().GetPlayerID();
            if (__instance.name.Contains(Pieces.HiddenStoneLocker.LockerName) && !__instance.CheckAccess(playerID)) return "";

            var maybe_Sign = __instance.gameObject.GetComponent<Sign>();
            return (maybe_Sign != null)
                ? __result + LanguageData.alternativeUse + LanguageData.lockerItemUse
                : __result;
        }
    }
}
