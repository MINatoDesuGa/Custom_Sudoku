using UnityEngine;

namespace SudokuCell {
    [RequireComponent (typeof(SudokuCellInput))]
    public class SudokuCellUI : MonoBehaviour {
        private SudokuCellInput _associatedInput;

        #region Mono
        private void Awake() {
            _associatedInput = GetComponent<SudokuCellInput>();
        }
        #endregion
    }
}