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
        public bool IsSelected { get; private set; }
        public bool IsEditedCell { get; private set; }
        //TBD: Pencil mark 
        //private string _pencilMarkedNumberString = string.Empty;

        //private HashSet<int> _pencilMarkedNumberSet;

        #region Updaters
        public void UpdateAssignedNumber(int number) {
            AssignedNumber = number;
        }
        public void UpdateIsSelected(bool isSelected) {
            IsSelected = isSelected;
        }
        public void UpdateIsEditedCell(bool isEdited) {
            IsEditedCell = isEdited;
        }
        //TBD: Pencil mark
        //public void UpdatePencilMarkedNumberString(int number) {
        //    if(_pencilMarkedNumberSet.Contains(number)) { 
                
        //    } else {
        //        _pencilMarkedNumberSet.Add(number);
        //        _pencilMarkedNumberString += number;
        //    }
        //}
        #endregion
    }
}