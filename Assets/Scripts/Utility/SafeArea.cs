using UnityEngine;

namespace Utility {
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour {
        private RectTransform _rectTransform;
#if UNITY_EDITOR
        private Rect _lastSafeArea = new Rect(0, 0, 0, 0);
        private Vector2 _lastScreenSize = Vector2.zero;
#endif
        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
            ApplySafeArea();
        }
#if UNITY_EDITOR
        private void Update() {
            // Re-apply if orientation or resolution changes
            if (_lastSafeArea != Screen.safeArea ||
                _lastScreenSize.x != Screen.width ||
                _lastScreenSize.y != Screen.height) {
                ApplySafeArea();
            }
        }
#endif
        private void ApplySafeArea() {
            Rect safeArea = Screen.safeArea;
#if UNITY_EDITOR
            _lastSafeArea = safeArea;
            _lastScreenSize = new Vector2(Screen.width, Screen.height);
#endif
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
        }
    }
}