using System.Collections.Generic;

namespace Code.Core
{
    public enum DebugColor
    {
        Blue,
        Indigo,
        Purple,
        Pink,
        Red,
        Orange,
        Yellow,
        Green,
        Teal,
        Cyan,
        White,

        // Gray,
        // GrayDark,
        // Gray100,
        // Gray200,
        // Gray300,
        // Gray400,
        // Gray500,
        // Gray600,
        // Gray700,
        // Gray800,
        // Gray900,
        Primary,
        Secondary,
        Success,
        Info,
        Warning,
        Danger,

        Light
        // Dark,
    }

    public static class Log
    {
        private static readonly Dictionary<DebugColor, string> ColorDict = new();

        static Log()
        {
            ColorDict.Add(DebugColor.Blue, "#0d6efd");
            ColorDict.Add(DebugColor.Indigo, "#6610f2");
            ColorDict.Add(DebugColor.Purple, "#6f42c1");
            ColorDict.Add(DebugColor.Pink, "#d63384");
            ColorDict.Add(DebugColor.Red, "#dc3545");
            ColorDict.Add(DebugColor.Orange, "#fd7e14");
            ColorDict.Add(DebugColor.Yellow, "#ffc107");
            ColorDict.Add(DebugColor.Green, "#198754");
            ColorDict.Add(DebugColor.Teal, "#20c997");
            ColorDict.Add(DebugColor.Cyan, "#0dcaf0");
            ColorDict.Add(DebugColor.White, "#fff");
            // ColorDic.Add(DebugColor.Gray, "#6c757d");
            // ColorDic.Add(DebugColor.GrayDark, "#343a40");
            // ColorDic.Add(DebugColor.Gray100, "#f8f9fa");
            // ColorDic.Add(DebugColor.Gray200, "#e9ecef");
            // ColorDic.Add(DebugColor.Gray300, "#dee2e6");
            // ColorDic.Add(DebugColor.Gray400, "#ced4da");
            // ColorDic.Add(DebugColor.Gray500, "#adb5bd");
            // ColorDic.Add(DebugColor.Gray600, "#6c757d");
            // ColorDic.Add(DebugColor.Gray700, "#495057");
            // ColorDic.Add(DebugColor.Gray800, "#343a40");
            // ColorDic.Add(DebugColor.Gray900, "#212529");
            ColorDict.Add(DebugColor.Primary, "#0d6efd");
            ColorDict.Add(DebugColor.Secondary, "#6c757d");
            ColorDict.Add(DebugColor.Success, "#198754");
            ColorDict.Add(DebugColor.Info, "#0dcaf0");
            ColorDict.Add(DebugColor.Warning, "#ffc107");
            ColorDict.Add(DebugColor.Danger, "#dc3545");
            ColorDict.Add(DebugColor.Light, "#f8f9fa");
            // ColorDic.Add(DebugColor.Dark, "#212529");
        }

        public static void Debug(string message, DebugColor color = DebugColor.Primary)
        {
            UnityEngine.Debug.Log($"<color={ColorDict[color]}>{message}</color>");
        }

        public static void Error(string message, DebugColor color = DebugColor.Red)
        {
            UnityEngine.Debug.LogError($"<color={ColorDict[color]}>{message}</color>");
        }
    }
}