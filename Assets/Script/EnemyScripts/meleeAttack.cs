using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    public float range = 1f;
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float EDF = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.position.x, 2) + Mathf.Pow(transform.position.y - Player.position.y, 2));
        if (EDF <= range){
            Animate();
            RaycastHit2D melee = Physics2D.Raycast(transform.position, Player.position - transform.position, range, LayerMask.GetMask(new string[]{"Player", "Obstacles"}));
            if (melee){
                if (melee.transform.tag == "Player"){
                    Debug.Log("Melee Hit");
                }
            }
        }
        else GetComponent<Animator>().SetBool("ReadyToAttack", false);
    }
    void Animate(){
        GetComponent<Animator>().SetBool("ReadyToAttack", true);
    }
}