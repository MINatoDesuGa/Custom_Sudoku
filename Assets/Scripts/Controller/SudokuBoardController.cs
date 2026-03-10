using UnityEngine;

namespace Controller {
    public class SudokuBoardController : MonoBehaviour {
        public static event System.Action On_Sudoku_Solved;

        private Data.SudokuBoardData _sudokuBoardData;

        #region Mono
        private void Awake() {
            _sudokuBoardData = new Data.SudokuBoardData();
        }
        private void OnEnable() {
            SudokuCellController.On_Data_Updated += OnSudokuCellDataUpdated;
            GameStateController.On_Game_State_Changed += OnGameStateChanged;
        }
        private void OnDisable() {
            SudokuCellController.On_Data_Updated -= OnSudokuCellDataUpdated;
            GameStateController.On_Game_State_Changed += OnGameStateChanged;
        }
        #endregion

        #region Event Listeners
        private void OnGameStateChanged(GameState gameState) {
            switch(gameState) { 
                case GameState.Reset:
                    Reset();
                    break;
            }
        }
        private void OnSudokuCellDataUpdated(SudokuCellController sudokuCellController) {
            Data.SudokuCellData cellData = sudokuCellController.CellData;
            _sudokuBoardData.UpdateData(cellData);

            if(cellData.AssignedNumber == 0) return; //no need to check if cell data was cleared

            if( _sudokuBoardData.IsCellValid(cellData) ) {
                sudokuCellController.OnInputValidated(true);
                //print("valid input value");
                _sudokuBoardData.CheckIfSudokuSolved(out bool isSudokuSolved);
                if(isSudokuSolved) {
                    ///TODO: game over logic
                    print("Sudoku solved");
                    On_Sudoku_Solved?.Invoke();
                }
            } else {
                print("invalid input value");
                sudokuCellController.OnInputValidated(false);
            }
        }
        #endregion

        private void Reset() {
            _sudokuBoardData.ClearData();
        }
    }
}