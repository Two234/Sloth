using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileAttack : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        GameObject playerProjectile = Instantiate(prefab);
        playerProjectile.transform.position = transform.position;
        playerProjectile.GetComponent<ProjectileAttackBehaviour>().DirectionChecker(pm.direction); 
    }

   
}

