using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Attack;
    public float slashSpeed = 20;
    public GameObject HitBox;
    public float meleeAttackRange = 2f;
    public Animator animator;
    public float delay = 0.3f;
    [HideInInspector] public bool isAttacking;
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
        Vector2 direction = GetComponent<PlayerMovement>().direction;
        float playerAngle= (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 360) % 360;
        float arc = 90 / slashSpeed;
        float angle = 0;
        while (angle <= 90){
            float attackAngle;
            if (315f < playerAngle || playerAngle <= 45f)
                attackAngle = 315;
            else if (45f < playerAngle && playerAngle <= 135f)
                attackAngle = 45;
            else if(135f < playerAngle && playerAngle <= 225)
                attackAngle = 135;
            else attackAngle = 225;

            Debug.DrawRay(
                transform.position,
                new Vector3(Mathf.Cos((attackAngle + angle) * Mathf.Deg2Rad) * meleeAttackRange, Mathf.Sin((attackAngle + angle) * Mathf.Deg2Rad) * meleeAttackRange, 0),
                Color.blue
            );
            angle += arc;
            yield return new WaitForSeconds(Time.deltaTime);
            
        }
    }
}
