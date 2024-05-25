using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackPushForce = 0.1f;
    public float pushDistance = 2f;
    public float pushSpeed = 0.1f;
    public float meleeAttackRange = 2f;
    public Animator animator;
    [HideInInspector] public bool isAttacking = false;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Awake(){
        animator = GetComponent<Animator>(); 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAttacking == false)
        {
            StartCoroutine(AttackField());
            animator.SetTrigger("Attack");
        }
    }
    public IEnumerator AttackField(){
        isAttacking = true;
        Vector2 direction = GetComponent<PlayerMovement>().direction;
        float playerAngle= (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 360) % 360;

        float angle = 0;
        List<GameObject> enemies = new List<GameObject>();
        while (angle <= 90){
            
            float arc = 90 * Time.deltaTime * animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; // adjust the ray to the exact animation time like swiping
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
            
            Debug.DrawRay(transform.position, AttackDirection * meleeAttackRange, Color.black); // for debug purpose
            if(melee){
                if (melee.transform.tag == "Enemies" && enemies.Contains(melee.transform.gameObject) == false){ 

                    melee.transform.GetComponent<HitBox>().hit = true;
                    StartCoroutine(AttackPush(melee, AttackDirection, pushDistance));
                    melee.transform.position = Vector3.Lerp(melee.transform.position, AttackDirection * pushDistance, attackPushForce);

                    enemies.Add(melee.transform.gameObject);
                }
            }
            angle += arc;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        isAttacking = false;
    }       
    IEnumerator AttackPush(RaycastHit2D melee, Vector3 direction, float distance){
        
        melee.transform.GetComponent<Chasing>().beingPushed = true;
        Vector3 initPos = melee.transform.position;
        float diff = abs(melee.transform.position - (initPos + direction * distance)).magnitude; // distance = position - destination

        // raycast to define that the position destinated is straight reachable
        int layer = LayerMask.NameToLayer("Obstacles");
        RaycastHit2D obstacle = Physics2D.Raycast(melee.transform.position, direction, distance, layer);

        // to edit the distance that of the reachable place and avoiding the obstacle
        distance = obstacle.point.magnitude;


        while (diff >= 0.5f){
            melee.transform.position = Vector2.MoveTowards(melee.transform.position, Vector2.Lerp(melee.transform.position, initPos + direction * distance, attackPushForce), pushSpeed); 
            diff = abs(melee.transform.position - (initPos + direction * distance)).magnitude;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        melee.transform.GetComponent<Chasing>().beingPushed = false;
        melee.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    Vector2 abs(Vector2 vector){
        return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
    }

}