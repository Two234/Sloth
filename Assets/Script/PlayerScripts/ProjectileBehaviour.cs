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
            transform.position += direction * pa.speed * Time.deltaTime;
    }
}
