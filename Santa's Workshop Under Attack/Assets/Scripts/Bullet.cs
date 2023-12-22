using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    int damage;
    bool healInstead;

    public IEnumerator Life(float time, int _damage, bool _healInstead, Transform _target)
    {
        damage = _damage;
        healInstead = _healInstead;
        target = _target;

        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == target)
        {
            Health h = target.GetComponent<Health>();
            if (h != null)
            {
                if (!healInstead) h.TakeHP(damage);
                else h.GiveHP(damage);
                Destroy(gameObject);
            }
        }
    }
}
