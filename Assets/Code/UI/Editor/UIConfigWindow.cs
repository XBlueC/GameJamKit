using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Code.UI.Core;
using UnityEditor;
using UnityEngine;

namespace Code.UI.Editor
{
    public class UIConfigWindow : EditorWindow
    {
        private UIManagerConfig _config;
        private Vector2 _scrollPos;

        private void OnEnable()
        {
            LoadOrCreateConfig();
        }

        private void OnGUI()
        {
            if (_config == null)
            {
                EditorGUILayout.LabelField("No config loaded");
                return;
            }

            #region 按钮

            GUILayout.BeginHorizontal();

            GUILayout.Label("UI Panel Configuration", EditorStyles.boldLabel);
            if (GUILayout.Button("Add Panel", GUILayout.Width(100))) ShowAddPanelMenu();

            if (GUILayout.Button("Sort by Enum", GUILayout.Width(100))) SortByEnumOrder();

            if (GUILayout.Button("Save", GUILayout.Width(80))) SaveConfig();

            GUILayout.EndHorizontal();

            #endregion

            #region 表头

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            EditorGUILayout.LabelField("Type", EditorStyles.toolbarButton, GUILayout.Width(120));
            EditorGUILayout.LabelField("Modal", EditorStyles.toolbarButton, GUILayout.Width(50));
            EditorGUILayout.LabelField("Destroy", EditorStyles.toolbarButton, GUILayout.Width(60));
            EditorGUILayout.LabelField("Layer", EditorStyles.toolbarButton, GUILayout.Width(100));
            EditorGUILayout.LabelField("Prefab", EditorStyles.toolbarButton, GUILayout.MinWidth(150));
            GUILayout.Space(40); // 为删除按钮留空间

            EditorGUILayout.EndHorizontal();

            #endregion

            #region 滚动视图

            var removeIndex = -1;
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            for (var i = 0; i < _config.rows.Count; i++)
            {
                var row = _config.rows[i];
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(row.UIType.ToString(), GUILayout.Width(120));
                // row.Name = row.UIType.ToString();
                row.IsModal = EditorGUILayout.Toggle(row.IsModal, GUILayout.Width(50));
                row.DestroyOnClose = EditorGUILayout.Toggle(row.DestroyOnClose, GUILayout.Width(60));
                row.Layer = (UILayerType)EditorGUILayout.EnumPopup(row.Layer, GUILayout.Width(100));
                row.Prefab = (GameObject)EditorGUILayout.ObjectField(row.Prefab, typeof(GameObject), false,
                    GUILayout.MinWidth(150));
                row.AssetPath = AssetDatabase.GetAssetPath(row.Prefab);
                if (GUILayout.Button("✕", GUILayout.Width(30), GUILayout.Height(16))) removeIndex = i;

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            if (removeIndex >= 0)
            {
                _config.RemoveAt(removeIndex);
                EditorUtility.SetDirty(_config);
                Repaint();
            }

            #endregion

            #region 页脚

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Config Asset")) EditorGUIUtility.PingObject(_config);

            GUILayout.EndHorizontal();

            #endregion
        }

        [MenuItem("Tools/UI Panel Config Manager")]
        public static void ShowWindow()
        {
            GetWindow<UIConfigWindow>("UI Panels");
        }

        private void LoadOrCreateConfig()
        {
            var guids = AssetDatabase.FindAssets("t:UIManagerConfig");
            if (guids.Length > 0)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                _config = AssetDatabase.LoadAssetAtPath<UIManagerConfig>(path);
            }
            else
            {
                _config = CreateInstance<UIManagerConfig>();
            }
        }

        private void ShowAddPanelMenu()
        {
            var menu = new GenericMenu();
            var allTypes = Enum.GetValues(typeof(UIType));
            var existingTypes = new HashSet<UIType>(_config.rows.Select(r => r.UIType));
            var hasValidOption = false;
            foreach (UIType type in allTypes)
                if (existingTypes.Contains(type))
                {
                    menu.AddDisabledItem(new GUIContent(type + " (already added)"));
                }
                else
                {
                    var text = type.ToString();
                    menu.AddItem(new GUIContent(text), false, () =>
                    {
                        _config.AddRow(type);
                        EditorUtility.SetDirty(_config);
                        Repaint();
                    });
                    hasValidOption = true;
                }

            if (!hasValidOption) menu.AddDisabledItem(new GUIContent("All panels already added"));

            menu.ShowAsContext();
        }

        private void SortByEnumOrder()
        {
            if (_config?.rows == null || _config.rows.Count == 0) return;

            // 按 UIType 的 int 值升序排列
            _config.rows.Sort((a, b) =>
            {
                var orderA = a.UIType;
                var orderB = b.UIType;
                return orderA.CompareTo(orderB);
            });

            EditorUtility.SetDirty(_config);
            Repaint();
        }

        private void SaveConfig()
        {
            if (!_config) return;
            const string configPath = UIManager.ConfigPath;
            var dir = Path.GetDirectoryName(configPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (AssetDatabase.LoadAssetAtPath<UIManagerConfig>(configPath) == null)
                AssetDatabase.CreateAsset(_config, configPath);
            else
                EditorUtility.SetDirty(_config);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("UI Config Saved!");
        }
    }
}