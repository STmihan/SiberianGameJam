namespace Game.CallbackObjects
{
    public class ReadDiaryCallback : CallbackObject
    {
        public override void Callback(object payload = null)
        {
            GameController.SendEvent(GameEvent.ReadDiary);
        }
    }
}