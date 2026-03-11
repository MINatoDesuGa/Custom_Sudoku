using System;
using System.Collections.Generic;
using System.Text;
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

        private static Dictionary<int, HashSet<(int, int)>> _assignedNumberTrackingMap = new();
        // Pencil mark 
        private Dictionary<int, int> _pencilMarkedNumberTrackingMap = new();
        private StringBuilder _pencilMarkedStringBuilder = new();

        public void Reset() {
            AssignedNumber = 0;
            IsEditedCell = false;
            IsSelected = false;
            _assignedNumberTrackingMap.Clear();
        }

        #region Updaters
        public void UpdateAssignedNumber(int number) {
            var value = (Row, Col);
            if (number == 0 && AssignedNumber != 0) {
                _assignedNumberTrackingMap[AssignedNumber].Remove(value);
            }

            AssignedNumber = number;

            if (AssignedNumber == 0) return;

            if (!_assignedNumberTrackingMap.ContainsKey(number)) {
                _assignedNumberTrackingMap.Add(number, new HashSet<(int, int)>());
            }

            _assignedNumberTrackingMap[number].Add(value);
        }
        public void CheckAssignedNumberFilledComplete(out bool isFilledComplete) {
            if (AssignedNumber == 0) {
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
        public void UpdatePencilMarkedNumberString(int number, out string pencilMarkNumbers) {
            if (_pencilMarkedNumberTrackingMap.ContainsKey(number)) {
                _pencilMarkedStringBuilder.Remove(_pencilMarkedNumberTrackingMap[number], 1);
                _pencilMarkedNumberTrackingMap.Remove(number);

                var pencilMarkMapKeys = new List<int>(_pencilMarkedNumberTrackingMap.Keys);

                foreach (int key in pencilMarkMapKeys) {
                    _pencilMarkedNumberTrackingMap[key] = Mathf.Clamp(--_pencilMarkedNumberTrackingMap[key], 0, _pencilMarkedNumberTrackingMap.Count);
                }
            } else {
                _pencilMarkedNumberTrackingMap.Add(number, _pencilMarkedStringBuilder.Length);
                _pencilMarkedStringBuilder.Append(number);
            }
            pencilMarkNumbers = _pencilMarkedStringBuilder.ToString();
        }
        #endregion
    }
}