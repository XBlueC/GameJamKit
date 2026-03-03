using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Code.Core.UI
{
    /// <summary>
    /// 层级配置数据
    /// </summary>
    [Serializable]
    public class LayerConfig
    {
        public bool ClearOnSceneLoad = false;
        public UILayerType LayerType;
        public bool UseScreenSpace = true;
    }

    public class UILayer
    {
        private readonly List<UIBase> _order = new();

        public UILayer(LayerConfig config, Transform parent)
        {
            LayerType = config.LayerType;
            ClearOnSceneLoad = config.ClearOnSceneLoad;

            var layerGo = new GameObject($"Layer{config.LayerType}");
            layerGo.transform.SetParent(parent);
            layerGo.transform.localPosition = Vector3.zero;
            layerGo.transform.localRotation = Quaternion.identity;
            layerGo.transform.localScale = Vector3.one;
            var rectTransform = layerGo.AddComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.zero;
            // 添加并配置Canvas
            Canvas = layerGo.AddComponent<Canvas>();
            Canvas.overrideSorting = true;
            Canvas.sortingOrder = (int)config.LayerType;
            Canvas.renderMode =
                config.UseScreenSpace ? RenderMode.ScreenSpaceOverlay : RenderMode.WorldSpace; // 根据配置设置渲染模式
            // 添加必要组件
            layerGo.AddComponent<GraphicRaycaster>();
        }

        public UILayerType LayerType { get; }
        public bool ClearOnSceneLoad { get; }
        public Canvas Canvas { get; }

        public UIBase OpenUI(UIInfo uiInfo, UIArgs args)
        {
            var isExisting = false;
            if (TryFindActiveUI(uiInfo.UIName, out var ui))
            {
                isExisting = true;
            }
            else
            {
                if (ui == null)
                {
                    ui = CreateNewUI(uiInfo);
                }
            }

            Debug.Log($"OpenUI {uiInfo.UIName}");
            // 挂接到层级管理
            ManageLayerActivation(ui, isExisting);

            ui.SetArgs(args);
            if (isExisting)
            {
                ui.OnArgsUpdate(args);
            }
            else
            {
                ui.OnInit();
                ui.OnShow(args);
            }

            return ui;
        }

        private bool TryFindActiveUI(string uiName, out UIBase ui)
        {
            ui = null;
            foreach (var node in _order)
            {
                if (node.UIName != uiName)
                {
                    continue;
                }

                ui = node;
                return true;
            }

            return false;
        }

        private void ManageLayerActivation(UIBase ui, bool isExisting)
        {
            ui.transform.SetParent(Canvas.transform);
            if (isExisting)
            {
                _order.Remove(ui);
            }
            else
            {
                if (_order.Count > 0)
                {
                    var currentTop = _order.Last();
                    currentTop.OnPause(); // 暂停当前顶层
                }
            }

            _order.Add(ui); // 将界面移到最前
            ui.transform.SetAsLastSibling();
        }

        private UIBase CreateNewUI(UIInfo uiInfo)
        {
            // string path = uiConfig.AssetPath;
            // var prefab = AssetsManager.Instance.LoadAsset<GameObject>(path);
            var prefab = uiInfo.Prefab;
            if (prefab == null)
            {
                throw new Exception($"UI prefab not found :{uiInfo.UIName} {uiInfo.AssetPath}");
            }

            var uiObj = Object.Instantiate(prefab, Canvas.transform);
            var ui = uiObj.GetComponent<UIBase>();
            ui.Init(uiInfo.UIName);
            return ui;
        }

        public void CloseUI(UIInfo uiInfo)
        {
            var uiBase = _order.Find(x => x.UIName == uiInfo.UIName);
            if (uiBase == null)
            {
                return;
            }

            _order.Remove(uiBase);
            // 恢复下一层界面
            if (_order.Count > 0)
            {
                _order.Last().OnResume();
            }

            uiBase.OnClose();
            Object.Destroy(uiBase.gameObject);
            Debug.Log($"CloseUI {uiInfo.UIName}");
        }

        public void CloseAllUI()
        {
            foreach (var uiBase in _order)
            {
                uiBase.OnClose();
                Object.Destroy(uiBase.gameObject);
            }

            _order.Clear();
        }
    }
}