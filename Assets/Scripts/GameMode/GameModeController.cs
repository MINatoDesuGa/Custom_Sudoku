using UnityEngine;

namespace GameMode {
    public class GameModeController : MonoBehaviour {
        public static event System.Action<GameModeType> On_Game_Mode_Changed;
        public static GameModeType Current_Game_Mode { get; private set;}
        [Header("UI")]
        [SerializeField] private UnityEngine.UI.Button _gameModeChangeButton;
        [SerializeField] private TMPro.TMP_Text _gameModeButtonText;
        [SerializeField] private TMPro.TMP_Text _gameModeText;
        #region Mono
        private void Start() {
            UpdateGameMode(GameModeType.Edit);
            _gameModeButtonText.SetText(GameModeType.Edit.ToString());
            _gameModeText.SetText("Editing");
        }
        private void OnEnable() {
            _gameModeChangeButton.onClick.AddListener(OnGameModeChanged);
        }
        private void OnDisable() {
            _gameModeChangeButton.onClick.RemoveListener(OnGameModeChanged);
        }
        #endregion

        #region Event Listeners
        private void OnGameModeChanged() {
            GameModeType gameModeType = Current_Game_Mode == GameModeType.Edit ? GameModeType.Play : GameModeType.Edit;
            UpdateGameMode(gameModeType);
            _gameModeButtonText.SetText(gameModeType.ToString());
            _gameModeText.SetText(gameModeType == GameModeType.Edit ? "Editing" : "Solving");
        }
        #endregion

        #region Public static Methods
        public static void UpdateGameMode(GameModeType gameMode) {
            Current_Game_Mode = gameMode;
            On_Game_Mode_Changed?.Invoke(gameMode);
            print($"Game Mode -> {Current_Game_Mode}");
        }
        #endregion
    }
    public enum GameModeType {
        Edit, Play
    }
}