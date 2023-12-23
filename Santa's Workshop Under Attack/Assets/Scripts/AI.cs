using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AI : MonoBehaviour // REMEMBER TO IMPLEMENT!! -> WHEN AI IS USING A WEAPON, IT IS NOT ROTATING, AND IT LOOKS AWFUL SOMETIMES
{
    [Header("Target types")] // Choose 1 or 2 target types (pro tip -> elf shouldn't target anvil haha)
    public bool FindElf;
    public bool FindEnemy;
    public bool FindAnvil;

    [Header("Settings")]
    public int Reward;
    public bool KeepCloseToSpawn;

    [Header("Weapon")]
    public Weapon Weapon;                 // set when weapon setupped
    public GameObject WeaponObj;          // set when weapon setupped
    public float WeaponCondition = 1f;    // set when weapon setupped
    public float WeaponSpeed = 1f;        // must be set at the beginning
    public GameObject DefaultWeapon;      // must be set at the beginning

    [Header("Debug")]
    public Transform target;
    NavMeshAgent agent;

    // Private
    float weaponCooldown = 0f;
    float touchDis = 3f;
    bool move = true;
    bool returningToSpawn = false;
    Vector3 spawnPos;
    Animator WeaponAnimator;


    public void SetupWeapon(Weapon _weapon, Mesh _mesh, float _weaponCondition)
    {
        // Clear old weapon
        Destroy(WeaponObj);
        WeaponAnimator = null;

        // Set stuff
        if (_weapon) Weapon = _weapon;
        WeaponCondition = _weaponCondition;

        // Turn the mesh into a gameobject (not implemented)
        //print("weapon added for " + name);
        WeaponObj = Instantiate(DefaultWeapon, transform);
        if (_mesh != null)
        {
            WeaponObj.GetComponent<MeshFilter>().mesh = _mesh;
            WeaponObj.transform.localScale = Vector3.one;
            WeaponObj.transform.localPosition = new Vector3((transform.localScale.x * 0.5f + _mesh.bounds.size.z * transform.localScale.x * 0.5f) * 0.5f, 0f, 0f);
            //print($"{name} | AI size add: {transform.localScale.x * 0.5f} / Mesh size add: {_mesh.bounds.size.z * transform.localScale.x * 0.5f}");
            WeaponObj.GetComponent<MeshCollider>().sharedMesh = _mesh;
        }
        else
        {
            WeaponObj.transform.localPosition = new Vector3(0.55f, 0f, 0f);
        }
        
        WeaponAnimator = WeaponObj.GetComponent<Animator>();
    }



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spawnPos = transform.position;
        SetupWeapon(Weapon, null, 1f);
    }

    void Update()
    {
        if (weaponCooldown > 0f) weaponCooldown -= Time.deltaTime;

        // Find target, if target null -> return
        target = GameController.FindClosest(transform.position, FindElf, FindEnemy, FindAnvil);
        if (!target)
        {
            agent.isStopped = true;
            return;
        } 
        touchDis = GameController.GetTouchDistance(transform, target);


        // Do this if target is valid
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z));

            // Try to Use Weapon if close enough
            if (WeaponObj && distance <= touchDis + Weapon.Range)
            {
                if (weaponCooldown <= 0f)
                {
                    Vector3 dir = (target.position - WeaponObj.transform.position).normalized;
                    bool weaponUsed = Weapon.TryToUseWeapon(WeaponObj.transform.position, dir, WeaponCondition, touchDis, target, transform, WeaponObj);
                    if (name == "Elf") print(weaponUsed);
                    move = !weaponUsed;
                    if (weaponUsed == true && WeaponAnimator != null) WeaponAnimator.Play("weapon_use");

                    weaponCooldown = 1 / WeaponSpeed;
                }
            }
            else move = true;

            // Move if it's enabled. it is only false when AI can use weapon (close enough and raycast hits target)
            if (move)
            {
                if (agent.isStopped) agent.isStopped = false;

                if (KeepCloseToSpawn && Vector3.Distance(transform.position, spawnPos) > 15 && Vector3.Distance(transform.position, target.position) > 15 && returningToSpawn == false)
                {
                    returningToSpawn = true;
                    
                }
                
                // Actual move
                if (!returningToSpawn)
                {
                    agent.SetDestination(target.position);
                }
                else
                {
                    if (Vector3.Distance(transform.position, spawnPos) < 10) returningToSpawn = false;
                    agent.SetDestination(spawnPos);
                }
            }
            else if (!agent.isStopped) agent.isStopped = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent != null)
        {
            Gift gift = collision.transform.parent.GetComponent<Gift>();
            if (gift != null)
            {
                SetupWeapon(gift.weapon, gift.model, gift.condition);
                Destroy(collision.transform.parent.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        GlobalStats.self.Tiles += Reward;

        GameController.Remove(gameObject);
    }
}
