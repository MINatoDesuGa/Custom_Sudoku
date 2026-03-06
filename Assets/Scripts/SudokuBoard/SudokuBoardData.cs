using System;
using System.Collections.Generic;

namespace SudokuBoard {
    public class SudokuBoardData {
        private const int GRID_MAX_SIZE = 9;

        private int[,] _boardArray;
        private HashSet<Tuple<int, int>> _filledCellSet;
        public SudokuBoardData() {
            _boardArray = new int[GRID_MAX_SIZE, GRID_MAX_SIZE];
            _filledCellSet = new();
        }
        #region Methods
        public void ClearData() {
            for(int row = 0; row < GRID_MAX_SIZE; row++) { 
                for(int col = 0; col < GRID_MAX_SIZE; col++) { 
                    _boardArray[row, col] = 0;    
                }    
            }
            _filledCellSet.Clear();
        }
        public void CheckIfSudokuSolved(out bool isSudokuSolved) {
            if(_filledCellSet.Count == (GRID_MAX_SIZE * GRID_MAX_SIZE)) {
                isSudokuSolved = true;
                return;
            }
            isSudokuSolved = false;
        }
        public void UpdateData(SudokuCell.SudokuCellData cellData) {
            _boardArray[cellData.Row, cellData.Col] = cellData.AssignedNumber;

            var cellTuple = Tuple.Create(cellData.Row, cellData.Col);

            if(cellData.AssignedNumber != 0 ) {
                _filledCellSet.Add(cellTuple);
            } else {
                if(_filledCellSet.Contains(cellTuple)) {
                    _filledCellSet.Remove(cellTuple);
                }
            }
        }
        public bool IsCellValid(SudokuCell.SudokuCellData cellData = null, int row = 0, int col = 0, int num = 0) {
            if(cellData == null) { 
                num = _boardArray[row,col];
            } else {
                row = cellData.Row;
                col = cellData.Col;
                num = cellData.AssignedNumber;
            }
            
            // Check row
            for (int c = 0; c < 9; c++) {
                if(c == col) continue;
                if (_boardArray[row, c] == num) return false;
            }
                
            // Check column
            for (int r = 0; r < 9; r++) {
                if(r == row) continue;
                if (_boardArray[r, col] == num) return false;
            }
                

            // Check 3×3 box
            int boxRow = (row / 3) * 3;
            int boxCol = (col / 3) * 3;
            for (int r = boxRow; r < boxRow + 3; r++)
                for (int c = boxCol; c < boxCol + 3; c++) {
                    if(r == row && c == col) continue;
                    if (_boardArray[r, c] == num) return false;
                }
            return true;
        }
        #endregion
    }
}