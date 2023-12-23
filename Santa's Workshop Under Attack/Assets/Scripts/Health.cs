using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    public int HP = 100;
    public bool GameoverOnDeath = false;
    //public bool BreakWhenDead = false;

    [Header("Events")]
    public UnityEvent OnDamage;
    public UnityEvent OnHeal;
    public UnityEvent OnDeath;
    public UnityEvent<float> OnHpChange;

    private float maxHealth;

    private void Start()
    {
        maxHealth = HP;
    }

    public void TakeHP(int amount)
    {
        HP -= amount;
        OnDamage.Invoke();

        OnHpChange.Invoke((float)HP / (float)maxHealth);

        if (HP <= 0)
        {
            /*if (BreakWhenDead) BreakAction();
            else DeathAction();*/
            DeathAction();
            if (GameoverOnDeath) GameController.Instance.GameOver();
        }
    }
    public void GiveHP(int amount)
    {
        HP += amount;
        if (HP > maxHealth) maxHealth = HP;
        OnHeal.Invoke();
        OnHpChange.Invoke((float)HP / (float)maxHealth);
    }

    void DeathAction()
    {
        OnDeath.Invoke();
        Destroy(gameObject);
    }

    /*void BreakAction()
    {
        transform.localScale /= 10;
        tag = "Destroyed";
    }*/
}
