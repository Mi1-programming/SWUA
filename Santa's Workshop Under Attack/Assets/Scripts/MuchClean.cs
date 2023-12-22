using UnityEngine;

public class MuchClean : MonoBehaviour
{
    public static bool[][][] Create3DMatrix(int size)
    {
        bool[][][] target = new bool[size][][];
        int x = 0;
        for (x = 0; x < size; x++)
        {
            target[x] = new bool[size][];
            int y = 0;
            for (y = 0; y < size; y++)
            {
                target[x][y] = new bool[size];
                int z = 0;
                for (z = 0; z < size; z++)
                {
                    target[x][y][z] = false;
                }
            }
        }
        return target;
    }
}
