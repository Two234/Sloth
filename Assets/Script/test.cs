using System.Collections;
using UnityEngine;

public class test : MonoBehaviour
{

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hellow?");
        StartCoroutine(chasing());        
    }
    IEnumerator chasing(){
        float differ = abs(transform.position - target.position).magnitude;
        while (differ >= 1e-2){
            transform.position = Vector2.Lerp(transform.position, target.position, 0.1f);
            Debug.Log("marching");
            yield return null;
        }
    }
    Vector2 abs(Vector2 vector){
        return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
    }
}
