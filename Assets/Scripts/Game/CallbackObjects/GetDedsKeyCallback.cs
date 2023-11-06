namespace Game.CallbackObjects
{
    public class GetDedsKeyCallback : CallbackObject
    {
        
        public override void Callback(object payload = null)
        {
            GameController.SendEvent(GameEvent.DiaryKeyPickUp);
        }
    }
}