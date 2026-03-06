using System;
using UnityEngine;
using GameMode;

namespace SudokuCell {
    [RequireComponent(typeof(SudokuCellInput), typeof(SudokuCellUI))]
    public class SudokuCellController : MonoBehaviour {
        public static event Action<SudokuCellController> OnDataUpdated;
        public static SudokuCellInput Current_Selected_Cell_Input { get; private set; } //keep track of previous cell to disable

        [SerializeField] private SudokuCellData _data;
        public SudokuCellData CellData { get { return _data; } }
        private SudokuCellInput _associatedInput;
        private SudokuCellUI _associatedUI;
        #region Mono
        private void Awake() {
            _associatedInput = GetComponent<SudokuCellInput>();
            _associatedUI = GetComponent<SudokuCellUI>();
        }
        private void OnEnable() {
            _associatedInput.OnSelect += OnSelect;
            _associatedInput.OnDeselect += OnDeselect;
            _associatedInput.OnNumberInput += OnNumberInput;
            _associatedInput.OnDoubleTap += ClearAssignedNumber;
            GameModeController.On_Game_Mode_Changed += OnGameModeChanged;
        }
        private void OnDisable() {
            _associatedInput.OnSelect -= OnSelect;
            _associatedInput.OnDeselect -= OnDeselect;
            _associatedInput.OnNumberInput -= OnNumberInput;
            _associatedInput.OnDoubleTap -= ClearAssignedNumber;
            GameModeController.On_Game_Mode_Changed -= OnGameModeChanged;
        }
        #endregion

        #region Event Listeners
        private void OnSelect() {
            if (GameModeController.Current_Game_Mode is GameModeType.Play
                && _data.IsEditedCell) {
                return;
            }

            CheckAndUpdatePreviousSelectedCell();

            _data.UpdateIsSelected(true);
            _associatedUI.ChangeBGColorOnSelected();
        }
        private void OnDeselect() {
            _data.UpdateIsSelected(false);
            _associatedUI.ChangeBGColorOnDeselect();
        }
        private void OnNumberInput(int number) {
            if (GameModeController.Current_Game_Mode is GameModeType.Edit) {
                _data.UpdateIsEditedCell(true);
            }

            _associatedUI.ChangeNumberText(number);
            _data.UpdateAssignedNumber(number);
            OnDataUpdated?.Invoke(this);
        }
        private void ClearAssignedNumber() {
            if (GameModeController.Current_Game_Mode is GameModeType.Edit) {
                _data.UpdateIsEditedCell(false);
            }

            _associatedUI.ClearNumberText();
            _data.UpdateAssignedNumber(0);
            OnDataUpdated?.Invoke(this);
        }
        private void OnGameModeChanged(GameModeType gameModeType) {
            if (_data.IsSelected) {
                Current_Selected_Cell_Input.ActivateActionMap(false);
                OnDeselect();
                Current_Selected_Cell_Input = null;
            }
            if (gameModeType is GameModeType.Edit) {
                _associatedUI.SetInteractable(true);
            } else {
                _associatedUI.SetInteractable(!_data.IsEditedCell);
            }
        }
        #endregion

        #region Public methods
        public void OnInputValidated(bool isValid) {
            //_associatedUI.ChangeBGColorOnInputValidated(isValid);
        }
        #endregion

        #region Private methods
        private void CheckAndUpdatePreviousSelectedCell() {
            if (Current_Selected_Cell_Input != null && Current_Selected_Cell_Input != _associatedInput) {
                Current_Selected_Cell_Input.ActivateActionMap(false);
                Current_Selected_Cell_Input.OnDeselect?.Invoke();
            }
            Current_Selected_Cell_Input = _associatedInput;
        }
        #endregion
    }
}