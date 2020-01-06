using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace a_player
{
    public class moving_into_an_item
    {
        [UnityTest]
        public IEnumerator picks_up_and_equip_item()
        {
            yield return Helpers.LoadItemTestScene();
            
            var player = Helpers.GetPlayer();
            
            player.PlayerInput.Vertical.Returns(1);
            Item item = Object.FindObjectOfType<Item>();
            
            Assert.AreNotSame(item, player.GetComponent<Inventory>().ActiveItem);
            
            yield return new WaitForSeconds(1f);

            Assert.AreSame(item, player.GetComponent<Inventory>().ActiveItem);
        }
        
        [UnityTest]
        public IEnumerator changes_crosshair_to_item_crosshair()
        {
            yield return Helpers.LoadItemTestScene();
            
            var player = Helpers.GetPlayer();
            var croshair = Object.FindObjectOfType<CrossHair>();
            
            Item item = Object.FindObjectOfType<Item>();
            
            Assert.AreNotSame(item.CrosshairDefinition.Sprite, croshair.GetComponent<Image>().sprite);

            item.transform.position = player.transform.position;
            yield return null;

            Assert.AreEqual(item.CrosshairDefinition.Sprite, croshair.GetComponent<Image>().sprite);
        }
    }
}