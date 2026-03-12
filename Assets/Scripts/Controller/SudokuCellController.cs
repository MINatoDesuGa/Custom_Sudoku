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
        private void Start() {
            _data.AssignCellGroup();
        }
        private void OnEnable() {
            _associatedInput.OnSelect += OnSelect;
            _associatedInput.OnDeselect += OnDeselect;
            _associatedInput.OnNumberInput += OnNumberInput;
            _associatedInput.OnDoubleTap += ClearAssignedNumber;
            _associatedInput.OnHold += OnPencilMarkToggled;
            GameStateController.On_Game_State_Changed += OnGameStateChanged;
            On_Data_Updated += OnCellDataUpdated;
        }
        private void OnDisable() {
            _associatedInput.OnSelect -= OnSelect;
            _associatedInput.OnDeselect -= OnDeselect;
            _associatedInput.OnNumberInput -= OnNumberInput;
            _associatedInput.OnDoubleTap -= ClearAssignedNumber;
            _associatedInput.OnHold -= OnPencilMarkToggled;
            GameStateController.On_Game_State_Changed -= OnGameStateChanged;
            On_Data_Updated -= OnCellDataUpdated;
        }
        #endregion

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

        #region Event Listeners
        private void OnCellDataUpdated(SudokuCellController sourceSudokuCellController) {
            if (GameStateController.Current_Game_State is not GameState.Solving ||
                _data.PencilMarkedNumberTrackingMap.Count is 0 || sourceSudokuCellController == this) {
                return;
            }

            int sourceAssignedNumber = sourceSudokuCellController.CellData.AssignedNumber;

            if (sourceAssignedNumber is Data.SudokuCellData.EMPTY_CELL_NUMBER || _data.AssignedNumber is not Data.SudokuCellData.EMPTY_CELL_NUMBER) return;

            int sourceRow = sourceSudokuCellController.CellData.Row;
            int sourceCol = sourceSudokuCellController.CellData.Col;
            (int, int) sourceCellGroup = sourceSudokuCellController.CellData.CellGroup;

            //check row / col / group for pencil marks
            if (_data.Row == sourceRow || _data.Col == sourceCol || _data.CellGroup == sourceCellGroup) {
                if (_data.PencilMarkedNumberTrackingMap.ContainsKey(sourceAssignedNumber)) {
                    _data.UpdatePencilMarkedNumberString(sourceAssignedNumber, out string pencilMarkNumbers);
                    _associatedUI.ChangePencilMarkText(pencilMarkNumbers);
                }
            }
        }
        private void OnPencilMarkToggled() {
            switch (GameStateController.Current_Game_State) {
                case GameState.Solving:
                    if (_data.AssignedNumber is not Data.SudokuCellData.EMPTY_CELL_NUMBER) {
                        Debug.LogWarning("Already has assigned number. Cannot enable pencil mode");
                        return;
                    }

                    GameStateController.UpdateGameState(GameState.PencilMarking);
                    _associatedUI.ChangeBGColor(UI.SudokuCellUI.State.PencilMarking);
                    break;
                case GameState.PencilMarking:
                    GameStateController.UpdateGameState(GameState.Solving);
                    //_associatedUI.ChangeBGColor(UI.SudokuCellUI.State.Selected);
                    break;
            }
        }
        private void OnSelect() {
            if (GameStateController.Current_Game_State is GameState.Solving
                && _data.IsEditedCell) {
                return;
            }

            CheckAndUpdateCurrentSelectedCell();

            _data.UpdateIsSelected(true);
            _associatedUI.ChangeBGColor(GameStateController.Current_Game_State is not GameState.PencilMarking ? UI.SudokuCellUI.State.Selected : UI.SudokuCellUI.State.PencilMarking);
        }
        private void OnDeselect() {
            _data.UpdateIsSelected(false);
            _associatedUI.ChangeBGColor(UI.SudokuCellUI.State.Deselected);
        }
        private void OnNumberInput(int number) {
            switch (GameStateController.Current_Game_State) {
                case GameState.Editing:
                    _data.UpdateIsEditedCell(true);
                    updateAndCheckAssignedNumberFilledComplete();
                    break;
                case GameState.Solving:
                    updateAndCheckAssignedNumberFilledComplete();
                    break;
                case GameState.PencilMarking:
                    _data.UpdatePencilMarkedNumberString(number, out string pencilMarkNumbers);
                    _associatedUI.ChangePencilMarkText(pencilMarkNumbers);
                    break;
            }
            ///Local methods
            void updateAndCheckAssignedNumberFilledComplete() {
                _associatedUI.ChangeNumberText(number.ToString());
                _data.UpdateAssignedNumber(number);

                _data.CheckAssignedNumberFilledComplete(out bool isFilledComplete);

                if (isFilledComplete) {
                    print($"{number} filled completed");
                    On_Number_Fill_Complete?.Invoke(number, true);
                }

                On_Data_Updated?.Invoke(this);
            }
        }
        private void ClearAssignedNumber() {
            switch (GameStateController.Current_Game_State) {
                case GameState.Editing:
                    _data.UpdateIsEditedCell(false);
                    updateAndCheckAssignedNumberWasFilled();
                    break;
                case GameState.Solving:
                    updateAndCheckAssignedNumberWasFilled();
                    break;
                case GameState.PencilMarking:
                    _data.UpdatePencilMarkedNumberString(Data.SudokuCellData.EMPTY_CELL_NUMBER, out string pencilMarkNumbers); //0 means erasing current pencil mark numbers
                    _associatedUI.ChangePencilMarkText(string.Empty);
                    break;
            }
            ///Local methods
            void updateAndCheckAssignedNumberWasFilled() {
                if (_data.AssignedNumber is not Data.SudokuCellData.EMPTY_CELL_NUMBER) {
                    _data.CheckAssignedNumberFilledComplete(out bool isFilledComplete);

                    if (isFilledComplete) {
                        On_Number_Fill_Complete?.Invoke(_data.AssignedNumber, false);
                        print($"{_data.AssignedNumber} was filled. Now its not");
                    }
                    _associatedUI.ChangeNumberText(string.Empty);
                    _data.UpdateAssignedNumber(Data.SudokuCellData.EMPTY_CELL_NUMBER);
                    On_Data_Updated?.Invoke(this);
                }
            }
        }
        private void OnGameStateChanged(GameState gameState) {
            if (gameState is not GameState.PencilMarking && _data.IsSelected) {
                DisableAndResetCurrentSelectedCell();
            }

            switch (gameState) {
                case GameState.Solving:
                    _associatedUI.SetInteractable(!_data.IsEditedCell);
                    break;
                case GameState.Editing:
                    _associatedUI.SetInteractable(true);
                    break;
                case GameState.Reset:
                    Reset();
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