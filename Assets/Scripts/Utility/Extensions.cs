using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Utility {
    public static class Extensions {
        #region Canvas Group
        public static void Enable_Group(this CanvasGroup canvasGroup) {
            canvasGroup.alpha = 1.0f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        public static void Disable_Group(this CanvasGroup canvasGroup) {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        public static bool Is_Enabled(this CanvasGroup canvasGroup) { 
            return canvasGroup.alpha == 1.0f ? true : false;    
        }
        #endregion

        #region Image
        private const float DISABLED_IMAGE_ALPHA = 0.5f;
        public static void Enable_Interaction(this Image image) {
            image.raycastTarget = true;
            image.Change_Alpha(1.0f);
        }
        public static void Disable_Interaction(this Image image) {
            image.raycastTarget = false;
            image.Change_Alpha(DISABLED_IMAGE_ALPHA);
        }
        public static void Change_Color(this Image image, Color color) { 
            image.color = color;    
        }
        public static void Change_Alpha(this Image image, float alpha) { 
            Color currentColor = image.color;
            currentColor.a = alpha;
            image.color = currentColor;    
        }
        #endregion

        #region Button
        public static void Enable_Interaction(this Button button) {
            button.interactable = true;
        }
        public static void Disable_Interaction(this Button button) {
            button.interactable = false;
        }
        #endregion

        #region TMPro Text
        public static void Change_Color(this TMP_Text tmp_Text, Color color) {
            tmp_Text.color = color;
        }
        #endregion
    }
}