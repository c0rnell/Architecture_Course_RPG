using System.Collections;
using NSubstitute;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        public static IEnumerator LoadEntitySTateMachineTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("EntityStateMachineTests");
            while (operation.isDone == false)
            {
                yield return null;
            }
        }
        
        public static IEnumerator LoadItemTestScene()
        {
            var operation = SceneManager.LoadSceneAsync("ItemTests");
            while (operation.isDone == false)
            {
                yield return null;
            }
            
            operation = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            while (operation.isDone == false)
            {
                yield return null;
            }
        }

        public static Player GetPlayer()
        {
            var player = Object.FindObjectOfType<Player>();
            //var testPlayerInput = Substitute.For<IPlayerInput>();
            
            //player.PlayerInput = testPlayerInput;

            return player;
        }

        public static float CalcualteTurn(Quaternion originalRotation, Quaternion transformRotation)
        {
            var cross = Vector3.Cross(originalRotation * Vector3.forward, transformRotation * Vector3.forward);
            var dot = Vector3.Dot(cross, Vector3.up);

            return dot;
        }

        public static IEnumerator LoadMenuScene()
        {
            SceneManager.LoadSceneAsync("Menu");
            
            var operation = SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
            while (operation.isDone == false)
            {
                yield return null;
            }
        }
    }
}