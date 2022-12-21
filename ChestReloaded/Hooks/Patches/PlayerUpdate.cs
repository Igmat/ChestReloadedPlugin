using ChestReloaded.Util;
using HarmonyLib;
using UnityEngine;

namespace ChestReloaded.Hooks.Patches
{
    [HarmonyPatch(typeof(Player), "Update")]
    static class PlayerUpdate
    {
        internal static bool isAltHold = false;

        private static bool AlternativeInteract(Player player, GameObject go, bool hold)
        {
            // it's a copy of original restrictions on "Interact" calls
            if (player.InAttack() || player.InDodge() || (hold && Time.time - player.m_lastHoverInteractTime < 0.2f))
                return true;

            if (!Helpers.IsSignAndOtherInteractable(go)) return true;
            var signComponentInParent = go.GetComponentInParent<Sign>();

            Jotunn.Logger.LogInfo("Alternative Interact");
            player.m_lastHoverInteractTime = Time.time;
            if (signComponentInParent.Interact(player, hold, false))
            {
                Vector3 forward = go.transform.position - player.transform.position;
                forward.y = 0f;
                forward.Normalize();
                player.transform.rotation = Quaternion.LookRotation(forward);
                player.m_zanim.SetTrigger("interact");
            }

            return false; // if we got this far then it's our special cased sign, so we should skip other "Interact" calls
        }

        public static bool Prefix(Player __instance)
        {
            var player = __instance;
            isAltHold = Input.GetKey(KeyCode.LeftAlt);

            if (ZInput.GetButtonDown("Use") && isAltHold)
            {
                Jotunn.Logger.LogInfo("Alternative Use");
                var hoverObject = player.GetHoverObject();
                if (hoverObject != null)
                    return AlternativeInteract(player, hoverObject, false);
            }

            return true; // if we got here then original method should be called
        }
    }
}
