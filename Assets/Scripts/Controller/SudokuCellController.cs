using System;
using UnityEngine;

namespace Controller {
    [RequireComponent(typeof(Input.SudokuCellInput), typeof(UI.SudokuCellUI))]
    public class SudokuCellController : MonoBehaviour {
        public static event Action<SudokuCellController> On_Data_Updated;
        public static event Action<int, bool> On_Number_Fill_Complete;
        public static Input.SudokuCellInput Current_Selected_Cell_Input { get; private set; } //keep track of previous cell to disable

        [SerializeField] private Data.SudokuCellData _data;
        public Data.SudokuCellData CellData { get { return _data; } }
        private Input.SudokuCellInput _associatedInput;
        private UI.SudokuCellUI _associatedUI;
        #region Mono
        private void Awake() {
            _associatedInput = GetComponent<Input.SudokuCellInput>();
            _associatedUI = GetComponent<UI.SudokuCellUI>();
        }
        private void OnEnable() {
            _associatedInput.OnSelect += OnSelect;
            _associatedInput.OnDeselect += OnDeselect;
            _associatedInput.OnNumberInput += OnNumberInput;
            _associatedInput.OnDoubleTap += ClearAssignedNumber;
            GameStateController.On_Game_State_Changed += OnGameStateChanged;
            UI.TopPanelUI.On_Game_Reset += Reset;
            UI.GameOverPanelUI.On_Reset_Game += Reset;
        }
        private void OnDisable() {
            _associatedInput.OnSelect -= OnSelect;
            _associatedInput.OnDeselect -= OnDeselect;
            _associatedInput.OnNumberInput -= OnNumberInput;
            _associatedInput.OnDoubleTap -= ClearAssignedNumber;
            GameStateController.On_Game_State_Changed -= OnGameStateChanged;
            UI.TopPanelUI.On_Game_Reset -= Reset;
            UI.GameOverPanelUI.On_Reset_Game -= Reset;
        }
        #endregion

        #region Event Listeners
        private void Reset() {
            if (_data.IsEditedCell) {
                _associatedUI.SetInteractable(true);
            }

            _data.Reset();
            if (_data.IsSelected) {
                DisableAndResetCurrentSelectedCell();
            }
            ClearAssignedNumber();
        }
        private void OnSelect() {
            if (GameStateController.Current_Game_State is GameState.Solving
                && _data.IsEditedCell) {
                return;
            }

            CheckAndUpdateCurrentSelectedCell();

            _data.UpdateIsSelected(true);
            _associatedUI.ChangeBGColorOnSelected();
        }
        private void OnDeselect() {
            _data.UpdateIsSelected(false);
            _associatedUI.ChangeBGColorOnDeselect();
        }
        private void OnNumberInput(int number) {
            TMPro.FontStyles fontStyle = TMPro.FontStyles.Normal;
            if (GameStateController.Current_Game_State is GameState.Editing) {
                _data.UpdateIsEditedCell(true);
                fontStyle = TMPro.FontStyles.Bold;
            }

            _associatedUI.ChangeNumberText(number, fontStyle);
            _data.UpdateAssignedNumber(number);

            _data.CheckAssignedNumberFilledComplete(out bool isFilledComplete);

            if (isFilledComplete) {
                print($"{number} filled completed");
                On_Number_Fill_Complete?.Invoke(number, true);
            }

            On_Data_Updated?.Invoke(this);
        }
        private void ClearAssignedNumber() {
            if (GameStateController.Current_Game_State is GameState.Editing) {
                _data.UpdateIsEditedCell(false);
            }

            _associatedUI.ClearNumberText();

            _data.CheckAssignedNumberFilledComplete(out bool isFilledComplete);

            if (isFilledComplete) {
                On_Number_Fill_Complete?.Invoke(_data.AssignedNumber, false);
                print($"{_data.AssignedNumber} was filled. Now its not");
            }

            _data.UpdateAssignedNumber(0);
            On_Data_Updated?.Invoke(this);
        }
        private void OnGameStateChanged(GameState gameState) {
            if (_data.IsSelected) {
                DisableAndResetCurrentSelectedCell();
            }

            switch (gameState) {
                case GameState.Solving:
                    _associatedUI.SetInteractable(!_data.IsEditedCell);
                    break;
                case GameState.Editing:
                    _associatedUI.SetInteractable(true);
                    break;
            }
        }
        #endregion

        #region Public methods
        public void OnInputValidated(bool isValid) {
            //_associatedUI.ChangeBGColorOnInputValidated(isValid);
        }
        #endregion

        #region Current Selected Cell Input
        private void CheckAndUpdateCurrentSelectedCell() {
            if (Current_Selected_Cell_Input != null && Current_Selected_Cell_Input != _associatedInput) {
                Current_Selected_Cell_Input.ActivateActionMap(false);
                Current_Selected_Cell_Input.OnDeselect?.Invoke();
            }
            Current_Selected_Cell_Input = _associatedInput;
        }
        private void DisableAndResetCurrentSelectedCell() {
            Current_Selected_Cell_Input.ActivateActionMap(false);
            OnDeselect();
            Current_Selected_Cell_Input = null;
        }
        #endregion
    }
}