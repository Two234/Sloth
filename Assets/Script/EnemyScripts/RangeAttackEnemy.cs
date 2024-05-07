using UnityEngine;

public class RangeAttackEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;
    private float shotCoolDown;
    public  float startShotCoolDown;
    Transform sight;
    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<Chasing>().player;
        shotCoolDown = startShotCoolDown; 
        foreach(Transform trans in transform) if (trans.name == "Sight") sight = trans;
        
    }

    // Update is called once per frame
    void Update()
    {
        //direction that the enemy will look at 
        if (player != null){
            Vector2 direction= new Vector2(player.position.x - transform.position.x ,player.position.y-transform.position.y);
            bool isAttacking = GetComponent<Chasing>().isAttacking ;
            bool isRanged = GetComponent<Chasing>().isRanged; 
            Debug.Log(sight.GetComponent<FieldofView>().PlayerDetected);
            if (shotCoolDown <= 0 && sight.GetComponent<FieldofView>().PlayerDetected == true && isAttacking == false && isRanged == true) //checks after a certain amount of time that a instance of a bullet is created where teh enemy is 
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.transform.rotation = Quaternion.Euler(0, 0, angle- 90);
                shotCoolDown = startShotCoolDown;
                GetComponent<Animator>().SetTrigger("Ranged");
                sight.GetComponent<FieldofView>().Animate();
            }
            else { shotCoolDown-=Time.deltaTime; }
        }
    }
}
