using System.Collections.Generic;
using System.Linq;
using Game.Data;
using UnityEngine;

namespace Game.UI
{
    public class InventoryUI : MonoBehaviour, IUI
    {
        [SerializeField] private RectTransform _itemContainer;

        private readonly List<Item> _items = new();
        private readonly Dictionary<string, GameObject> _itemObjects = new();

        public void AddItem(string key)
        {
            var item = Item.GetItem(key);
            if (item != null)
            {
                _items.Add(item);
                GameObject itemObject = Instantiate(item.GetUIPrefab(), _itemContainer);
                _itemObjects[key] = itemObject;
            }
        }

        public void RemoveItem(string key)
        {
            var item = _itemObjects[key];
            if (item != null)
            {
                _items.Remove(_items.First(i => i.Key == key));
                _itemObjects.Remove(key);
                Destroy(item);
            }
        }

        public bool HasItem(string key)
        {
            return _items.Any(i => i.Key == key);
        }
    }
}