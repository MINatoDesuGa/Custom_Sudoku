using UnityEngine;

namespace SudokuCell {
    [RequireComponent(typeof(SudokuCellInput))]
    public class SudokuCellController : MonoBehaviour {
        [SerializeField] private SudokuCellData _data;
        private SudokuCellInput _associatedInput;
        #region Mono
        private void Awake() {
            _associatedInput = GetComponent<SudokuCellInput>();
        }
        private void OnEnable() {
            _associatedInput.OnNumberInput += OnNumberInput;    
        }
        private void OnDisable() {
            _associatedInput.OnNumberInput -= OnNumberInput;
        }
        #endregion

        #region Event Listeners
        private void OnNumberInput(int number) { 
            _data.UpdateAssignedNumber(number);    
        }
        #endregion
    }
}