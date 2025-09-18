namespace Project.Systems
{
    using UnityEngine;
    using System;

    public static class UIUtilities
    {
        public static void SetAlpha(CanvasGroup canvasGroup, float alpha)
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = alpha;
            }
        }
        
        public static void SetInteractable(CanvasGroup canvasGroup, bool interactable)
        {
            if (canvasGroup != null)
            {
                canvasGroup.interactable = interactable;
                canvasGroup.blocksRaycasts = interactable;
            }
        }
        
        public static string FormatTime(TimeSpan timeSpan)
        {
            return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
        
        public static string FormatCoins(int coins)
        {
            if (coins >= 1000000)
                return $"{coins / 1000000f:F1}M";
            if (coins >= 1000)
                return $"{coins / 1000f:F1}K";
            return coins.ToString();
        }
    }
}