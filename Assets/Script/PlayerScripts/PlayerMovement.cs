using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rb;
    public float damage;
    public Animator animator;

    private Vector3 dir = Vector3.zero;
    private Vector3 input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Move()
    {
        rb.velocity = dir.normalized * Speed;
    }

    void Update()
    {
        dir = Vector3.zero;
        float AnimMoveX = input.x;
        float AnimMoveY = input.y;


        // Input detection
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            dir += Vector3.up;

        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            dir += Vector3.down;
            
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            dir += Vector3.left;
            
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            dir += Vector3.right;
            
        }

        Move();

    }

   
    void Animate()
    {
        animator.SetFloat("AnimMoveX", input.x);
        animator.SetFloat("AnimMoveY", input.y);

    }
}