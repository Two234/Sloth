using System;
using System.Collections;
using System.Security.Cryptography;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UIElements;

public class Chasing : MonoBehaviour
{
    public float speed ;
    public Transform player, enemyEye;
    public bool viewBlock = false;
    private float ray, speedingLevel, EDF;
    public float speedingLevelIncreases = 1.2f;
    public float acceleration; 
    public int speedingLevels ;
    // Start is called before the first frame update
    void Awake()
    {

        foreach(Transform child in transform){
            if (child.name == "Field"){
                ray = child.lossyScale.x;
                break; 
            }
        }
        speedingLevel = ray / 2 / speedingLevels;
    }

    // Update is called once per frame
    void FixedUpdate(){
        EDF = Mathf.Sqrt(Mathf.Pow(player.position.x - transform.position.x,2) + Mathf.Pow( player.position.y - transform.position.y,2)); 
        if (viewBlock == false && EDF <= ray/2 ){
            if (EDF <= ray/2 - speedingLevel)
                StartCoroutine(speedTransition());
            else speedingLevel /= 2;
            GetComponent<Rigidbody2D>().velocity = (speedingLevel * speed * Time.deltaTime * (player.position - transform.position).normalized);
        }
        else GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    IEnumerator speedTransition(){
        float goal = speedingLevel * speedingLevelIncreases;
        while (speedingLevel < goal){
            speedingLevel += acceleration;
            yield return new WaitForSeconds(Time.deltaTime);
        }        
    }
}