using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace a_player
{
    public static class Helpers
    {
        public static IEnumerator LoadMovementTestScene()
        {
            var operation = SceneManager.LoadSceneAsync("MovementTests");
            while (operation.isDone == false)
            {
                yield return null;
            }
        }

        public static Player GetPlayer()
        {
            var player = Object.FindObjectOfType<Player>();
            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = testPlayerInput;

            return player;
        }

        public static float CalcualteTurn(Quaternion originalRotation, Quaternion transformRotation)
        {
            var cross = Vector3.Cross(originalRotation * Vector3.forward, transformRotation * Vector3.forward);
            var dot = Vector3.Dot(cross, Vector3.up);

            return dot;
        }
    }
    
    public class with_positive_vertical_input
    {
        [UnityTest]
        public IEnumerator moves_Forward()
        {
            yield return Helpers.LoadMovementTestScene();
            
            var player = Helpers.GetPlayer();
            
            player.PlayerInput.Vertical.Returns(1);

            float startingZPosition = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);

            float endingZPosition = player.transform.position.z;
            Assert.Greater(endingZPosition, startingZPosition);
        }
    }
    
    public class with_negative_vertical_input
    {
        [UnityTest]
        public IEnumerator moves_Back()
        {
            yield return Helpers.LoadMovementTestScene();
            
            var player = Helpers.GetPlayer();
            
            player.PlayerInput.Vertical.Returns(-1);

            float startingZPosition = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);

            float endingZPosition = player.transform.position.z;
            Assert.Less(endingZPosition, startingZPosition);
        }
    }

    public class with_negative_mouse_X
    {
        [UnityTest]
        public IEnumerator turns_left()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayer();

            player.PlayerInput.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalcualteTurn(originalRotation, player.transform.rotation);
            
            Assert.Less(turnAmount, 0f);
        }
    }
    
    public class with_positive_mouse_X
    {
        [UnityTest]
        public IEnumerator turns_left()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayer();

            player.PlayerInput.MouseX.Returns(1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalcualteTurn(originalRotation, player.transform.rotation);
            
            Assert.Greater(turnAmount, 0f);
        }
    }
}