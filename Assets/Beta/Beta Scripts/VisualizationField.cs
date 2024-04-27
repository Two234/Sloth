using System;
using System.Collections;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.Rendering;

public class VisualizationField : MonoBehaviour
{
    public int ray = 50;
    public float angle = 90f, arc = 0.005f, distance = 10f;
    public float rotationSpeed;
    bool isMoving = false, viewBlock = false;
    Transform obstacle;
    // Start is called before the first frame update
    
    void Start()
    {
    }
    // Update is called once per frame
    void LateUpdate()
    {
        float alpha = angle * Mathf.Deg2Rad;
        // Create field of view using triangles with Mesh

        Mesh mesh = new Mesh();

        int[] triangles = new int[(ray-1) * 3];
        
        Vector2[] uv = new Vector2[ray + 1];
        
        Vector3[] vertices = new Vector3[ray + 1];
        vertices[0] = Vector3.zero;
        vertices[1] = new Vector3(distance * Mathf.Cos(alpha), distance * -Mathf.Sin(alpha));

        alpha += arc;
        
        Vector2[] colliders = new Vector2[ray + 1];
        colliders[0] = Vector2.zero;
        colliders[1] = new Vector2(vertices[1].x, vertices[1].y);

        int trianglesIndex = 0;
        for (int i = 2; i <= ray ; i ++){
            vertices[i] = new Vector3(
                Mathf.Cos(alpha) * distance,
                -Mathf.Sin(alpha) * distance
            );

            colliders[i] = vertices[i];
            if (viewBlock){
                Vector3 direction = obstacle.position - transform.parent.transform.position;
                RaycastHit2D raycast = Physics2D.Raycast(transform.parent.position, direction, Mathf.Sqrt(Mathf.Pow(vertices[i].x, 2) + Mathf.Pow(vertices[i].y, 2)));
                if (raycast != null)
                    vertices[i] = raycast.point;
            }
            triangles[trianglesIndex] = 0;
            triangles[trianglesIndex + 1] = i-1;
            triangles[trianglesIndex + 2] = i;
            trianglesIndex += 3;            
            
            alpha += arc;
        }
        
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<PolygonCollider2D>().points = colliders;

    }
    void Update(){
        if (isMoving == false){
            StartCoroutine(LookAround());
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag== "Obstacles"){
            viewBlock = true;
            obstacle = col.transform;
        }
        else viewBlock = false;
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.tag == "Obstacles"){
            viewBlock = false;
            obstacle = null;
        }
    }
    public IEnumerator LookAround(){
        isMoving = true;
        int i = UnityEngine.Random.Range(-180, 180);
        while (Math.Round(transform.rotation.eulerAngles.z) % 180 != Math.Abs(i)){
            transform.Rotate(Vector3.forward*i, rotationSpeed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(3f);
        isMoving = false;
    }
}
