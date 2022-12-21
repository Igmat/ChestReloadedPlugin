using HarmonyLib;
using Jotunn.Managers;
using System;
using System.Linq;
using UnityEngine.PlayerLoop;
using static Mono.Math.BigInteger;

namespace ChestReloaded.Hooks.Patches
{
    [HarmonyPatch(typeof(Sign))]
    static class SignClass
    {
        private static readonly string restrictedText = "ᚬᛏᛁᛚᛚᚴᛅᚾᚴᛚᛁᚴ";
        private struct SignData
        {
            public string description { get; set; }
            public string[] items { get; private set; }

            private static readonly string startSymbol = "${";
            private static readonly string endSymbol = "}$";
            private static readonly string[] separator = new string[] { ";" };

            private static readonly string displayedStartSymbol = " (";
            private static readonly string displayedEndSymbol = ")";
            private static readonly string[] displayedSeparator = new string[] { " & " };

            public string serialized => description + startSymbol + items.Distinct().Join(delimiter: separator[0]) + endSymbol;

            public string localized => items.Length > 0
                ? description
                    + displayedStartSymbol
                    + Localization.instance.Localize(
                        items
                            .Select(item => {
                                var prefab = PrefabManager.Instance.GetPrefab(item);
                                return prefab != null
                                    ? prefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_name
                                    : "";
                            })
                            .Join(delimiter: displayedSeparator[0]))
                    + displayedEndSymbol
            : description;

            public bool isUpdated;

            public SignData(string serializedText, bool updated = false)
            {
                isUpdated = updated;
                if (updated)
                {
                    var startOfItemNames = serializedText.IndexOf(startSymbol) + startSymbol.Length;
                    var length = serializedText.IndexOf(endSymbol) - startOfItemNames;
                    if (startOfItemNames == startSymbol.Length - 1
                        || length < 0)
                    {
                        description = serializedText;
                        items = new string[0];
                    }
                    else
                    {
                        description = serializedText.Substring(0, startOfItemNames - startSymbol.Length);
                        items = serializedText
                            .Substring(startOfItemNames, length)
                            .Split(separator, StringSplitOptions.RemoveEmptyEntries)
                            .ToArray();
                    }
                } else
                {
                    description = serializedText;
                    items = new string[0];
                }
            }
            public void ToggleItem(string item)
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

            Jotunn.Logger.LogInfo("Use Item on Sign");
            var data = __instance.GetData(true);
            var addedItem = item.m_dropPrefab.name;
            data.ToggleItem(addedItem);

            __instance.SetData(data);

            return __result;
        }

        [HarmonyPatch("SetText")]
        [HarmonyPrefix]
        public static bool SetText(Sign __instance, string text)
        {
            if (PrivateArea.CheckAccess(__instance.transform.position))
            {
                __instance.m_nview.ClaimOwnership();
                var data = __instance.GetData(true);
                data.description = text;
                __instance.SetData(data);
                __instance.UpdateText();

                return false;
            }
            return true;
        }

        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        public static void Awake(Sign __instance)
        {
            Jotunn.Logger.LogMessage("Awaken");
            __instance.m_nview.GetZDO().Set("updated", true);
        }

        [HarmonyPatch("UpdateText")]
        [HarmonyPrefix]
        public static bool UpdateText(Sign __instance)
        {
            var data = __instance.GetData();
            var currentText = __instance.m_currentText;
            if (data.isUpdated || currentText.Length == 0 || currentText == restrictedText)
            {
                // if current text is restricted we have to rechek access again in case it changed
                var text = currentText == restrictedText
                    ? ""
                    : data.localized;
                if (currentText != text)
                {
                    PrivilegeManager.CanViewUserGeneratedContent(__instance.m_nview.GetZDO().GetString("author"), OnResult);
                }
                else
                {
                    // text didn't changed so we can skip future redundant checks untill it'll be updated again
                    __instance.m_nview.GetZDO().Set("updated", false);
                }

                void OnResult(PrivilegeManager.Result access)
                {
                    switch (access)
                    {
                        case PrivilegeManager.Result.Allowed:
                            // if current text is restricted and we are here then we can finally calculate localized version of it
                            // otherwise we already calculated it to check if it changed comparing to already rendered
                            text = currentText == restrictedText
                                ? data.localized
                                : text;
                            Jotunn.Logger.LogMessage($"{__instance.m_textWidget.text} updating to {text}");
                            __instance.m_currentText = text;
                            __instance.m_textWidget.text = text;
                            __instance.m_isViewable = true;
                            break;
                        case PrivilegeManager.Result.NotAllowed:
                            Jotunn.Logger.LogMessage($"{__instance.m_textWidget.text} updating to {restrictedText}");
                            __instance.m_currentText = restrictedText;
                            __instance.m_textWidget.text = restrictedText;
                            __instance.m_isViewable = false;
                            break;
                        default:
                            Jotunn.Logger.LogMessage($"{__instance.m_textWidget.text} updating to {restrictedText}");
                            __instance.m_currentText = restrictedText;
                            __instance.m_textWidget.text = restrictedText;
                            __instance.m_isViewable = false;
                            ZLog.LogError("Failed to check UGC privilege");
                            break;
                    }
                    __instance.m_nview.GetZDO().Set("updated", false);
                }
            }

            return false;
        }

        [HarmonyPatch("GetHoverText")]
        [HarmonyPostfix]
        public static string GetHoverText(string __result)
        {
            return __result + LocalizationHelper.signItemUse;
        }

        [HarmonyPatch("GetText")]
        [HarmonyPostfix]
        public static string GetText(string __result, Sign __instance)
        {
            return __instance.GetData(true).description;
        }

        private static SignData GetData(this Sign sign, bool force = false)
        {
            var updated = force || sign.m_nview.GetZDO().GetBool("updated", false);
            var text = updated
                ? sign.m_nview.GetZDO().GetString("text", sign.m_defaultText)
                : "";
            return new SignData(text, updated);
        }

        private static void SetData(this Sign sign, SignData data)
        {
            sign.m_nview.GetZDO().Set("updated", true);
            sign.m_nview.GetZDO().Set("text", data.serialized);
            sign.m_nview.GetZDO().Set("author", PrivilegeManager.GetNetworkUserId());
        }
    }
}
