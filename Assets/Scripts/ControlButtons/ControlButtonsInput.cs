using UnityEngine;

namespace LamminlunSimte.CustomSudoku.ControlButtons {
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class ControlButtonsInput : MonoBehaviour {
        public enum ControlType {
            Number_1, Number_2, Number_3, Number_4, Number_5, Number_6, Number_7, Number_8, Number_9
        }

        public static event System.Action<ControlType> On_Control_Button_Click;
        public event System.Action<string> OnControlTypeInit;

        [SerializeField] private ControlType _controlType;
        private UnityEngine.UI.Button _controlButton;
        #region Mono
        private void Awake() {
            _controlButton = GetComponent<UnityEngine.UI.Button>();
        }
        private void OnEnable() {
            _controlButton.onClick.AddListener(OnControlButtonClick);
        }
        private void Start() {
            OnControlTypeInit?.Invoke(GetButtonTextFromControlType());
        }
        private void OnDisable() {
            _controlButton.onClick.RemoveListener(OnControlButtonClick);
        }
        #endregion

        #region Event Listeners
        private void OnControlButtonClick() {
            print($"Control {_controlType} click");
            On_Control_Button_Click?.Invoke(_controlType);
        }
        #endregion

        #region Helper methods
        private string GetButtonTextFromControlType() {
            switch (_controlType) {
                case ControlType.Number_1:
                    return "1";
                case ControlType.Number_2:
                    return "2";
                case ControlType.Number_3:
                    return "3";
                case ControlType.Number_4:
                    return "4";
                case ControlType.Number_5:
                    return "5";
                case ControlType.Number_6:
                    return "6";
                case ControlType.Number_7:
                    return "7";
                case ControlType.Number_8:
                    return "8";
                case ControlType.Number_9:
                    return "9";
            }
            return null;
        }
        #endregion
    }
}