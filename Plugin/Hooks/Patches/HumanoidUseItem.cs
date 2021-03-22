
using ChestReloaded.Util;
using HarmonyLib;
using UnityEngine;

namespace ChestReloaded.Hooks.Patches
{
	[HarmonyPatch(typeof(Humanoid), "UseItem")]
	static class HumanoidUseItem
    {
		public static void Postfix(Humanoid __instance, Inventory inventory, ItemDrop.ItemData item, bool fromInventoryGui)
		{
			GameObject hoverObject = __instance.GetHoverObject();
			Hoverable hoverable = (hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null);
			if (hoverable != null && !fromInventoryGui && PlayerUpdate.isAltHold)
			{
				if (!Helpers.IsSignAndOtherInteractable(hoverObject)) return;
				var signComponentInParent = hoverObject.GetComponentInParent<Sign>();

				Log.LogInfo("Alternative Use Item");
				signComponentInParent.UseItem(__instance, item);
			}
		}
	}
}
