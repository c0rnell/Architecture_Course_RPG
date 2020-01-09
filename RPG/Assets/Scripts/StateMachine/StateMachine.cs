using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StateMachines
{
    public class StateMachine
    {
        private List<StateTransition> m_StateTransitions = new List<StateTransition>();
        private List<StateTransition> m_AnyStateTransitions = new List<StateTransition>();
        private IState m_CurrentState;
        public IState CurrentState => m_CurrentState;
        public void AddTransition(IState from, IState to, Func<bool> condition)
        {
            var transition = new StateTransition(from, to, condition);
            m_StateTransitions.Add(transition);
        }
        
        public void AddAnyTransition(IState state, Func<bool> condition)
        {
            var stateTransition = new StateTransition(null, state, condition);
            m_AnyStateTransitions.Add(stateTransition);
        }

        public void SetState(IState state)
        {
            if(m_CurrentState == state)
                return;
            
            m_CurrentState?.OnExit();
            
            m_CurrentState = state;
            Debug.Log($"Changed to state {state}");
            m_CurrentState.OnEnter();
        }

        public void Tick()
        {
            var transition = CheckFroTransition();
            if (transition != null)
                SetState(transition.To);
            
            m_CurrentState.Tick();
        }

        private StateTransition CheckFroTransition()
        {
            foreach (StateTransition anyStateTransition in m_AnyStateTransitions)
            {
                if (anyStateTransition.Condition())
                    return anyStateTransition;
            }

            foreach (var stateTransition in m_StateTransitions)
            {
                if (stateTransition.From == m_CurrentState && stateTransition.Condition())
                    return stateTransition;
            }
            return null;
        }
    }
}