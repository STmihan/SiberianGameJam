namespace Game.CallbackObjects
{
    public class TalkToDedCallback : CallbackObject
    {
        public override void Callback(object payload = null)
        {
            GameController.SendEvent(GameEvent.TalkToDed);
        }
    }
}