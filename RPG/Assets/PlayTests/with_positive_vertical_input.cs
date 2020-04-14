using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

namespace a_player
{
    public class player_input_test
    {
        [SetUp]
        public void Setup()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
        }
    }
    
    public class with_positive_vertical_input : player_input_test
    {
        [UnityTest]
        public IEnumerator moves_Forward()
        {
            yield return Helpers.LoadMovementTestScene();
            
            var player = Helpers.GetPlayer();
            
            PlayerInput.Instance.Vertical.Returns(1);

            float startingZPosition = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);

            float endingZPosition = player.transform.position.z;
            Assert.Greater(endingZPosition, startingZPosition);
        }
    }
    
    public class with_negative_vertical_input : player_input_test
    {
        [UnityTest]
        public IEnumerator moves_Back()
        {
            yield return Helpers.LoadMovementTestScene();
            
            var player = Helpers.GetPlayer();
            
            PlayerInput.Instance.Vertical.Returns(-1);

            float startingZPosition = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);

            float endingZPosition = player.transform.position.z;
            Assert.Less(endingZPosition, startingZPosition);
        }
    }

    public class with_negative_mouse_X : player_input_test
    {
        [UnityTest]
        public IEnumerator turns_left()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayer();

            PlayerInput.Instance.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalcualteTurn(originalRotation, player.transform.rotation);
            
            Assert.Greater(turnAmount, 0f);
        }
    }
    
    public class with_positive_mouse_X : player_input_test
    {
        [UnityTest]
        public IEnumerator turns_right()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayer();

            PlayerInput.Instance.MouseX.Returns(1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalcualteTurn(originalRotation, player.transform.rotation);
            
            Assert.Less(turnAmount, 0f);
        }
    }
}