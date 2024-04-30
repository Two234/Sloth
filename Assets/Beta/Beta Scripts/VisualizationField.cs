using System;
using System.Collections;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Experimental.Rendering;

public class VisualizationField : MonoBehaviour
{
    public int ray = 50;
    public float FieldOfView = 90f, distance = 10f;
    float arc ;
    public float rotationSpeed;
    public int obstaclesLayer = 6; 
    bool isMoving = false;
    Transform obstacle;
    void Start(){
        arc = FieldOfView / ray; 
        obstaclesLayer = 1 << obstaclesLayer;
    }
    void LateUpdate(){
        float angle = FieldOfView;

        Mesh mesh = new Mesh();

        int[] triangles = new int[(ray - 1) * 3];

        Vector3[] vertices = new Vector3[ray + 2];
        vertices[0] = Vector3.zero;
        
        angle -= arc;
        int trianglesIndex = 0;
        for (int i = 1 ; i <= ray ; i ++){
            vertices[i] = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * distance, Mathf.Sin(angle * Mathf.Deg2Rad) * distance, 0);

            Vector3 direction = new Vector3(Mathf.Cos((transform.eulerAngles.z + angle) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + angle) * Mathf.Deg2Rad),0);
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction, distance, obstaclesLayer);
            if (raycast){
                vertices[i] = new Vector3(raycast.point.x - transform.position.x, raycast.point.y - transform.position.y);
                vertices[i] = Quaternion.Euler(0, 0, -transform.eulerAngles.z) * vertices[i];
            }
            if (i > 1){
                triangles[trianglesIndex] = 0;
                triangles[trianglesIndex + 1] = i - 1;
                triangles[trianglesIndex + 2] = i;
                trianglesIndex += 3;
            }
            angle -= arc;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000f);

        GetComponent<MeshFilter>().mesh = mesh;
        
    }
    void Update(){
        if (isMoving == false){
            StartCoroutine(LookAround());
        }
    }
    public IEnumerator LookAround(){
        isMoving = true;
        int i = UnityEngine.Random.Range(-180, 0);
        while (Math.Round(transform.rotation.eulerAngles.z) % 180 != Math.Abs(i)){
            transform.Rotate(Vector3.forward*i, rotationSpeed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(3f);
        isMoving = false;
    }
}
