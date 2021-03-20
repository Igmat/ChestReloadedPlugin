﻿using BepInEx;
using BepInEx.Configuration;

namespace ChestReloaded
{
    [BepInDependency(ValheimLib.ValheimLib.ModGuid)]
    [BepInPlugin(ModGuid, ModName, ModVer)]
    public class Plugin : BaseUnityPlugin
    {
        public const string ModGuid = AuthorName + "." + ModName;
        private const string AuthorName = "Igmat";
        private const string ModName = "ChestReloaded";
        private const string ModVer = "0.0.1";

        internal static Plugin Instance { get; private set; }

        /// <summary>
        /// Awake is called when the script instance is being loaded. 
        /// </summary>
        private void Awake()
        {
            Instance = this;

            Log.Init(Logger);
            LanguageData.Init();

            Hooks.Patch.Init();
            Pieces.SignedLocker.Init();
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