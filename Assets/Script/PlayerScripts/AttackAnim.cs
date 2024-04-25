using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnim : MonoBehaviour
{
    public GameObject Attack;
    public void AttackFinished()
    {
        Attack.SetActive(false);
    }
}
