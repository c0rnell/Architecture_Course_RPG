using UnityEngine;

namespace StateMachines
{
    public class Idle : IState {
        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            Debug.Log("Idle");
        }
    }
}