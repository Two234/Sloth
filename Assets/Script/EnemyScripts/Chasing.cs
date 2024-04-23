using System;
using System.Security.Cryptography;
using UnityEngine;

public class Chasing : MonoBehaviour
{
    public float speed ;
    public Transform player, enemyEye;
    float coef = 0;
    public bool viewBlock = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (viewBlock == false)
            GetComponent<Rigidbody2D>().velocity = (coef * speed * Time.deltaTime * (player.position - transform.position).normalized);
        else GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player") 
        {
            coef += 1;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.tag == "Player"){
            coef -= 1;
        }
    }
}
