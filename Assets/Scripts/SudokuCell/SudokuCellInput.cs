using UnityEngine;
using UnityEngine.EventSystems;

namespace SudokuCell {
    public class SudokuCellInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
        public static event System.Action OnSelect;
        public static event System.Action OnHold;
        public static event System.Action OnDoubleTap;

        #region Mono

        #endregion

        #region Interface implementation
        public void OnPointerDown(PointerEventData eventData) {
            
        }

        public void OnPointerUp(PointerEventData eventData) {
            
        }
        #endregion
    }
}