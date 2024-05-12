using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Attack;

    public GameObject HitBox;

    public Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        bool isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("playerattack");

        if (Input.GetMouseButtonDown(0) && isAttacking == false)
        {
            Attack.SetActive(true);
            animator.SetTrigger("Attack");
        }
    }
}
