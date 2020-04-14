using System.Collections;
using a_player;
using NSubstitute;
using NUnit.Framework;
using StateMachines;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;
using Object = UnityEngine.Object;

namespace state_machine
{
    public class game_state_machine
    {
        [SetUp]
        public void setup()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
        }
        
        [UnityTearDown]
        public IEnumerator Teardown()
        {
            GameObject.Destroy( Object.FindObjectOfType<GameStateMachine>());
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator switches_to_loading_when_level_to_load_selected()
        {
            yield return Helpers.LoadMenuScene();
            var stateMachine = Object.FindObjectOfType<GameStateMachine>();
            
            Assert.AreEqual(typeof(Menu), stateMachine.CurrentState);

            PlayButton.LevelToLoad = "Level1";
            
            yield return null;
            
            Assert.AreEqual(typeof(LoadLevel), stateMachine.CurrentState);
        }
        
        [UnityTest]
        public IEnumerator switches_to_play_when_level_to_load_completed()
        {
            yield return Helpers.LoadMenuScene();
            var stateMachine = Object.FindObjectOfType<GameStateMachine>();
            
            Assert.AreEqual(typeof(Menu), stateMachine.CurrentState);

            PlayButton.LevelToLoad = "Level1";
            
            yield return null;
            
            Assert.AreEqual(typeof(LoadLevel), stateMachine.CurrentState);
            
            yield return new WaitUntil(() => stateMachine.CurrentState == typeof(Play));
            
            Assert.AreEqual(typeof(Play), stateMachine.CurrentState);
        }
        
        [UnityTest]
        public IEnumerator switches_from_play_to_pause_when_pause_button_pressed()
        {
            yield return Helpers.LoadMenuScene();
            var stateMachine = Object.FindObjectOfType<GameStateMachine>();

            PlayButton.LevelToLoad = "Level1";

            yield return new WaitUntil(() => stateMachine.CurrentState == typeof(Play));

            // hit pause
            PlayerInput.Instance.PausePressed.Returns(true);

            yield return null;
            Assert.AreEqual(typeof(Pause), stateMachine.CurrentState);
        }

        [UnityTest]
        public IEnumerator only_allows_one_instance_to_exists()
        {
            var firstStateMachine = new GameObject("FirstStateMachine").AddComponent<GameStateMachine>();
            var secondStateMachine = new GameObject("SecondStateMachine").AddComponent<GameStateMachine>();

            yield return null;
            
            Assert.IsNull(secondStateMachine);
            Assert.IsNotNull(firstStateMachine);
        }
    }
}