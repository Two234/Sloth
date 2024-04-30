using System;
using System.Collections;
using System.Security.Cryptography;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Chasing : MonoBehaviour
{
    public float speed ;
    public Transform player, enemyEye;
    public bool viewBlock = false, targetLock = false, CheckIfRanged = false;
    float EnemyAttackDistance;
    private float ray, speedingLevel, EDF;
    public float speeding = 1.2f;
    public float acceleration;
    public int speedingLevels;
    public int speedingMaxLevel;
    // Start is called before the first frame update
    void Awake()
    {

        foreach(Transform child in transform){
            if (child.name == "Field"){
                ray = child.lossyScale.x;
                break; 
            }
        }
        EnemyAttackDistance = ray / 4;
        speedingLevel = ray / 2 / speedingLevels;
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (player != null){
            EDF = Mathf.Sqrt(Mathf.Pow(player.position.x - transform.position.x,2) + Mathf.Pow( player.position.y - transform.position.y,2)); 
            if (viewBlock == false && EDF <= ray/2 && (CheckIfRanged == false ||  (EDF > EnemyAttackDistance && CheckIfRanged == true))){
                if (EDF <= ray/2 - speedingLevel && speedingLevel < (ray/2/speedingLevels) * speedingMaxLevel)
                    StartCoroutine(speedTransition());
                else speedingLevel /= 2;
                GetComponent<Rigidbody2D>().velocity = (speedingLevel * speed * Time.deltaTime * (player.position - transform.position).normalized);
                targetLock = true;
            }
            else{
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                targetLock = viewBlock == false && EDF <= ray/2;
            }
        }
        if (GetComponent<Animator>() != null)
            Animate();
    }
    IEnumerator speedTransition(){
        float goal = speedingLevel * speeding;
        while (speedingLevel < goal){
            speedingLevel += acceleration;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    void Animate(){
        GetComponent<Animator>().SetFloat("AnimMoveX", GetComponent<Rigidbody2D>().velocity.x);
        GetComponent<Animator>().SetFloat("AnimMoveY", GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Animator>().SetFloat("WalkX", GetComponent<Rigidbody2D>().velocity.x);
        GetComponent<Animator>().SetFloat("WalkY", GetComponent<Rigidbody2D>().velocity.y);
    }
}