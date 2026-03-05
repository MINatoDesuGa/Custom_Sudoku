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
        private void OnSudokuCellDataUpdated(SudokuCell.SudokuCellData cellData) {
            _sudokuBoardData.UpdateData(cellData);
            if( _sudokuBoardData.IsCellValid(cellData) ) {
                print("valid input value");
                _sudokuBoardData.CheckIfSudokuSolved(out bool isSudokuSolved);
                if(isSudokuSolved) {
                    ///TODO: game over logic
                    print("Sudoku solved");
                }
            } else {
                print("invalid input value");
            }
        }
        #endregion
    }
}