using UnityEngine;
using Utility;

namespace UI {
    [RequireComponent (typeof(Input.SudokuCellInput))]
    public class SudokuCellUI : MonoBehaviour {
        [SerializeField] private UnityEngine.UI.Image _cellBG;
        [SerializeField] private TMPro.TMP_Text _numberText;

        [Header("Settings")]
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _invalidColor;
        private Color _defaultColor;

        #region Mono
        private void Start() {
            _defaultColor = _cellBG.color;
        }
        #endregion

        #region Public methods
        public void ChangeBGColorOnSelected() {
            _cellBG.ChangeColor(_selectedColor);
        }
        public void ChangeBGColorOnDeselect() {
            _cellBG.ChangeColor(_defaultColor);    
        }
        public void ChangeNumberText(int number, TMPro.FontStyles fontStyle = TMPro.FontStyles.Normal) { 
            _numberText.SetText(number.ToString());
            _numberText.fontStyle = fontStyle;
        }
        public void ClearNumberText() { 
            _numberText.SetText(string.Empty);
        }
        public void ChangeBGColorOnInputValidated(bool isValid) {
            _cellBG.ChangeColor(isValid ? _selectedColor : _invalidColor);
        }
        public void SetInteractable(bool isInteractable) {
            if(isInteractable) {
                _cellBG.EnableInteraction();
            } else {
                _cellBG.DisableInteraction();
            }
        }
        #endregion
    }
}