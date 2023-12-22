using UnityEngine;

public class UpgradeButton : ConditionalButton
{
    [SerializeField] string upgrade;
    public override bool Condition()
    {
        return GlobalStats.self.CanBuy(GlobalStats.self.buyablesDict[upgrade]);
    }
}
