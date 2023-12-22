using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public int Damage = 20;
    public bool HealInstead = false;

    public abstract void UseWeapon(Vector3 pos, Vector3 targetPos);

    /*
     If close enough then use weapon

    Melee:
    Shoot a raycast and damage the hitted object if it was the target

    Range:
    Shoot a raycast, if it hits the target, shoot the actual "bullet" (which will do damage)
     */
}
