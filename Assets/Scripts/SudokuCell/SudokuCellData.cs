using UnityEngine;

namespace SudokuCell {
    [System.Serializable]
    public class SudokuCellData {
        [field: SerializeField]
        public int Row { get; private set; }
        [field: SerializeField]
        public int Col { get; private set; }
        [field: SerializeField]
        public int AssignedNumber { get; private set; }

        public void UpdateAssignedNumber(int number) {
            AssignedNumber = number;
        }
    }
}