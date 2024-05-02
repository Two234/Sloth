using UnityEngine;

public class VisualizationSystem : MonoBehaviour
{
    public Transform player;
    public Chasing script;
    void Update()    
    {
        if (player != null)
            GetComponent<PolygonCollider2D>().points = new Vector2[]{
                GetComponent<PolygonCollider2D>().points[0],
                GetComponent<PolygonCollider2D>().points[1],
                new Vector2(-Mathf.Sqrt(Mathf.Pow(player.position.x - transform.parent.transform.position.x,2) + Mathf.Pow( player.position.y - transform.parent.transform.position.y,2)),0)
            };
    }
    void LateUpdate(){
        if (player != null){
            Vector3 directionToPlayer = transform.parent.transform.position - player.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Obstacles")
            script.viewBlock = true;            
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.tag == "Obstacles")
            script.viewBlock = false;
    }
}
