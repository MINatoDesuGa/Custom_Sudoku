using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data {
    [Serializable]
    public class SudokuCellData {
        [field: SerializeField]
        public int Row { get; private set; }
        [field: SerializeField]
        public int Col { get; private set; }
        [field: SerializeField]
        public int AssignedNumber { get; private set; } = 0; //0 means unassigned
        public bool IsSelected { get; private set; }
        public bool IsEditedCell { get; private set; }

        private static Dictionary<int, HashSet< (int, int) >> _assignedNumberTrackingMap = new();
        //TBD: Pencil mark 
        //private string _pencilMarkedNumberString = string.Empty;

        //private HashSet<int> _pencilMarkedNumberSet;

        public void Reset() {
            AssignedNumber = 0;
            IsEditedCell = false;
            IsSelected = false;
            _assignedNumberTrackingMap.Clear();
        }

        #region Updaters
        public void UpdateAssignedNumber(int number) {
            var value = (Row, Col);
            if(number == 0 && AssignedNumber != 0) {
                _assignedNumberTrackingMap[AssignedNumber].Remove(value);
            }

            AssignedNumber = number;

            if(AssignedNumber == 0) return;

            if(!_assignedNumberTrackingMap.ContainsKey(number)) {
                _assignedNumberTrackingMap.Add(number, new HashSet< (int, int) >());    
            }

            _assignedNumberTrackingMap [number].Add(value);
        }
        public void CheckAssignedNumberFilledComplete(out bool isFilledComplete) {
            if(AssignedNumber == 0) {
                isFilledComplete = false;
                return;
            }

           // Debug.Log($"filled state {_assignedNumberTrackingMap[AssignedNumber].Count}");
            isFilledComplete = _assignedNumberTrackingMap[AssignedNumber].Count == 9;
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