using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI {
    public class GameOverPanelUI : MonoBehaviour {
        [SerializeField] private CanvasGroup _panelCanvasGroup;
        [SerializeField] private Button _resetButton;

        #region Mono
        private void OnEnable() {
            _resetButton.onClick.AddListener(OnResetGameClick);
            Controller.SudokuBoardController.On_Sudoku_Solved += OnSudokuSolved;
        }
        private void OnDisable() {
            _resetButton.onClick.RemoveListener(OnResetGameClick);
            Controller.SudokuBoardController.On_Sudoku_Solved -= OnSudokuSolved;
        }
        #endregion

        #region Event Listeners
        private void OnResetGameClick() {
            Controller.GameStateController.UpdateGameState(GameState.Reset);
            _panelCanvasGroup.Disable_Group();
        }
        private void OnSudokuSolved() {
            _panelCanvasGroup.Enable_Group();
        }
        #endregion
    }
}