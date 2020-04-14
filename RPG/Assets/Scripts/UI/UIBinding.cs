using System.Collections;
using UnityEngine;

namespace UI
{
    public class UIBinding : MonoBehaviour
    {
        IEnumerator Start()
        {
            var player = FindObjectOfType<Player>();
            while (player == null)
            {
                yield return null;
                player = FindObjectOfType<Player>();
            }
            
            GetComponent<UIInventoryPanel>().Bind(player.GetComponent<Inventory>());
        }
    }
}