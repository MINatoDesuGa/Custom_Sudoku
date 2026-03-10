using UnityEngine;
using Utility;

namespace Input {
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
            Controller.SudokuCellController.On_Number_Fill_Complete += OnNumberFillCompleted;
        }
        private void Start() {
            OnControlTypeInit?.Invoke(GetNumberInputFromControlType().ToString());
        }
        private void OnDisable() {
            _controlButton.onClick.RemoveListener(OnControlButtonClick);
            Controller.SudokuCellController.On_Number_Fill_Complete -= OnNumberFillCompleted;
        }
        #endregion

        #region Event Listeners
        private void OnControlButtonClick() {
            //print($"Control {_controlType} click");
            Controller.SudokuCellController.Current_Selected_Cell_Input.OnNumberInput?.Invoke(GetNumberInputFromControlType());
        }
        private void OnNumberFillCompleted(int number, bool isFilled) {
            if(number != GetNumberInputFromControlType()) return;

            if(isFilled) { 
                _controlButton.Disable_Interaction();
            } else {
                _controlButton.Enable_Interaction();
            }
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