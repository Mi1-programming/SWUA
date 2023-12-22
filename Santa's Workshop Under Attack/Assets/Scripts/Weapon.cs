using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public int Damage = 20;
    public bool HealInstead = false;
    public float Range = 1f;

    // condition -> a percentage about how good the weapon is. variable is between 0-1
    // this returns true if it was able to use weapon
    public abstract bool TryToUseWeapon(Vector3 pos, Vector3 dir, float condition, float touchDis, Transform target, Transform sender, GameObject weaponObj);


    public void ActionToTarget(Transform target, float condition)
    {
        if (!target.GetComponent<Health>()) { Debug.LogError("Anvil or an object/enemy doesn't have a Health script. All targetable objects should have Health script"); return; }

        if (!HealInstead) target.GetComponent<Health>().TakeHP(Mathf.RoundToInt(Damage * condition));
        else target.GetComponent<Health>().GiveHP(Mathf.RoundToInt(Damage * condition));
    }

    /*
     If close enough then use weapon

    Melee:
    Shoot a raycast and damage the hitted object if it was the target

    Range:
    Shoot a raycast, if it hits the target, shoot the actual "bullet" (which will do damage)
     */
}
