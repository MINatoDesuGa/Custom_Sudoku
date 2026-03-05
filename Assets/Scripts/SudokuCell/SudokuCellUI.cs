using UnityEngine;

namespace SudokuCell {
    [RequireComponent (typeof(SudokuCellInput))]
    public class SudokuCellUI : MonoBehaviour {
        [SerializeField] private UnityEngine.UI.Image _cellBG;
        [SerializeField] private TMPro.TMP_Text _numberText;

        [Header("Settings")]
        [SerializeField] private Color _selectedColor;
        private Color _defaultColor;


        private SudokuCellInput _associatedInput;

        #region Mono
        private void Awake() {
            _associatedInput = GetComponent<SudokuCellInput>();
            _defaultColor = _cellBG.color;
        }
        private void OnEnable() {
            _associatedInput.OnSelect += OnSelect;
            _associatedInput.OnDeselect += OnDeselect;
            _associatedInput.OnNumberInput += OnNumberInput;
            _associatedInput.OnDoubleTap += ClearNumberText;
        }
        private void OnDisable() {
            _associatedInput.OnSelect -= OnSelect;
            _associatedInput.OnDeselect -= OnDeselect;
            _associatedInput.OnNumberInput -= OnNumberInput;
            _associatedInput.OnDoubleTap -= ClearNumberText;
        }
        #endregion

        #region Event Listeners
        private void OnSelect() {
            _cellBG.color = _selectedColor;
        }
        private void OnDeselect() { 
            _cellBG.color = _defaultColor;    
        }
        private void OnNumberInput(int number) { 
            _numberText.text = number.ToString();        
        }
        private void ClearNumberText() { 
            _numberText.text = string.Empty;    
        }
        #endregion
    }
}