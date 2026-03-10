using UnityEngine;

namespace Controller {
    public class GameStateController : MonoBehaviour {
        public enum GameState {
            Solving, Editing, GameOver
        }
    }
}