using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class Buyable
{
    public string name;
    public int startingPrice;
    public float increaseBy;
    [HideInInspector] public int timesBuy;
    public UnityEvent onBuy;
    public UnityEvent<string> onDisplay;
}
public class GlobalStats : MonoBehaviour
{
    public static GlobalStats self;

    [SerializeField] private Buyable[] buyables; 
    public Dictionary<string, Buyable> buyablesDict = new Dictionary<string, Buyable>();

    [SerializeField] private UnityEvent<string> onTiles;

    public int tiles;
    public int Tiles
    {
        get
        {
            return tiles;
        }
        set
        {
            tiles = value;
            onTiles.Invoke(tiles.ToString());
        }
    }
    public bool canPlace = true;

    private int CalculatePrice(Buyable buy)
    {
        return buy.startingPrice + (int)(buy.increaseBy * buy.timesBuy);
    }
    public bool CanBuy(Buyable buy)
    {
        int price = CalculatePrice(buy);
        if (Tiles - price < 0) return false;
        return true;
    }
    public void RefreshPrice(string name)
    {
        Buyable target = buyablesDict[name];
        target.onDisplay.Invoke(CalculatePrice(target).ToString());
    }
    public void Buy(string name)
    {
        Buyable target = buyablesDict[name];
        int price = CalculatePrice(target);

        if (!CanBuy(target)) return;
        Tiles -= price;

        target.onBuy.Invoke();
        target.timesBuy++;
        RefreshPrice(name);
    }
    private void Awake()
    {
        self = this;
        Tiles = tiles;

        foreach (var buy in buyables)
        {
            buyablesDict.Add(buy.name, buy);
        }
    }
}
