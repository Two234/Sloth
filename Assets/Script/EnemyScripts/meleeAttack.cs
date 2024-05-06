using System.Collections;
using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    Transform player, EnemySight;
    public float range = 1f;
    public float delay = 1f ; // will affect the hit delay before the dagger comes out
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Chasing>().player;
        EnemySight = GetComponent<Chasing>().EnemySight;
    }

    // Update is called once per frame
    void Update()
    {
        float EDF = Mathf.Sqrt(Mathf.Pow(transform.position.x - player.position.x, 2) + Mathf.Pow(transform.position.y - player.position.y, 2));

        bool isAttacking = GetComponent<Chasing>().isAttacking;
        
        if (Mathf.Ceil(EDF) < range && isAttacking == false){
            EnemySight.GetComponent<FieldofView>().PlayerDetected = true;
            EnemySight.GetComponent<FieldofView>().PlayerWasDetected = true;

            GetComponent<Animator>().SetTrigger("Attack");
            EnemySight.GetComponent<FieldofView>().Animate();

            StartCoroutine(MeleeAttackDelay());    
        }
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