using System;
using StateMachines;
using UnityEngine;

namespace UI
{
    public class PauseCanvas : MonoBehaviour
    {
        [SerializeField]
        private GameObject PausePanel;
        
        private void Awake()
        {
            PausePanel.gameObject.SetActive(false);
            GameStateMachine.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameStateMachine.OnGameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(IState state)
        {
            PausePanel.gameObject.SetActive(state is Pause);
        }
    }
}