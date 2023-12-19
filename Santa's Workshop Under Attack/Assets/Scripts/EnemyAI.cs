using System;

using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;


    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("collided with " + collision.gameObject.name);
    }
}
