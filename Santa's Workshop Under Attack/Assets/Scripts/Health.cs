using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    public int HP = 100;
    public bool BreakWhenDead = false;

    [Header("Events")]
    public UnityEvent OnDamage;
    public UnityEvent OnHeal;
    public UnityEvent OnDeath;

    public void TakeHP(int amount)
    {
        HP -= amount;
        OnDamage.Invoke();

        if (HP <= 0)
        {
            if (BreakWhenDead) BreakAction();
            else DeathAction();
        }
    }
    public void GiveHP(int amount)
    {
        HP += amount;
        OnHeal.Invoke();
    }

    void DeathAction()
    {
        OnDeath.Invoke();
        Destroy(gameObject);
    }

    void BreakAction()
    {
        transform.localScale /= 10;
        tag = "Destroyed";
    }
}
