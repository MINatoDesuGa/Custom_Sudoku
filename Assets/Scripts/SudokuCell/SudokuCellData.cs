using UnityEngine;

namespace SudokuCell {
    [System.Serializable]
    public class SudokuCellData {
        [field: SerializeField]
        public int row { get; private set; }
        [field: SerializeField]
        public int col { get; private set; }
        public bool IsSelected { get; private set; }
    }
}