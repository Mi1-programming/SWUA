using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnAreaData
{
    public GameObject SpawnObject;
    public int SpawnAmount;

    public bool isElf = false;
    public bool isEnemy = false;

    public Vector3 Pos1;
    public Vector3 Pos2;
}
