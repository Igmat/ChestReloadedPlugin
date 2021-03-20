using UnityEngine;

namespace ChestReloaded.Util
{
    static class Helpers
    {
        public static bool IsSignAndOtherInteractable(GameObject go)
        {
            var interactableComponentInParent = go.GetComponentInParent<Interactable>();
            if (interactableComponentInParent == null)
                return false; // absence of interactable covered by original code

            var signComponentInParent = go.GetComponentInParent<Sign>();
            if (signComponentInParent == null)
                return false; // one non-sign interactable covered by original code

            if ((object)signComponentInParent == interactableComponentInParent)
                return false; // one sign interactable covered by original code

            return true;
        }
    }
}
