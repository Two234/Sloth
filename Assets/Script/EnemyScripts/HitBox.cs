using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Transform healthBar;
    public float hpAmount, speed;
    float reducedHP, healthPosition;
    public bool isDead = true;
    // Start is called before the first frame update
    void Awake()
    {
        reducedHP = healthBar.lossyScale.y / hpAmount;
        healthPosition = healthBar.lossyScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar.lossyScale.y <= 0f){
            isDead = true;
            
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
