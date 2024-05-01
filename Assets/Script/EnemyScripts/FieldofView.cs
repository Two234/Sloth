using System;
using System.Collections;
using System.Security.Permissions;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Experimental.Rendering;

public class FieldofView : MonoBehaviour
{
    public int ray = 50;
    public float FieldOfView = 90f, distance = 10f;
    float arc ;
    public float rotationSpeed; 
    bool isMoving = false;
    [SerializeField] private Material material;
    public bool PlayerDetected = false;
    [SerializeField] private LayerMask playerLayer, obstaclesLayer;
    public Transform player;
    public Color color;
    void Start(){
        arc = FieldOfView / ray; 
        GetComponent<MeshRenderer>().materials = new Material[1]{material}; 
    }
    void LateUpdate(){
        // Making the field of view
        float angle = 0;
        Mesh mesh = new Mesh();

        int[] triangles = new int[(ray - 1) * 3];

        Vector3[] vertices = new Vector3[ray + 1];
        vertices[0] = Vector3.zero;
        
        Color[] colors = new Color[ray + 1];
        colors[0] = color;

        angle -= arc;
        int trianglesIndex = 0;
        for (int i = 1 ; i <= ray ; i ++){
            // field of view is considered multiple small triangle to make the arc
            vertices[i] = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * distance, Mathf.Sin(angle * Mathf.Deg2Rad) * distance, 0);

            Vector3 direction = new Vector3(Mathf.Cos((transform.eulerAngles.z + angle) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + angle) * Mathf.Deg2Rad),0);
            // the fov detects collided object that only has the same obstacles layer 
            RaycastHit2D obsRaycast = Physics2D.Raycast(transform.position, direction, distance, obstaclesLayer);
            RaycastHit2D plRaycast = Physics2D.Raycast(transform.position, direction, distance, playerLayer);
            if (plRaycast) PlayerDetected = true;
            if (obsRaycast){
                vertices[i] = new Vector3(obsRaycast.point.x - transform.position.x, obsRaycast.point.y - transform.position.y);
                vertices[i] = Quaternion.Euler(0, 0, -transform.eulerAngles.z) * vertices[i];
            }
            if (i > 1){
                triangles[trianglesIndex] = 0;
                triangles[trianglesIndex + 1] = i - 1;
                triangles[trianglesIndex + 2] = i;
                trianglesIndex += 3;
            }
            angle -= arc;
            colors[i] = color;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000f);
        mesh.colors = colors;

        GetComponent<MeshFilter>().mesh = mesh;
        
    }
    void Update(){
        // the funcitonality of the FOV
        Vector2 direction = player.position - transform.position;
        if (isMoving == false && PlayerDetected == false){
            StartCoroutine(LookAround());
        }
        else{
            transform.eulerAngles = new Vector3(0 , 0 , FieldOfView / 2 + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
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
