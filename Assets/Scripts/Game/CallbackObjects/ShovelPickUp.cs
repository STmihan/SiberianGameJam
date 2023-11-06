using VContainer;

namespace Game.CallbackObjects
{
    public class ShovelPickUp : CallbackObject
    {
        public override void Callback(object payload = null)
        {
            GameController.SendEvent(GameEvent.ShovelPickUp);
        }
    }
}