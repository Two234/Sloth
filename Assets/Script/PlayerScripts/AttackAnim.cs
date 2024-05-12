using UnityEngine;

public class AttackAnim : MonoBehaviour
{
    public GameObject Attack;
    public void AttackFinished()
    {
        Attack.SetActive(false);
    }
}
