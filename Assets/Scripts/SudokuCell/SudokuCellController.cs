using System;
using Unity.VisualScripting;
using UnityEngine;

namespace SudokuCell {
    [RequireComponent(typeof(SudokuCellInput), typeof(SudokuCellUI))]
    public class SudokuCellController : MonoBehaviour {
        public static event Action<SudokuCellController> OnDataUpdated;

        [SerializeField] private SudokuCellData _data;
        public SudokuCellData CellData { get { return _data; } }
        private SudokuCellInput _associatedInput;
        private SudokuCellUI _associatedUI;
        #region Mono
        private void Awake() {
            _associatedInput = GetComponent<SudokuCellInput>();
            _associatedUI = GetComponent<SudokuCellUI>();
        }
        private void OnEnable() {
            _associatedInput.OnSelect += OnSelect;
            _associatedInput.OnDeselect += OnDeselect;
            _associatedInput.OnNumberInput += OnNumberInput;  
            _associatedInput.OnDoubleTap += ClearAssignedNumber;
        }
        private void OnDisable() {
            _associatedInput.OnSelect -= OnSelect;
            _associatedInput.OnDeselect -= OnDeselect;
            _associatedInput.OnNumberInput -= OnNumberInput;
            _associatedInput.OnDoubleTap -= ClearAssignedNumber;
        }
        #endregion

        #region Event Listeners
        private void OnSelect() {
            _associatedUI.ChangeBGColorOnSelected();
        }
        private void OnDeselect() {
            _associatedUI.ChangeBGColorOnDeselect();
        }
        private void OnNumberInput(int number) {
            _associatedUI.ChangeNumberText(number);
            _data.UpdateAssignedNumber(number);    
            OnDataUpdated?.Invoke(this);
        }
        private void ClearAssignedNumber() {
            _associatedUI.ClearNumberText();
            _data.UpdateAssignedNumber(0);
            OnDataUpdated?.Invoke(this);
        }
        #endregion

        #region Public methods
        public void OnInputValidated(bool isValid) {
            //_associatedUI.ChangeBGColorOnInputValidated(isValid);
        }
        #endregion
    }
}