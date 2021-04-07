
using ValheimLib;

namespace ChestReloaded
{
    static class LanguageData
    {
        public static string alternativeUse
        {
            get
            {
                return "\n" + Localization.instance.Localize("[<color=yellow><b>ALT + $KEY_Use</b></color>] $piece_signed_locker_rename");
            }
        }
        public static string lockerItemUse
        {
            get
            {
                return "\n" + Localization.instance.Localize("[<color=yellow><b>ALT + 1-8</b></color>] $piece_locker_assign_item");
            }
        }
        public static string signItemUse
        {
            get
            {
                return "\n" + Localization.instance.Localize("[<color=yellow><b>ALT + 1-8</b></color>] $piece_sign_assign_item");
            }
        }

        public static void Init()
        {
            Language.AddToken("$big_wooden_signed_locker", "Big Wooden Locker");
            Language.AddToken("$wooden_signed_locker", "Signed Locker");
            Language.AddToken("$iron_signed_locker", "Iron Signed Locker");
            Language.AddToken("$stone_signed_locker", "Stone Signed Locker");
            Language.AddToken("$piece_signed_locker_rename", "Rename locker");
            Language.AddToken("$piece_locker_assign_item", "Assign item to locker");
            Language.AddToken("$piece_sign_assign_item", "Assign item to sign");
        }
    }
}
