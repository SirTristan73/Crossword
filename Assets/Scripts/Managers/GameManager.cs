using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

namespace EventBus
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public static event Action<GameState> OnPrepareGameStateChange;
        public static event Action<GameState> OnChangeGameState;
        public GameState State { get; private set; }



        void Start() => ChangeGameState(GameState.SystemsLoaded);
  


        public void ChangeGameState(GameState newState)
        {
            OnPrepareGameStateChange?.Invoke(newState);

            State = newState;

            switch (newState)
            {
                case GameState.SystemsLoaded:
                    EventBus.Trigger(new GameLaunchEvent());
                    break;
            }

            OnChangeGameState?.Invoke(newState);
        }
    }



        [Serializable]
        public enum GameState
        {
            SystemsLoaded = 0,
            Main = 1,
            StartLevel = 2,
            Playing = 3,
            Paused = 4,
            Lost = 5,
            Quit = 9,
        }


        [Serializable]
        public enum GameStateLayer
        {

        }
    
}