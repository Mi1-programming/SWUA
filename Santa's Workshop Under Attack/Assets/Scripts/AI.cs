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
            if (agent.isStopped) agent.isStopped = false;
            agent.SetDestination(target.position);

            
            float distance = Vector3.Distance(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z));

            // Use weapon if cooldown 0, distance is small enough, ...
            if (weaponCooldown <= 0f && distance <= touchDis + WeaponRange)
            {
                // Shoot a raycast to see if AI can see the target
                RaycastHit hit;
                Vector3 dir = (target.position - transform.position).normalized;
                if (Physics.Raycast(transform.position, dir, out hit, touchDis + WeaponRange))
                {
                    if (hit.transform == target)
                    {
                        UseWeapon();
                    }
                    //print($"{name} | hit = {hit.transform.name}   target = {target.name}");
                    Debug.DrawRay(transform.position, dir * hit.distance, Color.red);
                }
                weaponCooldown = 1 / WeaponSpeed;
            }
        }
        else
        {
            if (findCooldown <= 0f)
            {
                if (!agent.isStopped) agent.isStopped = true;
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
