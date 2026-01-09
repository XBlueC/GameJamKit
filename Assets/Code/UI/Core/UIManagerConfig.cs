using System.Collections.Generic;
using UnityEngine;

namespace Code.UI.Core
{
    [CreateAssetMenu(menuName = "UI/UI Manager Config")]
    public class UIManagerConfig : ScriptableObject
    {
        public List<UIConfig> rows = new();

        public void AddRow(UIType type)
        {
            var panelRow = new UIConfig { UIType = type };
            rows.Add(panelRow);
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < rows.Count)
            {
                rows.RemoveAt(index);
            }
        }
    }
}