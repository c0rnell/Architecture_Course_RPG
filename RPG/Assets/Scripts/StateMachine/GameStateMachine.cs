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
        private static bool _initialized;
        
        private StateMachine _stateMachine;
        public static event Action<IState> OnGameStateChanged;

        private void Awake()
        {
            if (_initialized)
            {
                Destroy(gameObject);
                return;
            }

            _initialized = true;
            DontDestroyOnLoad(gameObject);
            _stateMachine = new StateMachine();
            _stateMachine.OnStateChanged += state => OnGameStateChanged?.Invoke(state);
            
            var menu  = new Menu();
            var loading = new LoadLevel();
            var play = new Play();
            var pause = new Pause();
            
            _stateMachine.SetState(loading);
            _stateMachine.AddTransition(loading, play, loading.Finished);
            _stateMachine.AddTransition(play, pause, pause.Paused);
            _stateMachine.AddTransition(pause, play, pause.Paused);
            _stateMachine.AddTransition(pause, loading, () => RestartButton.Pressed);
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
        _operations.Add(SceneManager.LoadSceneAsync("Level1"));
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
    
    public bool Paused() => Input.GetKeyDown(KeyCode.Escape);
    
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