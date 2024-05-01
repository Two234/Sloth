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
    private float speedingLevel;
    public float speeding = 1.2f;
    public float acceleration;
    public int speedingLevels;
    public int speedingMaxLevel;

    // Update is called once per frame
    void FixedUpdate(){
        
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
        GetComponent<Animator>().SetFloat("MoveMagnitude", GetComponent<Rigidbody2D>().velocity.magnitude);
    }
}