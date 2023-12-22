using UnityEngine;
using UnityEngine.Events;

public class GlobalStats : MonoBehaviour
{
    public static GlobalStats self;
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

    private void Awake()
    {
        self = this;
        Tiles = tiles;
    }
}
