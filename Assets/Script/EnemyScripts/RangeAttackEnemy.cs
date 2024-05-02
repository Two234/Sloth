using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;
    private float shotCoolDown;
    public  float startShotCoolDown;
    // Start is called before the first frame update
    void Start()
    {
        shotCoolDown = startShotCoolDown; 
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //direction that the enemy will look at 
        if (player != null){
            Vector2 direction= new (player.position.x - transform.position.x ,player.position.y-transform.position.y);

            if (shotCoolDown <= 0 && GetComponent<Chasing>().EnemySight.GetComponent<FieldofView>().PlayerDetected == true) //checks after a certain amount of time that a instance of a bullet is created where teh enemy is 
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.transform.rotation = Quaternion.Euler(0, 0, angle- 90);
                shotCoolDown = startShotCoolDown;


            }
            else { shotCoolDown-=Time.deltaTime; }
        }
    }
}
