using UnityEngine;

namespace Game.CallbackObjects
{
    public class TestCallbackObject : CallbackObject
    {
        public override void Callback(object payload = null)
        {
            Debug.Log("Test callback object called");
        }
    }
}
