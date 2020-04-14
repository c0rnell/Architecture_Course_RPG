using System.Collections;
using a_player;
using NSubstitute;
using NUnit.Framework;
using StateMachines;
using UnityEngine;
using UnityEngine.TestTools;

namespace state_machine
{
    public class entity_state_machine
    {
        private EntityStateMachine statMachine;
        private Player player;

        [UnitySetUp]
        public IEnumerator Init()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
            
            yield return Helpers.LoadEntitySTateMachineTestsScene();
            player = Helpers.GetPlayer();
            
            statMachine =  Object.FindObjectOfType<EntityStateMachine>();
        }
        
        [UnityTest]
        public IEnumerator start_in_idle_state()
        {
            Assert.AreEqual(typeof(Idle), statMachine.CurrentStateType);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator switches_to_chase_player_when_player_in_range()
        {
            player.transform.position = statMachine.transform.position + new Vector3(5.1f, 0);
            yield return null;
            
            Assert.AreEqual(typeof(Idle), statMachine.CurrentStateType);
            
            player.transform.position = statMachine.transform.position + new Vector3(4.9f, 0);
            yield return null;
            
            Assert.AreEqual(typeof(ChasePlayer), statMachine.CurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator switches_to_dead_when_health_reaches_zero()
        {
            var entity = statMachine.GetComponent<Entity>();
            
            yield return null;
            Assert.AreEqual(typeof(Idle), statMachine.CurrentStateType);
            
            entity.TakeHit(entity.Health - 1);
            yield return null;
            Assert.AreEqual(typeof(Idle), statMachine.CurrentStateType);
            
            entity.TakeHit(entity.Health);
            yield return null;
            Assert.AreEqual(typeof(Dead), statMachine.CurrentStateType);
        }
    }
}