using UnityEngine;

public class Chasing : MonoBehaviour
{
    public float speed ;
    public Transform player;
    public bool CheckIfRanged = false;
    float sightDistance;
    float speedingLevel;
    [HideInInspector] public bool isRanged;
    float speeding;
    public float acceleration;
    public int speedingLevels, speedingMaxLevel;
    float EDF;
    [HideInInspector] public Transform EnemySight;
    [HideInInspector] public bool isAttacking = false;
    void Awake(){
        foreach(Transform trans in transform)
            if (trans.name == "Sight")
                EnemySight = trans;
        sightDistance = EnemySight.GetComponent<FieldofView>().distance;
        speedingLevel = sightDistance / speedingLevels;
        speeding = speedingLevel;
    }
    void Start(){
        if (CheckIfRanged == true){
            int i = Random.Range(1,3);
            isRanged = i == 1;
        }
    }
    // Update is called once per frame
    void FixedUpdate(){
        if (player != null ){
            EDF = Mathf.Sqrt(Mathf.Pow(player.position.x - transform.position.x,2) + Mathf.Pow( player.position.y - transform.position.y,2)); 

            bool playerDetected = EnemySight.GetComponent<FieldofView>().PlayerDetected;
            isAttacking = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Melee Attack") || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Ranged Attack");
            if(playerDetected == true && isAttacking == false && (isRanged == false || EDF >= speeding * speedingMaxLevel)) {
                if (EDF >= speeding * speedingMaxLevel){
                    speedingLevel = speedingLevels / (EDF * speeding);
                }
                else speedingLevel = speedingLevels / speedingMaxLevel;
                GetComponent<Rigidbody2D>().velocity = (player.position - transform.position).normalized * speed * speedingLevel * Time.deltaTime;
            }
            else {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                speedingLevel = 0;
                if (CheckIfRanged == true){
                    int i = Random.Range(1,3);
                    isRanged = i == 1;
                }
            }
        }
        if (GetComponent<Animator>() != null){
            EnemySight.GetComponent<FieldofView>().Animate();
            GetComponent<Animator>().SetFloat("MoveMagnitude", GetComponent<Rigidbody2D>().velocity.magnitude);
        }
            
    }
}