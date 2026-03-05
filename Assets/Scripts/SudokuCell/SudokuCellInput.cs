using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace SudokuCell {
    public class SudokuCellInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
        private const float HOLD_ACTION_TRIGGER_TIME_IN_SEC = 0.8f;
        private const float DOUBLE_TAP_ACTION_TRIGGER_TIME_IN_SEC = 0.2f;

        private static SudokuCellInput _previous_Input;

        public event Action OnDeselect;
        public event Action OnSelect;
        public event Action OnHold;
        public event Action OnDoubleTap;
        public event Action<int> OnNumberInput;

        [SerializeField]
        private InputActionMap _inputActionMap;
        //Double tap & Hold helpers
        private bool _isTappedOnce = false;
        private Coroutine _doubleTapTrackCoroutine;
        private Coroutine _holdTrackCoroutine;
        private WaitForSeconds _holdTime;
        private WaitForSeconds _doubleTapTime;
        #region Mono
        private void Start() {
            _holdTime = new(HOLD_ACTION_TRIGGER_TIME_IN_SEC);
            _doubleTapTime = new(DOUBLE_TAP_ACTION_TRIGGER_TIME_IN_SEC);
        }
        #endregion

        #region Event Listeners
        private void OnActionPerformed(InputAction.CallbackContext context) {
            OnNumberInput?.Invoke(int.Parse(context.action.name[^1].ToString()));
        }
        #endregion

        #region Interface implementation
        public void OnPointerDown(PointerEventData eventData) {
            OnSelect?.Invoke();

            if (_previous_Input != null && _previous_Input != this) {
                ActivateActionMap(false, _previous_Input._inputActionMap);
                _previous_Input.OnDeselect?.Invoke();
            }

            ActivateActionMap(true);

            HandleDoubleTapAction();
            HandleHoldAction();

            #region local functions
            void ActivateActionMap(bool active, InputActionMap inputActionMap = null) {
                if (inputActionMap == null) {
                    inputActionMap = _inputActionMap;
                    _previous_Input = this;
                }

                if (active) {
                    inputActionMap.Enable();
                    for (int id = 0; id < inputActionMap.actions.Count; id++) {
                        inputActionMap.actions[id].performed += OnActionPerformed;
                    }
                } else {
                    inputActionMap.Disable();
                    for (int id = 0; id < inputActionMap.actions.Count; id++) {
                        inputActionMap.actions[id].performed -= OnActionPerformed;
                    }
                }

            }
            void HandleDoubleTapAction() {
                if (!_isTappedOnce) {
                    _isTappedOnce = true;
                    _doubleTapTrackCoroutine = StartCoroutine(TriggerDoubleTapTracker());
                } else {
                    _isTappedOnce = false;
                    OnDoubleTap?.Invoke();
                    print("double tap action triggered");
                }
                ///LOCAL FUNCTIONS
                IEnumerator TriggerDoubleTapTracker() {
                    yield return _doubleTapTime;
                    _isTappedOnce = false;
                }
            }
            void HandleHoldAction() {
                _holdTrackCoroutine = StartCoroutine(TriggerHoldTracker());
                ///LOCAL FUNCTIONS
                IEnumerator TriggerHoldTracker() {
                    yield return _holdTime;
                    OnHold?.Invoke();
                    print("hold action triggered");
                }
            }
            #endregion
        }

        public void OnPointerUp(PointerEventData eventData) {
            ResetCoroutine(_holdTrackCoroutine);
            ///LOCAL FUNCTIONS
            void ResetCoroutine(Coroutine coroutine) {
                if (coroutine != null) {
                    StopCoroutine(coroutine);
                    coroutine = null;
                }
            }
        }
        #endregion
    }
}