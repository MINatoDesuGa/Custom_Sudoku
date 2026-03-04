using UnityEngine;

namespace SudokuCell {
    [System.Serializable]
    public class SudokuCellData {
        [field: SerializeField]
        public int Row { get; private set; }
        [field: SerializeField]
        public int Col { get; private set; }
        public bool IsSelected { get; private set; }
    }
}