using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class FieldofView : MonoBehaviour
{
    public int ray = 50;
    public float FieldOfView = 90f, distance = 10f;
    float arc ;
    public float rotationSpeed = 0.5f; 
    bool isMoving = false;
    [HideInInspector] public bool PlayerDetected = false, PlayerWasDetected = false;
    [SerializeField] private Material material;
    public Transform player;
    [SerializeField] private LayerMask Player, Obstacles;
    public Color color;
    void Start(){
        GetComponent<MeshRenderer>().materials = new Material[1]{material}; 
    }
    void LateUpdate(){
        arc = FieldOfView / ray; 
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
        PlayerDetected = false;
        Vector2 prevertex = Vector2.zero;
        for (int i = 1 ; i <= ray ; i ++){
            // field of view is considered multiple small triangle to make the arc
            vertices[i] = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * distance, Mathf.Sin(angle * Mathf.Deg2Rad) * distance, 0);

            Vector3 direction = new Vector3(Mathf.Cos((transform.eulerAngles.z + angle) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + angle) * Mathf.Deg2Rad),0);
            // the fov detects collided object that only has the same obstacles layer 
            RaycastHit2D obstacle = Physics2D.Raycast(transform.position, direction, distance, Obstacles);
            
            if (obstacle){
                    vertices[i] = new Vector3(obstacle.point.x - transform.position.x, obstacle.point.y - transform.position.y);
                    vertices[i] = Quaternion.Euler(0, 0, -transform.eulerAngles.z) * vertices[i];
            }
            RaycastHit2D player = Physics2D.Raycast(transform.position, direction, Mathf.Sqrt(Mathf.Pow(vertices[i].x, 2) + Mathf.Pow(vertices[i].y, 2)), Player);
            if (player){
                    PlayerDetected = true;
                    PlayerWasDetected = true;
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
        else if(PlayerDetected == true){
            transform.eulerAngles = new Vector3(0 , 0 , Mathf.Ceil(FieldOfView / 2 + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        }
    }
    public IEnumerator LookAround(){
        isMoving = true;
        if (PlayerWasDetected == true){
            PlayerWasDetected = false;
            transform.Rotate(Vector3.zero + transform.eulerAngles, 0f);
            yield return new WaitForSeconds(3f);
        }
        else{
            int i = UnityEngine.Random.Range(-180, 180);
            while (Mathf.Round(transform.rotation.eulerAngles.z) % 180 != Math.Abs(i)){
                transform.Rotate(Vector3.forward*i, rotationSpeed);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return new WaitForSeconds(3f);
        }
        isMoving = false;
    }
    public void Animate(){
        Vector2 sightDirection = new Vector2(Mathf.Cos((transform.eulerAngles.z - FieldOfView / 2 % 360) * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));

        transform.parent.GetComponent<Animator>().SetFloat("AnimMoveX", sightDirection.x);
        transform.parent.GetComponent<Animator>().SetFloat("AnimMoveY", sightDirection.y);
        transform.parent.GetComponent<Animator>().SetFloat("WalkX", sightDirection.x);
        transform.parent.GetComponent<Animator>().SetFloat("WalkY", sightDirection.y);
    }
}
