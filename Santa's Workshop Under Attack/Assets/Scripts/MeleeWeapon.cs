using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MeleeWeapon", menuName = "Weapon/MeleeWeapon")]
public class MeleeWeapon : Weapon
{
    public override bool TryToUseWeapon(Vector3 pos, Vector3 dir, float condition, float touchDis, Transform target, Transform sender, GameObject weaponObj)
    {
        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, touchDis + Range))
        {
            if (hit.transform == target)
            {
                ActionToTarget(target, condition);
                return true;
            }
        }
        return false;
    }
}
