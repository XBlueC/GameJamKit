using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Core.UI
{
    public class UIManager : SingletonMono<UIManager>
    {
        public UIConfig uiConfig;
        public Camera UICamera;
        public Canvas Canvas;
        public List<LayerConfig> UILayerConfigs = new();
        private readonly Dictionary<string, UIInfo> _uiConfigs = new();
        private readonly Dictionary<UILayerType, UILayer> _uiLayers = new();

        protected override void Initialize()
        {
            if (Canvas.gameObject.GetComponent<CanvasScaler>() == null)
            {
                Canvas.gameObject.AddComponent<CanvasScaler>();
            }

            foreach (var layer in UILayerConfigs)
            {
                _uiLayers.Add(layer.LayerType, new UILayer(new LayerConfig
                {
                    LayerType = layer.LayerType, ClearOnSceneLoad = layer.ClearOnSceneLoad,
                    UseScreenSpace = layer.UseScreenSpace
                }, Canvas.transform));
            }

            foreach (var row in uiConfig.rows)
            {
                _uiConfigs[row.UIName] = row;
            }
        }

        public UIBase OpenUI(string uiName, UIArgs args = default)
        {
            if (!_uiConfigs.TryGetValue(uiName, out var uiConfig))
            {
                throw new ArgumentException($"UIName {uiName} not found");
            }

            if (!_uiLayers.TryGetValue(uiConfig.Layer, out var layer))
            {
                throw new ArgumentException($"LayerType {uiName} not found");
            }

            return layer.OpenUI(uiConfig, args);
        }


        public void CloseUI(string uiName)
        {
            if (!_uiConfigs.TryGetValue(uiName, out var config))
            {
                throw new ArgumentException($"UIName {uiName} not found");
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