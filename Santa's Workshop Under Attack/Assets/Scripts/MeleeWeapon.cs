using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MeleeWeapon", menuName = "Weapon/MeleeWeapon")]
public class MeleeWeapon : Weapon
{
    public override bool TryToUseWeapon(Vector3 pos, Vector3 dir, float condition, float touchDis, Transform target, Transform sender, GameObject weaponObj)
    {
        RaycastHit hit;
        Collider senderCollider = sender.GetComponent<Collider>();
        senderCollider.enabled = false;
        if (Physics.Raycast(pos, dir, out hit, touchDis + Range))
        {
            senderCollider.enabled = true;

            Debug.DrawRay(pos, dir, Color.red, hit.distance);
            if (hit.transform == target)
            {
                ActionToTarget(target, condition);
                return true;
            }
        }
        senderCollider.enabled = true;
        return false;
    }
}
