using System;
using System.Collections;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class VisualizationField : MonoBehaviour
{
    public int ray = 50;
    public float viewDistance = 10f, viewArc, viewSharpness = 1f;
    public float rotationSpeed;
    bool isMoving = false, viewBlock = false;
    Transform obstacle;
    // Start is called before the first frame update
    
    void Start()
    {
        ray += 1 - (ray % 2) * (1);
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Create field of view using triangles with Mesh

        Mesh mesh = new Mesh();

        int[] triangles = new int[(ray-1) * 3];
        
        Vector3[] vertices = new Vector3[ray + 1];
        vertices[0] = Vector3.zero;
        
        Vector2[] colliders = new Vector2[ray + 1] ;
        colliders[0] = Vector2.zero;

        int mid = ray/2 + 1;
        vertices[mid] = new Vector3(viewDistance, 0 ,0);
        colliders[mid] = new Vector2(viewDistance, 0);
        
        int trianglesIndex = (ray-1) * 3/2;
        float derivation = 0 ;
        for (int i = mid ; i > 1 ; i -- ){
            Vector3 vertice1 = new Vector3(vertices[i].x - derivation, vertices[i].y + viewSharpness, 0);
            Vector3 vertice2 = new Vector3(vertices[ray-i + 1].x - derivation, vertices[ray-i + 1].y - viewSharpness , 0);
            vertices[i - 1] = new Vector2(vertice1.x, vertice1.y);
            vertices[ray - i + 2] = new Vector2(vertice2.x, vertice2.y);
            if (viewBlock == true){
                vertices[i-1] = Vector2.zero;
            }

            colliders[i -1] = vertice1;
            colliders[ray - i + 2] = vertice2;
            derivation += viewArc;

            triangles[trianglesIndex] = 0;
            triangles[trianglesIndex - 1] = i;
            triangles[trianglesIndex - 2] = i - 1;
            triangles[(ray-1) * 3 - trianglesIndex] = 0;
            triangles[(ray-1) * 3 - trianglesIndex + 1] = ray - i + 1;
            triangles[(ray-1) * 3 - trianglesIndex + 2] = ray - i + 2;
            trianglesIndex -= 3;
        }
        
        
        mesh.vertices = vertices;
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
