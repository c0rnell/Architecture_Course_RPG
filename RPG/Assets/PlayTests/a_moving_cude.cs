using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class a_moving_cude
    {
        [UnityTest]
        public IEnumerator moving_forward_changes_position()
        {
            // ARRANGE
            var cude = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cude.transform.position = Vector3.zero;
            
            // ACT
            for (int i = 0; i < 10; i++)
            {
                cude.transform.position += Vector3.forward;
                yield return null;
                
                // ASSERT
                Assert.AreEqual(i + 1, cude.transform.position.z);
            }
 
            yield return null;
        }
    }
}
