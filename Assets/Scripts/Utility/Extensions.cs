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
        private const float DISABLED_IMAGE_ALPHA = 0.6f;
        public static void EnableInteraction(this Image image) {
            image.raycastTarget = true;
            image.ChangeAlpha(1.0f);
        }
        public static void DisableInteraction(this Image image) {
            image.raycastTarget = false;
            image.ChangeAlpha(DISABLED_IMAGE_ALPHA);
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

        #region Button
        public static void EnableInteraction(this Button button) {
            button.interactable = true;
        }
        public static void DisableInteraction(this Button button) {
            button.interactable = false;
        }
        #endregion
    }
}