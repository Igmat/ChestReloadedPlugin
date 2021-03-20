using ChestReloaded.Util;
using HarmonyLib;
using UnityEngine;

namespace ChestReloaded.Hooks.Patches
{
    [HarmonyPatch(typeof(Player), "Update")]
    static class PlayerUpdate
    {
        private static bool isAltHold = false;

        private static bool AlternativeInteract(Player player, GameObject go, bool hold)
        {
            // it's a copy of original restrictions on "Interact" calls
            if (player.InAttack() || player.InDodge() || (hold && Time.time - player.m_lastHoverInteractTime < 0.2f))
                return true;

            if (!Helpers.IsSignAndOtherInteractable(go)) return true;
            var signComponentInParent = go.GetComponentInParent<Sign>();

            Log.LogInfo("Alternative Interact");
            player.m_lastHoverInteractTime = Time.time;
            if (signComponentInParent.Interact(player, hold))
            {
                // ((Humanoid)player) is used because in original class used call to base
                // IDK if it matters in this context
                Vector3 forward = go.transform.position - ((Humanoid)player).transform.position;
                forward.y = 0f;
                forward.Normalize();
                ((Humanoid)player).transform.rotation = Quaternion.LookRotation(forward);
                player.m_zanim.SetTrigger("interact");
            }

            return false; // if we got this far then it's our special cased sign, so we should skip other "Interact" calls
        }

        public static bool Prefix(Player __instance)
        {
            var player = __instance;
            if (Input.GetKeyDown(KeyCode.LeftAlt))
                isAltHold = true;
            if (Input.GetKeyUp(KeyCode.LeftAlt))
                isAltHold = false;

            if (ZInput.GetButtonDown("Use") && isAltHold)
            {
                Log.LogInfo("Alternative Use");
                var hoverObject = player.GetHoverObject();
                if (hoverObject != null)
                    return AlternativeInteract(player, hoverObject, false);
            }

            return true; // if we got here then original method should be called
        }
    }
}
