
using ChestReloaded.Util;
using HarmonyLib;
using UnityEngine;

namespace ChestReloaded.Hooks.Patches
{
	[HarmonyPatch(typeof(Humanoid), "UseItem")]
	static class HumanoidUseItem
    {
		public static bool Prefix(Humanoid __instance, Inventory inventory, ItemDrop.ItemData item, bool fromInventoryGui)
		{
			GameObject hoverObject = __instance.GetHoverObject();
			Hoverable hoverable = (hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null);
			if (hoverable == null || fromInventoryGui || !PlayerUpdate.isAltHold) return true;
			var signComponentInParent = hoverObject.GetComponentInParent<Sign>();
			if (signComponentInParent == null) return true;

			Log.LogInfo("Alternative Use Item");
			signComponentInParent.UseItem(__instance, item);
			return false;
		}
	}
}
