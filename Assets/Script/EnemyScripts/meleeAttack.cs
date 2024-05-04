using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    Transform player, EnemySight;
    public float range = 1f;
    bool attackRange = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Chasing>().player;
        EnemySight = GetComponent<Chasing>().EnemySight;
    }

    // Update is called once per frame
    void Update()
    {
        bool isAttacking = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Melee Attack");
        if (attackRange && isAttacking == false){
            RaycastHit2D melee = Physics2D.Raycast(transform.position, player.position - transform.position, range, LayerMask.GetMask(new string[]{"Player", "Obstacles"}));
            if (melee){
                if (melee.transform.tag == "Player"){
                    Debug.Log("Melee Hit");
                }
            }
            GetComponent<Animator>().SetTrigger("Attack");
            EnemySight.GetComponent<FieldofView>().Animate();
        }
    }
    void OnCollisionEnter2D(Collision2D col){
        if (col.transform.tag == "Player")
            attackRange = true;
    }
    void OnCollisionExit2D(Collision2D col){
        if (col.transform.tag == "Player")
            attackRange = false;
    }
}