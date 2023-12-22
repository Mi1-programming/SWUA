public class SendToGiftButton : ConditionalButton
{
    public override bool Condition()
    {
        return GlobalStats.self.canPlace;
    }
}
