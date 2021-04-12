using HarmonyLib;
using System;
using System.Linq;

namespace ChestReloaded.Hooks.Patches
{
    [HarmonyPatch(typeof(Sign))]
    static class SignClass
    {
        private struct SignData
        {
            public string description { get; set; }
            public int[] items { get; private set; }

            private static readonly string startSymbol = "${";
            private static readonly string endSymbol = "}$";
            private static readonly string[] separator = new string[] { ";" };

            private static readonly string displayedStartSymbol = " (";
            private static readonly string displayedendSymbol = ")";
            private static readonly string[] displayedSeparator = new string[] { " & " };

            public string serialized => description + startSymbol + items.Distinct().Join(delimiter: separator[0]) + endSymbol;

            public string localized => items.Length > 0
                ? description
                    + displayedStartSymbol
                    + Localization.instance.Localize(
                        items
                            .Select(item => ObjectDB.instance.GetItemPrefab(item).GetComponent<ItemDrop>().m_itemData.m_shared.m_name)
                            .Join(delimiter: displayedSeparator[0]))
                    + displayedendSymbol
                : description;

            public SignData(string text)
            {
                var startOfItemNames = text.IndexOf(startSymbol) + startSymbol.Length;
                var length = text.IndexOf(endSymbol) - startOfItemNames;
                if (startOfItemNames == startSymbol.Length - 1
                    || length < 0)
                {
                    description = text;
                    items = new int[0];
                }
                else
                {
                    description = text.Substring(0, startOfItemNames - startSymbol.Length);
                    items = text
                        .Substring(startOfItemNames, length)
                        .Split(separator, StringSplitOptions.RemoveEmptyEntries)
                        .Select(item => int.Parse(item))
                        .ToArray();
                }
            }
            public void ToggleItem(int item)
            {
                items = (!items.Contains(item))
                    ? items.Concat(new[] { item }).Distinct().ToArray()
                    : items.Where(existing => existing != item).ToArray();
            }
        }

        [HarmonyPatch("UseItem")]
        [HarmonyPostfix]
        public static bool UseItem(bool __result, Sign __instance, Humanoid user, ItemDrop.ItemData item)
        {
            if (!PlayerUpdate.isAltHold) return __result;

            Log.LogInfo("Use Item on Sign");
            var data = __instance.GetData();
            var addedItem = item.m_dropPrefab.name.GetStableHashCode();
            data.ToggleItem(addedItem);

            __instance.SetData(data);

            return __result;
        }

        [HarmonyPatch("GetHoverText")]
        [HarmonyPostfix]
        public static string GetHoverText(string __result)
        {
            return __result + LanguageData.signItemUse;
        }

        [HarmonyPatch("GetText")]
        [HarmonyPostfix]
        public static string GetText(string __result, Sign __instance)
        {
            return __instance.GetData().description;
        }

        [HarmonyPatch("UpdateText")]
        [HarmonyPrefix]
        public static bool UpdateText(Sign __instance)
        {
            var text = __instance.GetData().localized;
            if (!(__instance.m_textWidget.text == text))
            {
                Log.LogMessage(__instance.m_textWidget.text + " changed to " + text);
                __instance.m_textWidget.text = text;
            }
            return false;
        }

        [HarmonyPatch("SetText")]
        [HarmonyPrefix]
        public static bool SetText(Sign __instance, string text)
        {
            if (PrivateArea.CheckAccess(__instance.transform.position))
            {
                var data = __instance.GetData();
                __instance.m_nview.ClaimOwnership();
                data.description = text;
                __instance.m_textWidget.text = data.localized;
                __instance.SetData(data);

                return false;
            }
            return true;
        }

        private static SignData GetData(this Sign sign)
        {
            return new SignData(sign.m_nview.GetZDO().GetString("text", sign.m_defaultText));
        }

        private static void SetData(this Sign sign, SignData data)
        {
            sign.m_nview.GetZDO().Set("text", data.serialized);
        }
    }
}
