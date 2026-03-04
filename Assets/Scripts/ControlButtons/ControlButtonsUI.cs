using UnityEngine;

namespace ControlButtons {
    public class ControlButtonsUI : MonoBehaviour {
        [SerializeField] private TMPro.TMP_Text _buttonText;

        private ControlButtonsInput _associatedInputHandler;
        #region Mono
        private void Awake() {
            _associatedInputHandler = GetComponent<ControlButtonsInput>();
        }
        private void OnEnable() {
            _associatedInputHandler.OnControlTypeInit += UpdateButtonText;
        }
        private void OnDisable() {
            _associatedInputHandler.OnControlTypeInit -= UpdateButtonText;
        }
        #endregion
        public void UpdateButtonText(string controlType) {
            _buttonText.SetText(controlType);
        }
    }
}