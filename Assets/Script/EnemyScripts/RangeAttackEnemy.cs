using System.Collections;
using UnityEngine;

public class RangeAttackEnemy : MonoBehaviour
{
    Transform player;
    public GameObject bullet;
    float angle;
    private float shotCoolDown;
    public  float startShotCoolDown;
    Transform sight;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Chasing>().player;
        sight = GetComponent<Chasing>().EnemySight;
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null){
            bool isAttacking = GetComponent<Chasing>().isAttacking ;
            bool isRanged = GetComponent<Chasing>().isRanged; 
            bool PlayerDetected = sight.GetComponent<FieldofView>().PlayerDetected;
            Vector2 direction = new Vector2(player.position.x - transform.position.x ,player.position.y-transform.position.y);
            //direction that the enemy will look at 
            if (shotCoolDown <= 0 && PlayerDetected == true && isAttacking == false && isRanged == true) //checks after a certain amount of time that a instance of a bullet is created where teh enemy is 
            {
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                StartCoroutine(AttackDelay());
                shotCoolDown = startShotCoolDown;
                GetComponent<Animator>().SetTrigger("Ranged");
                sight.GetComponent<FieldofView>().Animate();
            }
            else { shotCoolDown-=Time.deltaTime; }
        }
    }
    
    IEnumerator AttackDelay(){
        float time = 0;
        float length = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        while (time <= length){
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        
        if (newBullet.GetComponent<Bullet>() == null){
        
            newBullet.AddComponent<Bullet>().speed = 0;
            newBullet.GetComponent<Bullet>().timer = 5;
            newBullet.GetComponent<Chasing>().player = GetComponent<Chasing>().player;
            newBullet.GetComponent<Chasing>().EnemySight.GetComponent<FieldofView>().startingAngle = GetComponent<Chasing>().EnemySight.eulerAngles.z;

            newBullet.layer = LayerMask.NameToLayer("Summoned Watcher");

        }
        else
            newBullet.transform.rotation = Quaternion.Euler(0, 0, angle- 90);
    }
}
