using System;
using UnityEngine;

namespace Controller {
    public class GameStateController : MonoBehaviour {
        public static event Action<GameState> On_Game_State_Changed;
        public static GameState Current_Game_State { get; private set;}
        #region Mono

        #endregion

        #region Event Listeners

        #endregion

        #region Public static methods
        public static void UpdateGameState(GameState state) {
            if(state is not GameState.Reset) {
                Current_Game_State = state;
            }
            
            On_Game_State_Changed?.Invoke(state);
            #if UNITY_EDITOR
            print($"GAME_STATE -> <color=green>{Current_Game_State}</color>");
            #endif
        }
        #endregion
    }
}

public enum GameState {
    Solving, Editing, GameOver, Reset
}