using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;
    private float shotCoolDown;
    private  float startShotCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        shotCoolDown = startShotCoolDown; 
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //direction that the enemy will look at 
        Vector2 direction= new(player.position.x - transform.position.x ,player.position.y-transform.position.y);
        transform.up = direction; 

        if (shotCoolDown <= 0) //checks after a certain amount of time that a instance of a bullet is created where teh enemy is 
        {
            Instantiate(bullet, transform.position, transform.rotation);
            shotCoolDown = startShotCoolDown;


        }
        else { shotCoolDown-=Time.deltaTime; }
    }
}
