using UnityEngine;

namespace Game.CallbackObjects
{
    public abstract class CallbackObject : MonoBehaviour
    {
        public abstract void Callback(object payload = null);
    }
}