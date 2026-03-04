using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SudokuCell {
    public class SudokuCellInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
        private const float HOLD_ACTION_TRIGGER_TIME_IN_SEC = 0.8f;
        private const float DOUBLE_TAP_ACTION_TRIGGER_TIME_IN_SEC = 0.2f;

        public event System.Action OnSelect;
        public event System.Action OnHold;
        public event System.Action OnDoubleTap;

        //Double tap & Hold helpers
        private bool _isTappedOnce = false;
        private Coroutine _doubleTapTrackCoroutine;
        [SerializeField]
        private Coroutine _holdTrackCoroutine;
        private WaitForSeconds _holdTime;
        private WaitForSeconds _doubleTapTime;
        #region Mono
        private void Start() {
            _holdTime = new(HOLD_ACTION_TRIGGER_TIME_IN_SEC);
            _doubleTapTime = new(DOUBLE_TAP_ACTION_TRIGGER_TIME_IN_SEC);
        }
        #endregion

        #region Interface implementation
        public void OnPointerDown(PointerEventData eventData) {
            OnSelect?.Invoke();

            HandleDoubleTapAction();
            HandleHoldAction();
        }

        public void OnPointerUp(PointerEventData eventData) {
            ResetCoroutine(_holdTrackCoroutine);
        }
        #endregion

        #region Helpers
        private void HandleDoubleTapAction() {
            if (!_isTappedOnce) { 
                _isTappedOnce = true;
                _doubleTapTrackCoroutine = StartCoroutine(TriggerDoubleTapTracker());
            } else {
                _isTappedOnce = false;
                OnDoubleTap?.Invoke();
                print("double tap action triggered");
            }

            IEnumerator TriggerDoubleTapTracker() {
                yield return _doubleTapTime;
                _isTappedOnce = false;
            }
        }
        private void HandleHoldAction() {
            _holdTrackCoroutine = StartCoroutine(TriggerHoldTracker());

            IEnumerator TriggerHoldTracker() {
                yield return _holdTime;
                OnHold?.Invoke();
                print("hold action triggered");
            }
        }
        private void ResetCoroutine(Coroutine coroutine) { 
            if(coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }    
        }
        #endregion
    }
}