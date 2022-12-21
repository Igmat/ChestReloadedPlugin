// ChestReloaded
// a Valheim mod skeleton using Jötunn
// 
// File:    ChestReloaded.cs
// Project: ChestReloaded

using BepInEx;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;

namespace ChestReloaded
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class ChestReloaded : BaseUnityPlugin
    {
        public const string AuthorName = "Igmat";
        public const string PluginName = "ChestReloaded";
        public const string PluginGUID = AuthorName + "." + PluginName;
        public const string PluginVersion = "1.0.0";
        
        // Use this class to add your own localization to the game
        // https://valheim-modding.github.io/Jotunn/tutorials/localization.html
        public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();

        internal static ChestReloaded Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            // Jotunn comes with its own Logger class to provide a consistent Log style for all mods using it
            Jotunn.Logger.LogInfo("ChestReloaded has landed");
            Jotunn.Logger.LogInfo("version: " + PluginVersion);

            ConfigData.Init(Config);
            Jotunn.Logger.LogInfo("Configuration initialized");

            Hooks.Patch.Init();
            Jotunn.Logger.LogInfo("Patches initialized");

            Pieces.Lockers.Init();
            Jotunn.Logger.LogInfo("Pieces initialized");
            
            // To learn more about Jotunn's features, go to
            // https://valheim-modding.github.io/Jotunn/tutorials/overview.html
        }


        /// <summary>
        /// Destroying the attached Behaviour will result in the game or Scene receiving OnDestroy.
        /// OnDestroy occurs when a Scene or game ends.
        /// It is also called when your mod is unloaded, this is where you do clean up of hooks, harmony patches,
        /// loose GameObjects and loose monobehaviours.
        /// Loose here refers to gameobjects not attached
        /// to the parent BepIn GameObject where your BaseUnityPlugin is attached
        /// </summary>
        private void OnDestroy()
        {
            Hooks.Patch.Disable();
        }
    }
}

