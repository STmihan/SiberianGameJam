namespace Game.CallbackObjects
{
    public class OpenChessInDungeonCallback : CallbackObject
    {
        public override void Callback(object payload = null)
        {
            GameController.SendEvent(GameEvent.OpenChessInDungeon);
        }
    }
}