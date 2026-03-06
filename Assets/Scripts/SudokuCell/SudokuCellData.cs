using System.Collections.Generic;
using UnityEngine;

namespace SudokuCell {
    [System.Serializable]
    public class SudokuCellData {
        [field: SerializeField]
        public int Row { get; private set; }
        [field: SerializeField]
        public int Col { get; private set; }
        [field: SerializeField]
        public int AssignedNumber { get; private set; } = 0; //0 means unassigned
        private string _pencilMarkedNumberString = string.Empty;

        private HashSet<int> _pencilMarkedNumberSet;

        #region Updaters
        public void UpdateAssignedNumber(int number) {
            AssignedNumber = number;
        }
        public void UpdatePencilMarkedNumberString(int number) {
            if(_pencilMarkedNumberSet.Contains(number)) { 
                
            } else {
                _pencilMarkedNumberSet.Add(number);
                _pencilMarkedNumberString += number;
            }
        }
        #endregion
    }
}