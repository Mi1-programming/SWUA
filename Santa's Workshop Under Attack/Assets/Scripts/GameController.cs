using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public List<GameObject> Elfs = new();
    public List<GameObject> Enemies = new();
    public List<GameObject> CastleParts = new();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameObject FindClosest(Vector3 pos, bool findElf, bool findEnemy, bool findPart)
    {
        GameObject closest = null;
        float closestDistance = -1;

        if (findElf)
        {
            foreach (GameObject g in Instance.Elfs)
            {
                float distance = Vector3.Distance(pos, g.transform.position);
                if (distance < closestDistance || closestDistance == -1)
                {
                    closest = g;
                    closestDistance = distance;
                }
            }
        }
        if (findEnemy)
        {
            foreach (GameObject g in Instance.Enemies)
            {
                float distance = Vector3.Distance(pos, g.transform.position);
                if (distance < closestDistance || closestDistance == -1)
                {
                    closest = g;
                    closestDistance = distance;
                }
            }
        }
        if (findPart)
        {
            foreach (GameObject g in Instance.CastleParts)
            {
                float distance = Vector3.Distance(pos, g.transform.position);
                if (distance < closestDistance || closestDistance == -1)
                {
                    closest = g;
                    closestDistance = distance;
                }
            }
        }

        return closest;
    }
}
