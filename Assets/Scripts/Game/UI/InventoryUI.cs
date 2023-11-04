using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class InventoryUI : MonoBehaviour
    {
        private readonly List<Item> _items = new();

        public void AddItem(string key)
        {
            var item = Item.GetItem(key);
            if (item != null)
            {
                _items.Add(item);
            }
        }
        
        public void RemoveItem(string key)
        {
            _items.RemoveAll(item => item.Key == key);
        }
    }
}