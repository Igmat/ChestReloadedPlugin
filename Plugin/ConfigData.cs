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
        public static ConfigEntry<int> ironLockerWidth;
        public static ConfigEntry<int> ironLockerHeight;
        public static ConfigEntry<int> hiddenStoneLockerWidth;
        public static ConfigEntry<int> hiddenStoneLockerHeight;
        public static ConfigEntry<int> nexusID;
        public static ConfigEntry<bool> isBalanced;

        public static void Init(ConfigFile configFile)
        {
            nexusID = configFile.Bind("General",
                                        "NexusID",
                                        653,
                                        new ConfigDescription("Nexus mod ID for updates", null, new ConfigurationManagerAttributes { IsAdvanced = true, ReadOnly = true }));
            isBalanced = configFile.Bind("",
                                         "Balanced sizes",
                                         true,
                                         "Custom lockers have balanced sizes according to their price relative to vanilla containers.\nRequire world restart.");

            signedLockerWidth = configFile.Bind("Signed Locker",
                                                "Width",
                                                4,
                                                new ConfigDescription("Width of Signed Locker.\nRequire world restart.", new AcceptableValueRange<int>(1, 8)));
            signedLockerHeight = configFile.Bind("Signed Locker",
                                                "Height",
                                                4,
                                                new ConfigDescription("Height of Signed Locker.\nRequire world restart.", new AcceptableValueRange<int>(1, 20)));

            bigWoodenLockerWidth = configFile.Bind("Big Wooden Locker",
                                                "Width",
                                                5,
                                                new ConfigDescription("Width of Big Wooden Locker.\nRequire world restart.", new AcceptableValueRange<int>(1, 8)));
            bigWoodenLockerHeight = configFile.Bind("Big Wooden Locker",
                                                "Height",
                                                4,
                                                new ConfigDescription("Height of Big Wooden Locker.\nRequire world restart.", new AcceptableValueRange<int>(1, 20)));

            ironLockerWidth = configFile.Bind("Iron Locker",
                                                "Width",
                                                7,
                                                new ConfigDescription("Width of Iron Locker.\nRequire world restart.", new AcceptableValueRange<int>(1, 8)));
            ironLockerHeight = configFile.Bind("Iron Locker",
                                                "Height",
                                                4,
                                                new ConfigDescription("Height of Iron Locker.\nRequire world restart.", new AcceptableValueRange<int>(1, 20)));

            hiddenStoneLockerWidth = configFile.Bind("Hidden Stone Locker",
                                                "Width",
                                                7,
                                                new ConfigDescription("Width of Hidden Stone Locker.\nRequire world restart.", new AcceptableValueRange<int>(1, 8)));
            hiddenStoneLockerHeight = configFile.Bind("Hidden Stone Locker",
                                                "Height",
                                                5,
                                                new ConfigDescription("Height of Hidden Stone Locker.\nRequire world restart.", new AcceptableValueRange<int>(1, 20)));
        }
    }
}
