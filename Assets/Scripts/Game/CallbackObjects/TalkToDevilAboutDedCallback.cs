namespace Game.CallbackObjects
{
    public class TalkToDevilAboutDedCallback : CallbackObject
    {
        public override void Callback(object payload = null)
        {
            GameController.SendEvent(GameEvent.TalkToDevilAboutDed);
        }
    }
}