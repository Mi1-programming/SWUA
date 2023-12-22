using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour // T‰nne jonnekki tee se wave systeemi. voit k‰ytt‰‰ tai olla k‰ytt‰m‰tt‰ noita spawnAreaData hommia
{
    public static GameController Instance;

    public List<GameObject> Elfs = new();
    public List<GameObject> Enemies = new();
    public GameObject Anvil;

    public List<SpawnAreaData> SpawnAreas = new();

    [Header("Events")]
    public UnityEvent OnGameover;

    void Awake()
    {
        Instance = this;
        SpawnObjects();
    }
    
    void GameOver()
    {
        print("game over");
        OnGameover.Invoke();
    }



    void SpawnObjects()
    {
        foreach (SpawnAreaData area in SpawnAreas)
        {
            for (int i = 0; i < area.SpawnAmount; i++)
            {
                // SpawnPos somehow doesn't work... they spawn to the not instantiated object's position
                GameObject obj = Instantiate(area.SpawnObject);
                obj.SetActive(false);
                obj.transform.position = new Vector3(Random.Range(area.Pos1.x, area.Pos2.x), area.Pos1.y, Random.Range(area.Pos1.z, area.Pos2.z));
                obj.SetActive(true);
                if (area.isElf) Elfs.Add(obj);
                else if (area.isEnemy) Enemies.Add(obj);
            }
        }
    }


    #region Static Functions
    public static Transform FindClosest(Vector3 pos, bool findElf, bool findEnemy, bool findAnvil)
    {
        GameObject closest = null;
        float closestDistance = -1;

        if (findElf)
        {
            foreach (GameObject g in Instance.Elfs)
            {
                if (g == null) continue;
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
                if (g == null) continue;
                float distance = Vector3.Distance(pos, g.transform.position);
                if (distance < closestDistance || closestDistance == -1)
                {
                    closest = g;
                    closestDistance = distance;
                }
            }
        }
        if (findAnvil)
        {
            if (Instance.Anvil)
            {
                float distance = Vector3.Distance(pos, Instance.Anvil.transform.position);
                if (distance < closestDistance || closestDistance == -1)
                {
                    if (!Instance.Anvil.CompareTag("Destroyed"))
                    {
                        closest = Instance.Anvil;
                        closestDistance = distance;
                    }
                }
            }
        }

        if (closest) return closest.transform;
        return null;
    }
    
    public static void Remove(GameObject obj)
    {
        Instance.Elfs.Remove(obj);
        Instance.Enemies.Remove(obj);
        if (obj == Instance.Anvil)
        {
            Instance.GameOver();
        }
    }

    // Between you and target. This determines how far you have to be that you can hit each other when touching
    public static float GetTouchDistance(Transform you, Transform target)
    {
        float yourDis = Mathf.Min(you.localScale.x, you.localScale.z) * 0.5f;
        float targetDis = Mathf.Min(target.localScale.x, target.localScale.z) * 0.5f;
        return yourDis + targetDis;
    }
    #endregion
}
