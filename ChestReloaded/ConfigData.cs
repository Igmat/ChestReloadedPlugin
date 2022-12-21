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
            configFile.SaveOnConfigSet = true;

            nexusID = configFile.Bind("General",
                                        "NexusID",
                                        653,
                                        new ConfigDescription("Nexus mod ID for updates", null, new ConfigurationManagerAttributes { IsAdvanced = true, ReadOnly = true }));
            isBalanced = configFile.Bind("",
                                         "Balanced sizes",
                                         true,
                                         description("Custom lockers have balanced sizes according to their price relative to vanilla containers.\nRequire world restart."));

            signedLockerWidth = configFile.Bind("Signed Locker",
                                                "Width",
                                                4,
                                                rangeDescription("Width of Signed Locker.\nRequire world restart.", 1, 8));
            signedLockerHeight = configFile.Bind("Signed Locker",
                                                "Height",
                                                4,
                                                rangeDescription("Height of Signed Locker.\nRequire world restart.", 1, 20));

            bigWoodenLockerWidth = configFile.Bind("Big Wooden Locker",
                                                "Width",
                                                5,
                                                rangeDescription("Width of Big Wooden Locker.\nRequire world restart.", 1, 8));
            bigWoodenLockerHeight = configFile.Bind("Big Wooden Locker",
                                                "Height",
                                                4,
                                                rangeDescription("Height of Big Wooden Locker.\nRequire world restart.", 1, 20));

            ironLockerWidth = configFile.Bind("Iron Locker",
                                                "Width",
                                                7,
                                                rangeDescription("Width of Iron Locker.\nRequire world restart.", 1, 8));
            ironLockerHeight = configFile.Bind("Iron Locker",
                                                "Height",
                                                4,
                                                rangeDescription("Height of Iron Locker.\nRequire world restart.", 1, 20));

            hiddenStoneLockerWidth = configFile.Bind("Hidden Stone Locker",
                                                "Width",
                                                7,
                                                rangeDescription("Width of Hidden Stone Locker.\nRequire world restart.", 1, 8));
            hiddenStoneLockerHeight = configFile.Bind("Hidden Stone Locker",
                                                "Height",
                                                5,
                                                rangeDescription("Height of Hidden Stone Locker.\nRequire world restart.", 1, 20));
        }

        private static ConfigDescription description(string desc)
        {
            return new ConfigDescription(desc, null, new ConfigurationManagerAttributes { IsAdminOnly = true });
        }
        private static ConfigDescription rangeDescription(string desc, int min, int max)
        {
            return new ConfigDescription(desc, new AcceptableValueRange<int>(min, max), new ConfigurationManagerAttributes { IsAdminOnly = true });
        }
    }
}
