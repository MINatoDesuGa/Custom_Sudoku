namespace SudokuBoard {
    public class SudokuBoardData {
        private const int GRID_MAX_SIZE = 9;

        private int[,] _boardArray;
        private int _cellFilledCount;
        public SudokuBoardData() {
            _boardArray = new int[GRID_MAX_SIZE, GRID_MAX_SIZE];
            _cellFilledCount = 0;
        }
        #region Methods
        public void ClearData() {
            for(int row = 0; row < GRID_MAX_SIZE; row++) { 
                for(int col = 0; col < GRID_MAX_SIZE; col++) { 
                    _boardArray[row, col] = 0;    
                }    
            }
            _cellFilledCount = 0;
        }
        public void CheckIfSudokuSolved(out bool isSudokuSolved) {
            if(_cellFilledCount is (GRID_MAX_SIZE * GRID_MAX_SIZE)) {
                isSudokuSolved = true;
            }
            isSudokuSolved = false;
        }
        public void UpdateData(SudokuCell.SudokuCellData cellData) {
            _boardArray[cellData.Row, cellData.Col] = cellData.AssignedNumber;
            if(cellData.AssignedNumber != 0 ) {
                _cellFilledCount++;
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
            for (int c = 0; c < 9; c++)
                if (_boardArray[row, c] == num) return false;

            // Check column
            for (int r = 0; r < 9; r++)
                if (_boardArray[r, col] == num) return false;

            // Check 3×3 box
            int boxRow = (row / 3) * 3;
            int boxCol = (col / 3) * 3;
            for (int r = boxRow; r < boxRow + 3; r++)
                for (int c = boxCol; c < boxCol + 3; c++)
                    if (_boardArray[r, c] == num) return false;

            return true;
        }
        #endregion
    }
}