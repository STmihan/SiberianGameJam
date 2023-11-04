using UnityEngine;

namespace Game.CallbackObjects
{
    public class TestCallbackObject : CallbackObject
    {
        public override void Callback()
        {
            Debug.Log("Test callback object called");
        }
    }
}