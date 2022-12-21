using ChestReloaded.Util;
using UnityEngine;
using System;
using BepInEx.Configuration;
using UnityObject = UnityEngine.Object;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;

namespace ChestReloaded.Pieces
{
    abstract class AbstractLocker
    {
        public abstract string Name { get; }
        public abstract string PrefabName { get; }
        public virtual string GameObjectName => ChestReloaded.PluginGUID + "." + PrefabName;
        protected abstract ContainerSize CalculateBalancedSize();
        protected abstract ConfigEntry<int> height { get; }
        protected abstract ConfigEntry<int> width { get; }
        protected virtual ConfigEntry<bool> isBalanced => ConfigData.isBalanced;

        protected Container container;

        public AbstractLocker(AssetBundle assetBundle)
        {
            var assetName = "Assets/Pieces/" + PrefabName + ".prefab";
            GameObject lockerObject = assetBundle.LoadAsset<GameObject>(assetName);
            lockerObject.name = GameObjectName;            
            container = lockerObject.GetComponent<Container>();
            RecalculateSize();
            isBalanced.SettingChanged += (object sender, EventArgs e) => RecalculateSize();
            height.SettingChanged += (object sender, EventArgs e) => RecalculateSize();
            width.SettingChanged += (object sender, EventArgs e) => RecalculateSize();

            //PieceConfig pieceConfig = new PieceConfig();
            //pieceConfig.Name = GameObjectName;
            //pieceConfig.PieceTable = "Hammer";
            //pieceConfig.Category = "Storage";

            PieceManager.Instance.AddPiece(new CustomPiece(lockerObject, "Hammer", fixReference: true));

            Jotunn.Logger.LogInfo("Register " + Name + " as " + GameObjectName);
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
