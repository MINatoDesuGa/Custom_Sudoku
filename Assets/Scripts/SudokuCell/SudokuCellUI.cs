using UnityEngine;

namespace SudokuCell {
    [RequireComponent (typeof(SudokuCellInput))]
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
            UpdateCellBGColor(_selectedColor);
        }
        public void ChangeBGColorOnDeselect() {
            UpdateCellBGColor(_defaultColor);    
        }
        public void ChangeNumberText(int number) { 
            UpdateNumberText(number.ToString());        
        }
        public void ClearNumberText() { 
            UpdateNumberText(string.Empty);    
        }
        public void ChangeBGColorOnInputValidated(bool isValid) {
            UpdateCellBGColor(isValid ? _selectedColor : _invalidColor);
        }
        #endregion

        #region Private methods
        private void UpdateNumberText(string number = "") {
            _numberText.text = number;
        }
        private void UpdateCellBGColor(Color color) {
            _cellBG.color = color;
        }
        #endregion
    }
}