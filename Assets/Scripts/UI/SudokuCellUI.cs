using UnityEngine;
using Utility;

namespace UI {
    [RequireComponent(typeof(Input.SudokuCellInput))]
    public class SudokuCellUI : MonoBehaviour {
        public enum State {
            Selected, Deselected, PencilMarking
        }

        [SerializeField] private UnityEngine.UI.Image _cellBG;
        [SerializeField] private TMPro.TMP_Text _numberText;
        [SerializeField] private TMPro.TMP_Text _pencilMarkText;

        [Header("Settings")]
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _invalidColor;
        [SerializeField] private Color _pencilMarkColor;
        private Color _defaultColor;
        #region Mono
        private void Start() {
            _defaultColor = _cellBG.color;
        }
        #endregion

        #region Public methods
        public void ChangeBGColor(State state) {
            switch (state) {
                case State.Selected:
                    _cellBG.Change_Color(_selectedColor);
                    break;
                case State.Deselected:
                    _cellBG.Change_Color(_defaultColor);
                    break;
                case State.PencilMarking:
                    _cellBG.Change_Color(_pencilMarkColor);
                    break;
            }
        }
        public void ChangeNumberText(string numberString) {
            switch (Controller.GameStateController.Current_Game_State) {
                case GameState.Editing:
                    _numberText.fontStyle = TMPro.FontStyles.Bold;
                    break;
                case GameState.Solving:
                    _numberText.fontStyle = TMPro.FontStyles.Normal;
                    break;
            }
            _numberText.SetText(numberString);
            ChangePencilMarkText(string.Empty);
        }
        public void ChangePencilMarkText(string pencilMarkString) {
            _pencilMarkText.SetText(pencilMarkString);
        }
        public void ChangeBGColorOnInputValidated(bool isValid) {
            _cellBG.Change_Color(isValid ? _selectedColor : _invalidColor);
        }
        public void SetInteractable(bool isInteractable) {
            if (isInteractable) {
                _cellBG.Enable_Interaction();
            } else {
                _cellBG.Disable_Interaction();
            }
        }
        #endregion
    }
}