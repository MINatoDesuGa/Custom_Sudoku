using UnityEngine;

namespace ControlButtons {
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class ControlButtonsInput : MonoBehaviour {
        public enum ControlType {
            Number_1, Number_2, Number_3, Number_4, Number_5, Number_6, Number_7, Number_8, Number_9
        }

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
            OnControlTypeInit?.Invoke(GetNumberInputFromControlType().ToString());
        }
        private void OnDisable() {
            _controlButton.onClick.RemoveListener(OnControlButtonClick);
        }
        #endregion

        #region Event Listeners
        private void OnControlButtonClick() {
            //print($"Control {_controlType} click");
            SudokuCell.SudokuCellController.Current_Selected_Cell_Input.OnNumberInput?.Invoke(GetNumberInputFromControlType());
        }
        #endregion

        #region Helper methods
        private int GetNumberInputFromControlType() {
            string controlType = _controlType.ToString();
            return int.Parse(controlType[^1].ToString());
        }
        #endregion
    }
}