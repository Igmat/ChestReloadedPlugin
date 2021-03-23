using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChestReloaded.Hooks.Patches
{
    [HarmonyPatch(typeof(Sign))]
    class SignClass
    {
        private static string[] separator = new[] { " & " };

        [HarmonyPatch("UseItem")]
        [HarmonyPostfix]
        public static void UseItem(Sign __instance, Humanoid user, ItemDrop.ItemData item)
        {
            Log.LogInfo("Use Item on Sign");
            var currentText = __instance.GetText();
            var description = GetDescriptionText(currentText);
            var currentItems = GetItemNames(currentText);
            var addedItem = item.m_shared.m_name;
            var items = currentItems.Concat(new[] { addedItem }).Distinct();
            var localizedItems = Localization.instance.Localize(items.Join(delimiter: separator[0]));

            var result = description + " (" + localizedItems + ")";
            

            __instance.SetText(result);
        }

        [HarmonyPatch("GetHoverText")]
        [HarmonyPostfix]
        public static string GetHoverText(string __result)
        {
            return __result + LanguageData.signItemUse;
        }

        private static string GetDescriptionText(string text)
        {
            var startOfItemNames = text.IndexOf('(') + 1;
            if (startOfItemNames == 0) return text;
            var length = text.IndexOf(')') - startOfItemNames;
            if (length < 0) return text;

            return text.Substring(0, startOfItemNames - 1);
        }

        private static string[] GetItemNames(string text)
        {
            var startOfItemNames = text.IndexOf('(') + 1;
            if (startOfItemNames == 0) return new string[0];
            var length = text.IndexOf(')') - startOfItemNames;
            if (length <= 0) return new string[0];

            return text.Substring(startOfItemNames, length).Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
