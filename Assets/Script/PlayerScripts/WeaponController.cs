using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Weapon Controller
/// </summary>
public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject prefab;
    public float damage;
    public float speed;
    public float cooldownDuration;
    float currentCooldown;
    public int pierce;

    protected PlayerMovement pm;
    protected virtual void Start()
    {
        currentCooldown = cooldownDuration; //At start, set the current cooldown to be the cooldown duration
        pm = FindAnyObjectByType<PlayerMovement>();
        
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f) 
        {
            if(Input.GetMouseButtonDown(0))
            Attack();
        }
    }   

        protected virtual void Attack()
        {
            currentCooldown = cooldownDuration;
        }
    
}
