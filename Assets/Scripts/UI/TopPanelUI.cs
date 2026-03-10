using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    /// <summary>
    /// Handles UI of Game Mode, Reset, Game Options
    /// </summary>
    public class TopPanelUI : MonoBehaviour {
        private const string EDIT_MODE_BUTTON_TEXT = "EDIT"; ///TODO: picture based
        private const string SOLVE_MODE_BUTTON_TEXT = "SOLVE"; ///TODO: picture based
        private const string SOLVE_MODE_TEXT = "SOLVING";
        private const string EDIT_MODE_TEXT = "EDITING";

        [Header("Reset Game")]
        [SerializeField] private Button _resetButton;
        [Header("Game Mode")]
        [SerializeField] private Button _gameModeChangeButton;
        [SerializeField] private TMP_Text _gameModeButtonText;
        [SerializeField] private TMP_Text _gameModeText;
        #region Mono
        private void Start() {
            Controller.GameStateController.UpdateGameState(GameState.Editing);
            UpdateGameModeText(GameState.Editing);
        }
        private void OnEnable() {
            _resetButton.onClick.AddListener(OnResetClick);
            _gameModeChangeButton.onClick.AddListener(OnGameModeChangButtonClick);
        }
        private void OnDisable() {
            _resetButton.onClick.RemoveListener(OnResetClick);
            _gameModeChangeButton.onClick.RemoveListener(OnGameModeChangButtonClick);
        }
        #endregion

        #region Event Listeners
        private void OnResetClick() {
            Controller.GameStateController.UpdateGameState(GameState.Reset);
        }
        private void OnGameModeChangButtonClick() {
            GameState newGameState = Controller.GameStateController.Current_Game_State == GameState.Editing ? GameState.Solving : GameState.Editing;
            Controller.GameStateController.UpdateGameState(newGameState);
            UpdateGameModeText(newGameState);
        }
        #endregion

        #region Game Mode UI
        private void UpdateGameModeText(GameState gameState) {
            switch (gameState) {
                case GameState.Solving:
                    _gameModeButtonText.SetText(SOLVE_MODE_BUTTON_TEXT);
                    _gameModeText.SetText(SOLVE_MODE_TEXT);
                    break;
                case GameState.Editing:
                    _gameModeButtonText.SetText(EDIT_MODE_BUTTON_TEXT);
                    _gameModeText.SetText(EDIT_MODE_TEXT);
                    break;
            }
        }
        #endregion
    }
}