using System;

using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public bool FindElf;
    public bool FindPart;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;

    private void Start()
    {
        target = GameController.FindClosest(transform.position, FindElf, false, FindPart).transform;
    }

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
        
    }
}
