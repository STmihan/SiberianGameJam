using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(menuName = "Create Item", fileName = "Item", order = 0)]
    public class Item : ScriptableObject
    {
        public string Key { get; set; }
        public string Name { get; set; }

        private const string PrefabsPath = "Prefabs/Items/";
        
        public static Item GetItem(string key)
        {
            var item = Resources.Load<Item>(PrefabsPath + key);
            if (item == null)
            {
                Debug.LogError($"Item {key} not found.");
                return null;
            }
            
            return item;
        }
    }
}