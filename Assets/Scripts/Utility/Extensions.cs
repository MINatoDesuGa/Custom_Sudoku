using UnityEngine;
using UnityEngine.UI;

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

        #region Image
        public static void EnableInteraction(this Image image) {
            image.raycastTarget = true;
            image.ChangeAlpha(1.0f);
        }
        public static void DisableInteraction(this Image image) {
            image.raycastTarget = false;
            image.ChangeAlpha(0.5f);
        }
        public static void ChangeColor(this Image image, Color color) { 
            image.color = color;    
        }
        public static void ChangeAlpha(this Image image, float alpha) { 
            Color currentColor = image.color;
            currentColor.a = alpha;
            image.color = currentColor;    
        }
        #endregion
    }
}