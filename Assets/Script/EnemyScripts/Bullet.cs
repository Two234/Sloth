using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed, timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(deathAfter(timer));
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up* speed*Time.deltaTime);
        
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Obstacles") Destroy(this);
        

        
    }
    public IEnumerator deathAfter(float timer){
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
