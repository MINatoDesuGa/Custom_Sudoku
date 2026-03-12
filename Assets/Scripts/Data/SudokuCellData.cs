using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Data {
    [Serializable]
    public class SudokuCellData {
        private const int CELL_GROUP_DIVIDE_FACTOR = 3;
        private const int MAX_CELL_NUMBER_COUNT = 9;
        public const int EMPTY_CELL_NUMBER = 0;


        [field: SerializeField]
        public int Row { get; private set; }
        [field: SerializeField]
        public int Col { get; private set; }
        [field: SerializeField]
        public int AssignedNumber { get; private set; } = EMPTY_CELL_NUMBER; 
        public (int, int) CellGroup { get; private set; }
        public bool IsSelected { get; private set; }
        public bool IsEditedCell { get; private set; }

        private static Dictionary<int, HashSet<(int, int)>> _assignedNumberTrackingMap = new();
        // Pencil mark 
        public Dictionary<int, int> PencilMarkedNumberTrackingMap = new();
        private StringBuilder _pencilMarkedStringBuilder = new();

        public void Reset() {
            AssignedNumber = EMPTY_CELL_NUMBER;
            IsEditedCell = false;
            IsSelected = false;
            _assignedNumberTrackingMap.Clear();
            PencilMarkedNumberTrackingMap.Clear();
            _pencilMarkedStringBuilder.Clear();
        }

        public void AssignCellGroup() {
            CellGroup = (Row / CELL_GROUP_DIVIDE_FACTOR, Col / CELL_GROUP_DIVIDE_FACTOR);
        }

        #region Assigned Number
        public void UpdateAssignedNumber(int number) {
            var value = (Row, Col);
            if (number == EMPTY_CELL_NUMBER && AssignedNumber != EMPTY_CELL_NUMBER) {
                _assignedNumberTrackingMap[AssignedNumber].Remove(value);
            }

            AssignedNumber = number;

            if (AssignedNumber == EMPTY_CELL_NUMBER) return;

            if (!_assignedNumberTrackingMap.ContainsKey(number)) {
                _assignedNumberTrackingMap.Add(number, new HashSet<(int, int)>());
            }

            _assignedNumberTrackingMap[number].Add(value);
        }
        public void CheckAssignedNumberFilledComplete(out bool isFilledComplete) {
            if (AssignedNumber == EMPTY_CELL_NUMBER) {
                isFilledComplete = false;
                return;
            }

            // Debug.Log($"filled state {_assignedNumberTrackingMap[AssignedNumber].Count}");
            isFilledComplete = _assignedNumberTrackingMap[AssignedNumber].Count == MAX_CELL_NUMBER_COUNT;
        }
        #endregion
        public void UpdateIsSelected(bool isSelected) {
            IsSelected = isSelected;
        }
        public void UpdateIsEditedCell(bool isEdited) {
            IsEditedCell = isEdited;
        }

        #region Pencil Marking
        public void UpdatePencilMarkedNumberString(int number, out string pencilMarkNumbers) {
            if (number is EMPTY_CELL_NUMBER) {
                PencilMarkedNumberTrackingMap.Clear();
                _pencilMarkedStringBuilder.Clear();
                pencilMarkNumbers = string.Empty;
                return;
            }

            if (PencilMarkedNumberTrackingMap.ContainsKey(number)) {
                RemovePencilMarkNumber(number);
            } else {
                AddPencilMarkNumber(number);
            }
            pencilMarkNumbers = _pencilMarkedStringBuilder.ToString();
        }
        private void AddPencilMarkNumber(int number) {
            PencilMarkedNumberTrackingMap.Add(number, _pencilMarkedStringBuilder.Length);
            _pencilMarkedStringBuilder.Append(number);
        }
        public void RemovePencilMarkNumber(int number) {
            int pencilMarkedNumberStartingIndex = PencilMarkedNumberTrackingMap[number];

            _pencilMarkedStringBuilder.Remove(pencilMarkedNumberStartingIndex, 1);
            PencilMarkedNumberTrackingMap.Remove(number);

            UpdatePencilMarkTrackingMapAfterRemoval();
            //LOCAL FUNCTION
            void UpdatePencilMarkTrackingMapAfterRemoval() {
                var pencilMarkMapKeys = new List<int>(PencilMarkedNumberTrackingMap.Keys);

                foreach (int key in pencilMarkMapKeys) {
                    if (PencilMarkedNumberTrackingMap[key] > pencilMarkedNumberStartingIndex) {
                        PencilMarkedNumberTrackingMap[key]--;
                    }
                }
            }
        }
        #endregion


    }
}