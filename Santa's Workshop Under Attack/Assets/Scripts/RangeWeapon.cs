using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RangeWeapon", menuName = "Weapon/RangeWeapon")]
public class RangeWeapon : Weapon
{
    public float BulletSpeed = 10f;
    public float BulletLife = 3f;

    public override bool TryToUseWeapon(Vector3 pos, Vector3 dir, float condition, float touchDis, Transform target, Transform sender, GameObject weaponObj)
    {
        RaycastHit hit;
        Collider senderCollider = sender.GetComponent<Collider>();
        senderCollider.enabled = false;
        if (Physics.Raycast(pos, dir, out hit, touchDis + Range))
        {
            senderCollider.enabled = true;
            if (hit.transform == target)
            {
                // Instantiate the weaponObj as a bullet and give it velocity and rotation
                GameObject obj = Instantiate(weaponObj);
                Animator anim = obj.GetComponent<Animator>();
                if (anim != null) Destroy(anim);

                obj.transform.position = pos + dir;
                obj.transform.rotation = Quaternion.LookRotation(dir);
                obj.transform.Rotate(90, 0, 0);
                obj.transform.localScale = weaponObj.transform.lossyScale;
                obj.GetComponent<Collider>().enabled = true;
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.velocity = dir * BulletSpeed;

                // Add a bullet script to the obj
                Bullet b = obj.AddComponent<Bullet>();
                b.StartCoroutine(b.Life(BulletLife, Mathf.RoundToInt(Damage * condition), HealInstead, target, sender));

                return true;
            }
        }
        senderCollider.enabled = true;
        return false;
    }
}
