using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LootSystem : MonoBehaviour
{
    [SerializeField]
    private AssetReference _lootItemHolderPrefab;
    private static LootSystem _instance;

    private Queue<LootItemHolder> _holdersPool = new Queue<LootItemHolder>();
    
    private void Awake()
    {
        if(_instance != null)
            Destroy(gameObject);
        _instance = this;
    }

    public static async void Drop(Item item, Transform droppingTransform)
    {
        var lootItemHolder = await _instance.GetLootItemHolder();
        Vector2 insideUnitCircle = UnityEngine.Random.insideUnitCircle * 2;
        Vector3 randomPosition = droppingTransform.position + new Vector3(insideUnitCircle.x, 0, insideUnitCircle.y);
        lootItemHolder.transform.position = randomPosition;
        lootItemHolder.TakeItem(item);
        item.WasPickedUp = false;
    }

    private async Task<LootItemHolder> GetLootItemHolder()
    {
        if (_holdersPool.Any())
        {
            var holder = _holdersPool.Dequeue();
            holder.gameObject.SetActive(true);
            return holder;
        }

        var operation = _lootItemHolderPrefab.InstantiateAsync();
        var result = await operation.Task;
        var lootItemHolder = result.GetComponent<LootItemHolder>();
        return lootItemHolder;
    }

    public static void AddToPool(LootItemHolder lootItemHolder)
    {
        lootItemHolder.gameObject.SetActive(false);
        _instance._holdersPool.Enqueue(lootItemHolder);
    }
}