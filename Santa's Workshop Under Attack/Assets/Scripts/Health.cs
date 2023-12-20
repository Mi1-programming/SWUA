using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Health : MonoBehaviour
{
    public int HP = 100;
    public bool BreakWhenDead = false;
    public bool isElf = false;
    public bool isEnemy = false;

    public void TakeHP(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            if (BreakWhenDead) BreakAction();
            else DeathAction();
        }
    }
    public void GiveHP(int amount)
    {
        HP += amount;
    }

    void DeathAction()
    {
        GameController.Remove(gameObject, isElf, isEnemy);
        Destroy(gameObject);
    }

    void BreakAction()
    {
        transform.localScale /= 10;
        tag = "Destroyed";
    }
}
