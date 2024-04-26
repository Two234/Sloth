using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAiEnemy : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed;
    public Transform enemyEye;
    public bool viewBlock = false;
    private float ray, speedingLevel;
    public float speedingLevelIncreases = 1.2f;
    public float acceleration;
    public int speedingLevels;
    public float raycastDistance = 2f; // Distance of the raycast

    private Rigidbody2D rb;

    void Awake()
    {
        // Cache the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        foreach (Transform child in transform)
        {
            if (child.name == "Field")
            {
                ray = child.lossyScale.x;
                break;
            }
        }
        speedingLevel = ray / 2 / speedingLevels;
    }

    void FixedUpdate()
    {
        float EDF = Mathf.Sqrt(Mathf.Pow(player.position.x - transform.position.x, 2) + Mathf.Pow(player.position.y - transform.position.y, 2));

        if (!viewBlock && EDF <= ray / 2)
        {
            if (EDF <= ray / 2 - speedingLevel)
            {
                StartCoroutine(speedTransition());
            }
            else
            {
                speedingLevel /= 2;
            }

            // Smoothly move towards the player using lerping
            Vector2 targetPosition = Vector2.Lerp(transform.position, player.position, Time.deltaTime * speed);
            rb.MovePosition(targetPosition);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        // Perform raycast around the enemy
        RaycastHit2D hit = Physics2D.Raycast(enemyEye.position, Vector2.right, raycastDistance);

        // Check if the raycast hits an object with the tag "object"
        if (hit.collider != null && hit.collider.CompareTag("object"))
        {
            // Move around the object
            Vector2 direction = (hit.point - (Vector2)transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }

    IEnumerator speedTransition()
    {
        float goal = speedingLevel * speedingLevelIncreases;
        while (speedingLevel < goal)
        {
            speedingLevel += acceleration;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    bool CanSeePlayer()
    {
        // Perform a raycast from the enemy towards the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, raycastDistance);

        // Check if the raycast hits the player
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    void Update()
    {
        // Check if player is not null and the enemy can see the player
        if (player != null && CanSeePlayer())
        {
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
