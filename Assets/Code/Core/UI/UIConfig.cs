using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.UI
{
    [CreateAssetMenu(menuName = "UI/UI Manager Config")]
    public class UIConfig : ScriptableObject
    {
        public List<UIInfo> rows = new();

        public void AddRow(string uiName)
        {
            var panelRow = new UIInfo { UIName = uiName };
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