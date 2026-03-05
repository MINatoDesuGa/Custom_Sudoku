using System;
using UnityEngine;

namespace SudokuCell {
    [RequireComponent(typeof(SudokuCellInput))]
    public class SudokuCellController : MonoBehaviour {
        public static event Action<SudokuCellData, SudokuCellInput> OnDataUpdated;

        [SerializeField] private SudokuCellData _data;
        private SudokuCellInput _associatedInput;
        #region Mono
        private void Awake() {
            _associatedInput = GetComponent<SudokuCellInput>();
        }
        private void OnEnable() {
            _associatedInput.OnNumberInput += OnNumberInput;  
            _associatedInput.OnDoubleTap += ClearAssignedNumber;
        }
        private void OnDisable() {
            _associatedInput.OnNumberInput -= OnNumberInput;
            _associatedInput.OnDoubleTap -= ClearAssignedNumber;
        }
        #endregion

        #region Event Listeners
        private void OnNumberInput(int number) { 
            _data.UpdateAssignedNumber(number);    
            OnDataUpdated?.Invoke(_data, _associatedInput);
        }
        private void ClearAssignedNumber() {
            _data.UpdateAssignedNumber(0);
            OnDataUpdated?.Invoke(_data, _associatedInput);
        }
        #endregion
    }
}