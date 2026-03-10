using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class TopPanelUI : MonoBehaviour {
        public static event System.Action On_Game_Reset;

        [SerializeField] private Button _resetButton;

        #region Mono
        private void OnEnable() {
            _resetButton.onClick.AddListener(OnResetClick);
        }
        private void OnDisable() {
            _resetButton.onClick.RemoveListener(OnResetClick);
        }
        #endregion

        #region Event Listeners
        private void OnResetClick() {
            On_Game_Reset?.Invoke();
        }
        #endregion

    }
}