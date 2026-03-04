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
        #endregion
    }
}