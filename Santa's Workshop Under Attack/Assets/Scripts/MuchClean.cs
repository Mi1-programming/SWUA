using UnityEngine;
using System.Collections.Generic;

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
    public static bool[][][] CoordToBuild(Vector3Int[] coords, int size)
    {
        bool[][][] newBuild = Create3DMatrix(size);
        foreach (var coord in coords)
        {
            newBuild[coord.x][coord.y][coord.z] = true;
        }
        return newBuild;
    }
    public static Vector3Int[] BuildToCoord(bool[][][] build, int size)
    {
        List<Vector3Int> coords = new List<Vector3Int>();
        int x = 0;
        for (x = 0; x < size; x++)
        {
            int y = 0;
            for (y = 0; y < size; y++)
            {
                int z = 0;
                for (z = 0; z <size ; z++)
                {
                    bool tile = build[x][y][z];
                    if (tile) coords.Add(new Vector3Int(x, y, z));
                }
            }
        }
        return coords.ToArray();
    }
}