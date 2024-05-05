using System.Collections;
using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    Transform player, EnemySight;
    public float range = 1f;
    public float delay = 1f ; // will affect the hit delay before the dagger comes out
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
            GetComponent<Animator>().SetTrigger("Attack");
            EnemySight.GetComponent<FieldofView>().Animate();

            StartCoroutine(MeleeAttackDelay());    
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
    IEnumerator MeleeAttackDelay(){
        RaycastHit2D melee = Physics2D.Raycast(transform.position, player.position - transform.position, range, LayerMask.GetMask(new string[]{"Player", "Obstacles"}));

        float time = 0, animLength = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        while (time < animLength / delay){
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        if (melee.transform.name == "Player")
            Debug.Log("Melee hit");
        
        
    }
}