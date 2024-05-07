using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : ProjectileAttackBehaviour
{

    ProjectileAttack pa;
    PlayerMovement pm;
    protected override void Start()
    {
        base.Start();
        pa = FindObjectOfType<ProjectileAttack>();
        pm = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject != null && pa != null)
            transform.position += direction * pa.speed * Time.deltaTime;
    }
}
