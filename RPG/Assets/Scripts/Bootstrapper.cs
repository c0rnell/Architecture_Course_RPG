using UnityEngine;

namespace DefaultNamespace
{
    
    public class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Initialize()
        {
            var inputGameObject = new GameObject("[INPUT SYSTEM]");
            inputGameObject.AddComponent<PlayerInput>();
            Object.DontDestroyOnLoad(inputGameObject);
        }
    }
}