using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

namespace a_player
{
    public static class Helpers
    {
        public static void CreateFloor()
        {
            var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.localScale = new Vector3(50,0.1f,50);
            floor.transform.position = Vector3.zero;
        }

        public static Player CreatePlayer()
        {
            var playerGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerGameObject.AddComponent<CharacterController>();
            playerGameObject.AddComponent<NavMeshAgent>();
            playerGameObject.transform.position = new Vector3(0,1.3f, 0);
            
            var player = playerGameObject.AddComponent<Player>();
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
            Helpers.CreateFloor();
            var player = Helpers.CreatePlayer();
            
            player.PlayerInput.Vertical.Returns(1);

            float startingZPosition = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);

            float endingZPosition = player.transform.position.z;
            Assert.Greater(endingZPosition, startingZPosition);
        }
    }

    public class with_negative_mouse_X
    {
        [UnityTest]
        public IEnumerator turns_left()
        {
            Helpers.CreateFloor();
            var player = Helpers.CreatePlayer();

            player.PlayerInput.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalcualteTurn(originalRotation, player.transform.rotation);
            
            Assert.Less(turnAmount, 0f);
        }
    }
}