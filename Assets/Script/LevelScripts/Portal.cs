using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class Portal : MonoBehaviour
{

    public string NextLevel;
    public GameObject boss;



    private bool playerInsideTrigger = false;
    [SerializeField]
    private float timer = 10f;
    [SerializeField]
    private float timeToStay = 20f; // Adjust this value to set the duration the player should stay

    // Start is called before the first frame update
    void Start()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            timer = 0f;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Increment timer if the player is still inside the trigger
            timer += Time.deltaTime;

            // Check if the player has stayed for the desired duration
            //TO DO: boss object should have some check or the boss gameobject should be deleted
            if (timer >= timeToStay && boss == null)
            {
                SceneManager.LoadScene(NextLevel);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = false;
            timer = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
