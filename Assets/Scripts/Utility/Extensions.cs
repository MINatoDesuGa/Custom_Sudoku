using UnityEngine;

namespace Utility {
    public static class Extensions {
        #region Canvas Group
        public static void EnableGroup(this CanvasGroup canvasGroup) {
            canvasGroup.alpha = 1.0f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        public static void DisableGroup(this CanvasGroup canvasGroup) {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        public static bool IsEnabled(this CanvasGroup canvasGroup) { 
            return canvasGroup.alpha == 1.0f ? true : false;    
        }
        #endregion
    }
}