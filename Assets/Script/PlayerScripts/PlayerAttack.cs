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
        StartCoroutine(DelayAttack());
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && attackBlocked == false)
        {
            Attack.SetActive(true);
            StartCoroutine(DelayAttack());
            animator.SetTrigger("Attack");
        }
    }

    private IEnumerator DelayAttack()
    {
        attackBlocked = true;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        attackBlocked = false;
    }
}
