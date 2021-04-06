using ChestReloaded.Util;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValheimLib;
using BepInEx.Configuration;
using UnityObject = UnityEngine.Object;

namespace ChestReloaded.Pieces
{
    abstract class AbstractLocker
    {
        public abstract string Name { get; }
        public abstract string PrefabName { get; }
        public virtual string GameObjectName => Plugin.ModGuid + "." + PrefabName;
        protected abstract ContainerSize CalculateBalancedSize();
        protected abstract ConfigEntry<int> height { get; }
        protected abstract ConfigEntry<int> width { get; }
        protected virtual ConfigEntry<bool> isBalanced => ConfigData.isBalanced;

        protected Container container;

        public AbstractLocker(AssetBundle assetBundle)
        {
            GameObject lockerObject = assetBundle.LoadAsset<GameObject>("Assets/Pieces/" + PrefabName + ".prefab");
            lockerObject.FixReferences();
            lockerObject.name = GameObjectName;
            lockerObject.NetworkRegister();
            container = lockerObject.GetComponent<Container>();
            RecalculateSize();
            isBalanced.SettingChanged += (object sender, EventArgs e) => RecalculateSize();
            height.SettingChanged += (object sender, EventArgs e) => RecalculateSize(); ;
            width.SettingChanged += (object sender, EventArgs e) => RecalculateSize(); ;


            Helpers.HammerTable.m_pieces.Add(lockerObject);
            Log.LogInfo("Register " + Name + " as " + GameObjectName);
        }

        protected virtual void RecalculateSize()
        {
            var size = (isBalanced.Value)
                ? CalculateBalancedSize()
                : new ContainerSize()
                {
                    Height = height.Value,
                    Width = width.Value
                };
            container.m_height = size.Height;
            container.m_width = size.Width;
        }
    }
}
