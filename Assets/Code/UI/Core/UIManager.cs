using System;
using System.Collections.Generic;
using Code.Core;
using Code.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Core
{
    public class UIManager : SingletonMono<UIManager>
    {
        public const string ConfigPath = "Assets/GameAssets/Prefabs/UI/UIManagerConfig.asset";

        public Camera UICamera;
        public Canvas Canvas;
        private readonly Dictionary<UIType, UIConfig> _uiConfigs = new();

        private readonly Dictionary<UILayerType, UILayer> _uiLayers = new();

        protected override void Initialize()
        {
            Canvas.gameObject.GetOrAddComponent<CanvasScaler>();

            var uiLayers = new List<UILayer>
            {
                new(new LayerConfig
                {
                    LayerType = UILayerType.SceneLayer, ClearOnSceneLoad = true, Parent = Canvas.transform,
                    UseScreenSpace = true
                }),
                new(new LayerConfig
                {
                    LayerType = UILayerType.Background, ClearOnSceneLoad = true, Parent = Canvas.transform,
                    UseScreenSpace = true
                }),
                new(new LayerConfig
                {
                    LayerType = UILayerType.Normal, ClearOnSceneLoad = true, Parent = Canvas.transform,
                    UseScreenSpace = true
                }),
                new(new LayerConfig
                {
                    LayerType = UILayerType.Popup, ClearOnSceneLoad = true, Parent = Canvas.transform,
                    UseScreenSpace = true
                }),
                new(new LayerConfig
                {
                    LayerType = UILayerType.Tips, ClearOnSceneLoad = true, Parent = Canvas.transform,
                    UseScreenSpace = true
                }),
                new(new LayerConfig
                {
                    LayerType = UILayerType.Top, ClearOnSceneLoad = false, Parent = Canvas.transform,
                    UseScreenSpace = true
                })
            };

            foreach (var layer in uiLayers)
            {
                _uiLayers.Add(layer.LayerType, layer);
            }
        }

        public void Init()
        {
            var config = AssetDatabase.LoadAssetAtPath<UIManagerConfig>(ConfigPath);
            if (config == null)
            {
                throw new Exception("UIManagerConfig not found");
            }

            foreach (var row in config.rows)
            {
                _uiConfigs[row.UIType] = row;
            }
        }

        public UIBase OpenUI(UIType uiType, UIArgs args = default)
        {
            if (!_uiConfigs.TryGetValue(uiType, out var uiConfig))
            {
                throw new ArgumentException($"UIType {uiType} not found");
            }

            if (!_uiLayers.TryGetValue(uiConfig.Layer, out var layer))
            {
                throw new ArgumentException($"LayerType {uiType} not found");
            }

            return layer.OpenUI(uiConfig, args);
        }


        public void CloseUI(UIType uiType)
        {
            if (!_uiConfigs.TryGetValue(uiType, out var config))
            {
                throw new ArgumentException($"UIType {uiType} not found");
            }

            if (_uiLayers.TryGetValue(config.Layer, out var value))
            {
                value.CloseUI(config);
            }
        }

        /// <summary>
        /// 场景加载时清理特定层级
        /// </summary>
        public void OnSceneLoaded()
        {
            foreach (var (k, config) in _uiLayers)
            {
                if (config.ClearOnSceneLoad)
                {
                    ClearLayer(config.LayerType);
                }
            }
        }

        /// <summary>
        /// 清理指定层级的UI
        /// </summary>
        /// <param name="layerType"></param>
        public void ClearLayer(UILayerType layerType)
        {
            if (!_uiLayers.TryGetValue(layerType, out var layer))
            {
                return;
            }

            layer.CloseAllUI();
        }
    }
}