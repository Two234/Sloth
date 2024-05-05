using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rb;
    public float damage;
    public Animator animator;
    public GameObject camera;
    private Vector3 dir = Vector3.zero;
    private Vector3 mousePosition;
    public Vector2 direction;
    private Vector3 input;
    void Awake()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition + transform.position;
    }
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
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = mousePosition - transform.position;

        dir = Vector3.zero;

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Input detection
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            dir += Vector3.up;
            Input.GetAxisRaw("Vertical");

        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
           
        {
            dir += Vector3.down;
            Input.GetAxisRaw("Vertical");


        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            dir += Vector3.left;
            Input.GetAxisRaw("Horizontal");


        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            dir += Vector3.right;
            Input.GetAxisRaw("Horizontal");


        }

        Move();
        Animate(direction);
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

   
    void Animate(Vector3 direction)
    {
        animator.SetFloat("AnimMoveX", direction.x);
        animator.SetFloat("AnimMoveY", direction.y);
        animator.SetFloat("WalkX", direction.x);
        animator.SetFloat("WalkY", direction.y);
        animator.SetFloat("MoveMagnitude", rb.velocity.magnitude);
        

    }
}