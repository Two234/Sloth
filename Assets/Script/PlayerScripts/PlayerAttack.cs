using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Attack;
    public float slashSpeed = 20;
    public float attackPushForce = 0.1f;
    public float pushDistance = 2f;
    public float pushSpeed = 0.1f;
    public GameObject HitBox;
    public float meleeAttackRange = 2f;
    public Animator animator;
    [HideInInspector] public bool isAttacking;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("playerattack");
        if (Input.GetMouseButtonDown(0) && isAttacking == false)
        {
            Attack.SetActive(true);
            StartCoroutine(AttackField());
            animator.SetTrigger("Attack");
        }
    }
    public IEnumerator AttackField(){
        isAttacking = true;
        Vector2 direction = GetComponent<PlayerMovement>().direction;
        float playerAngle= (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 360) % 360;
        float arc = 90 / slashSpeed;
        float angle = 0;
        List<GameObject> enemies = new List<GameObject>();
        while (angle <= 90){
            float attackAngle;
            if (315f < playerAngle || playerAngle <= 45f)
                attackAngle = 315;
            else if (45f < playerAngle && playerAngle <= 135f)
                attackAngle = 45;
            else if(135f < playerAngle && playerAngle <= 225)
                attackAngle = 135;
            else attackAngle = 225;
            Vector2 AttackDirection = new Vector2(Mathf.Cos((attackAngle + angle) * Mathf.Deg2Rad), Mathf.Sin((attackAngle + angle) * Mathf.Deg2Rad));
            RaycastHit2D melee = Physics2D.Raycast(transform.position, AttackDirection, meleeAttackRange, layerMask);
            
            if(melee){
                if (melee.transform.tag == "Enemies" && enemies.Contains(melee.transform.gameObject) == false){ 
                    
                    melee.transform.GetComponent<HitBox>().hit = true;
                    StartCoroutine(AttackPush(melee, AttackDirection));
                    melee.transform.position = Vector3.Lerp(melee.transform.position, AttackDirection * pushDistance, attackPushForce);

                    enemies.Add(melee.transform.gameObject);
                }
            }
            angle += arc;
            yield return new WaitForSeconds(Time.deltaTime);            
        }
        isAttacking = false;
    }       
    IEnumerator AttackPush(RaycastHit2D melee, Vector3 direction){
        // melee.rigidbody.velocity = Vector2.zero;
        
        melee.transform.GetComponent<Chasing>().beingPushed = true;
        Vector3 initPos = melee.transform.position;
        float diff = abs(melee.transform.position - (initPos + direction * pushDistance)).magnitude; // distance = position - destination
        while (diff >= 1e-1){
            melee.transform.position = Vector2.MoveTowards(melee.transform.position, Vector2.Lerp(melee.transform.position, initPos + direction * pushDistance, attackPushForce), pushSpeed); 
            diff = abs(melee.transform.position - (initPos + direction * pushDistance)).magnitude;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        melee.transform.GetComponent<Chasing>().beingPushed = false;
        melee.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    Vector2 abs(Vector2 vector){
        return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
    }

}