using UnityEngine;

namespace SudokuBoard {
    public class SudokuBoardController : MonoBehaviour {
        private SudokuBoardData _sudokuBoardData;

        #region Mono
        private void Awake() {
            _sudokuBoardData = new SudokuBoardData();
        }
        private void OnEnable() {
            SudokuCell.SudokuCellController.OnDataUpdated += OnSudokuCellDataUpdated;
        }
        private void OnDisable() {
            SudokuCell.SudokuCellController.OnDataUpdated -= OnSudokuCellDataUpdated;
        }
        #endregion

        #region Event Listeners
        private void OnSudokuCellDataUpdated(SudokuCell.SudokuCellController sudokuCellController) {
            SudokuCell.SudokuCellData cellData = sudokuCellController.CellData;
            _sudokuBoardData.UpdateData(cellData);

            if(cellData.AssignedNumber == 0) return; //no need to check if cell data was cleared

            if( _sudokuBoardData.IsCellValid(cellData) ) {
                sudokuCellController.OnInputValidated(true);
                //print("valid input value");
                _sudokuBoardData.CheckIfSudokuSolved(out bool isSudokuSolved);
                if(isSudokuSolved) {
                    ///TODO: game over logic
                    print("Sudoku solved");
                }
            } else {
                print("invalid input value");
                sudokuCellController.OnInputValidated(false);
            }
        }
        #endregion
    }
}