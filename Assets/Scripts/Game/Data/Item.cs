using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Create Item", fileName = "Item", order = 0)]
    public class Item : ScriptableObject
    {
        [field: SerializeField]
        public string Key { get; private set; }
        [field: SerializeField]
        public string Name { get; private set; }

        private const string DataPath = "Data/Items/";
        private const string PrefabPath = "Prefabs/Items/UI/";

        public static Item GetItem(string key)
        {
            var item = Resources.Load<Item>(DataPath + key);
            if (item == null)
            {
                Debug.LogError($"Item {key} not found.");
                return null;
            }

            return item;
        }

        public GameObject GetUIPrefab()
        {
            var prefab = Resources.Load<GameObject>(PrefabPath + Key);
            if (prefab == null)
            {
                Debug.LogError($"Item UI prefab {Key} not found.");
                return null;
            }

            return prefab;
        }
    }
}