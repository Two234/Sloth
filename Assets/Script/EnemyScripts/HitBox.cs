using System.Collections;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Transform healthBar;
    Transform sight;
    public float hpAmount, speed;
    float reducedHP, healthPosition;
    public bool isDead = true;
    float time = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        reducedHP = healthBar.lossyScale.y / hpAmount;
        healthPosition = healthBar.lossyScale.y;
    }
    void Start(){
        sight = GetComponent<Chasing>().EnemySight;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar.lossyScale.y <= 0f){
            bool isPlaying = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Death");
            if (isPlaying == false){
                GetComponent<Animator>().SetBool("Dead", true);
                sight.GetComponent<FieldofView>().Animate();   
            }
            if (time >= GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length)
                Destroy(gameObject);
            time += Time.deltaTime;

            
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Attack" && healthPosition > 0f){
            StartCoroutine(reductionTransition()); 
        }
    }
    IEnumerator reductionTransition(){
        healthPosition = healthBar.lossyScale.y - reducedHP;
        while (healthBar.lossyScale.y > healthPosition && (healthPosition > 0 || healthBar.lossyScale.y > 0)){
            healthBar.localScale -= Vector3.up * speed;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
