
using ValheimLib;

namespace ChestReloaded
{
    class LanguageData
    {
        public static string alternativeUse
        {
            get
            {
                return "\n" + Localization.instance.Localize("[<color=yellow><b>ALT + $KEY_Use</b></color>] $piece_signed_chest_rename");
            }
        }
        public static string signItemUse
        {
            get
            {
                return "\n" + Localization.instance.Localize("[<color=yellow><b>ALT + 1-8</b></color>] $piece_signed_chest_rename");
            }
        }

        public static void Init()
        {
            Language.AddToken("$wooden_signed_locker", "Signed Locker");
            Language.AddToken("$stone_signed_locker", "Stone Signed Locker");
            Language.AddToken("$metal_signed_locker", "Metal Signed Locker");
            Language.AddToken("$piece_signed_chest_rename", "Rename locker");
        }
    }
}
