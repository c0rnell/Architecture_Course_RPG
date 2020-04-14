using System;
using System.Collections.Generic;
using StateMachines;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StateMachines
{
    public class GameStateMachine : MonoBehaviour
    {
        private static GameStateMachine _instance;
        private static bool _initialized;
        
        private StateMachine _stateMachine;
        public static event Action<IState> OnGameStateChanged;
        public Type CurrentState => _stateMachine.CurrentState.GetType();

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            
            _initialized = true;
            DontDestroyOnLoad(gameObject);
            _stateMachine = new StateMachine();
            _stateMachine.OnStateChanged += state => OnGameStateChanged?.Invoke(state);
            
            var menu  = new Menu();
            var loading = new LoadLevel();
            var play = new Play();
            var pause = new Pause(PlayerInput.Instance);
            
            _stateMachine.SetState(menu);
            _stateMachine.AddTransition(menu, loading, () => PlayButton.LevelToLoad != null);
            _stateMachine.AddTransition(loading, play, loading.Finished);
            _stateMachine.AddTransition(play, pause, pause.Paused);
            _stateMachine.AddTransition(pause, play, pause.Paused);
            _stateMachine.AddTransition(pause, menu, () => RestartButton.Pressed);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}

public class Menu : IState
{
    public void OnEnter()
    {
        PlayButton.LevelToLoad = null;
        SceneManager.LoadSceneAsync("Menu");
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}

public class Play : IState
{
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}

public class LoadLevel : IState
{
    public bool Finished() => _operations.TrueForAll(t => t.isDone);
    
    private List<AsyncOperation> _operations = new List<AsyncOperation>();
    
    public void OnEnter()
    {
        _operations.Add(SceneManager.LoadSceneAsync(PlayButton.LevelToLoad));
        _operations.Add(SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive));
    }

    public void OnExit()
    {
        _operations.Clear();
    }

    public void Tick()
    {
    }
}

public class Pause : IState
{
    public static bool Active { get; private set; }
    
    public bool Paused() => m_Input.PausePressed;

    private IPlayerInput m_Input;

    public Pause(IPlayerInput mInput)
    {
        m_Input = mInput;
    }

    public void OnEnter()
    {
        Active = true;
        Time.timeScale = 0;
    }

    public void OnExit()
    {
        Active = false;
        Time.timeScale = 1;
    }

    public void Tick()
    {
    }
}