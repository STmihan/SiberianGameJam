namespace Game.CallbackObjects
{
    public class OpenShopBookCallback : CallbackObject
    {
        public override void Callback(object payload = null)
        {
            GameController.SendEvent(GameEvent.ShopBook);
        }
    }
}