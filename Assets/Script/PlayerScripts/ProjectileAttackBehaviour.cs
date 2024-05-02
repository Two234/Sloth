using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script for all projectile behaviours [placed on prefab of weaponn]
/// </summary>
public class ProjectileAttackBehaviour : MonoBehaviour
{

    protected Vector3 direction;
    public float destroyAfterSeconds;
   
    protected virtual void Start()
    {
       Destroy(gameObject, destroyAfterSeconds); 
    }

    // Update is called once per frame
    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
    }
}
