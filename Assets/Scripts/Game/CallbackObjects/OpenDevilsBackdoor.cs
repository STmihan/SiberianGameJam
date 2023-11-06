using UnityEngine;

namespace Game.CallbackObjects
{
    public class OpenDevilsBackdoor : CallbackObject
    {
        [SerializeField] private GameObject _door;
        
        public override void Callback(object payload = null)
        {
            _door.SetActive(false);
        }
    }
}