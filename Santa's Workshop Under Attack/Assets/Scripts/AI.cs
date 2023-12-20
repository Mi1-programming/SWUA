using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AI : MonoBehaviour
{
    public bool FindElf;
    public bool FindEnemy;
    public bool FindPart;

    public int Damage = 20;
    public float WeaponSpeed = 1f;
    public float WeaponRange = 1f;

    float weaponCooldown = 0f;
    float findCooldown = 0f;
    float touchDis = 3f;

    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    Health targetHealth;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null && !target.CompareTag("Destroyed"))
        {
            agent.SetDestination(target.position);

            float distance = Vector3.Distance(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z));
            if (weaponCooldown <= 0f && distance <= touchDis + WeaponRange)
            {
                UseWeapon();
                weaponCooldown = 1 / WeaponSpeed;
            }
        }
        else
        {
            if (findCooldown <= 0f)
            {
                target = GameController.FindClosest(transform.position, FindElf, FindEnemy, FindPart);
                if (target)
                {
                    targetHealth = target.GetComponent<Health>();
                    touchDis = GameController.GetTouchDistance(transform, target);
                }
                else findCooldown = 1f;
            }
            else findCooldown -= Time.deltaTime;
        }
        if (weaponCooldown > 0f) weaponCooldown -= Time.deltaTime;
    }

    void UseWeapon()
    {
        targetHealth.TakeHP(Damage);
    }
}
