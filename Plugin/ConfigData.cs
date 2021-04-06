using BepInEx;
using BepInEx.Configuration;

namespace ChestReloaded
{
    static class ConfigData
    {
        public static ConfigEntry<int> signedLockerWidth;
        public static ConfigEntry<int> signedLockerHeight;
        public static ConfigEntry<int> bigWoodenLockerWidth;
        public static ConfigEntry<int> bigWoodenLockerHeight;
        public static ConfigEntry<bool> isBalanced;

        public static void Init(ConfigFile configFile)
        {
            isBalanced = configFile.Bind("",
                                         "Balanced sizes",
                                         true,
                                         "Custom lockers have balanced sizes according to their price relative to vanilla containers.\nRequire world restart.");

            signedLockerWidth = configFile.Bind("Signed Locker",
                                                "Width",
                                                8,
                                                new ConfigDescription("Width of Signed Locker.\nRequire world restart.", new AcceptableValueRange<int>(1, 8)));
            signedLockerHeight = configFile.Bind("Signed Locker",
                                                "Height",
                                                2,
                                                new ConfigDescription("Height of Signed Locker.\nRequire world restart.", new AcceptableValueRange<int>(1, 20)));

            bigWoodenLockerWidth = configFile.Bind("Big Wooden Locker",
                                                "Width",
                                                5,
                                                new ConfigDescription("Width of Big Wooden Locker.\nRequire world restart.", new AcceptableValueRange<int>(1, 8)));
            bigWoodenLockerHeight = configFile.Bind("Big Wooden Locker",
                                                "Height",
                                                4,
                                                new ConfigDescription("Height of Big Wooden Locker.\nRequire world restart.", new AcceptableValueRange<int>(1, 20)));
        }
    }
}
