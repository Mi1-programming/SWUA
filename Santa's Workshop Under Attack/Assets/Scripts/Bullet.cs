using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform sender;
    Transform target;
    int damage;
    bool healInstead;

    bool allowColDetect = false;

    public IEnumerator Life(float time, int _damage, bool _healInstead, Transform _target, Transform _sender)
    {
        damage = _damage;
        healInstead = _healInstead;
        target = _target;
        sender = _sender;

        yield return new WaitForEndOfFrame();
        allowColDetect = true;
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!allowColDetect) return;

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
        else if (collision.transform != sender)
        {
            Destroy(gameObject);
        }
    }
}
