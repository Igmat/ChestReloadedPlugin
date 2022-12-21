
namespace ChestReloaded
{
    static class LocalizationHelper
    {
        private static string KEY_Use => ChestReloaded.Localization.TryTranslate("$KEY_Use");
        private static string piece_signed_locker_rename => ChestReloaded.Localization.TryTranslate("$piece_signed_locker_rename");
        private static string piece_locker_assign_item => ChestReloaded.Localization.TryTranslate("$piece_locker_assign_item");
        private static string piece_sign_assign_item => ChestReloaded.Localization.TryTranslate("$piece_sign_assign_item");

        public static string alternativeUse => $"\n[<color=yellow><b>ALT + {KEY_Use}</b></color>] {piece_signed_locker_rename}";
        public static string lockerItemUse => $"\n[<color=yellow><b>ALT + 1-8</b></color>] {piece_locker_assign_item}";
        public static string signItemUse => $"\n[<color=yellow><b>ALT + 1-8</b></color>] {piece_sign_assign_item}";
    }
}
