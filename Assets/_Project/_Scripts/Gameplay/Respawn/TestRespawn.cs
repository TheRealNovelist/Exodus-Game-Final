using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is responsible for respawning the player
//UI fade screen is adapted from SpeedTutor
public class TestRespawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnYAxis;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] BoxCollider bCollider;
    [SerializeField] float respawnTime = 3f;

    [SerializeField] CanvasGroup deadScreenUI;
    [SerializeField] bool fadeIn = false;
    [SerializeField] bool fadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody>();
        mesh = this.GetComponent<MeshRenderer>();
        bCollider = this.GetComponent<BoxCollider>();
        deadScreenUI.alpha = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        Respawn();  //calls respawn if a collided with a trigger
    }

    public void fadeInUI()
    {
        fadeIn = true;
    }
    public void fadeOutUI()
    {
        fadeOut = true;
    }

    //Calls by OnTriggerEnter()
    //Calls by Update()
    //Calls IEnumerator RespawnDelay() to start couroutine
    void Respawn()
    {
        fadeInUI(); //call fadeOutUI() to set fade out to true
        StartCoroutine(RespawnDelay()); //start couroutine for a dynamic respawn mechanic
    }

    //disable mesh renderer
    //delay respawn on a timer
    //brings player back to respawn point
    //reenable mesh renderer
    //calls fadeOutUI()
    IEnumerator RespawnDelay()
    {
        mesh.enabled = false;   //turn off mesh renderer
        //bCollider.enabled = false;    //turn off box collider
        yield return new WaitForSeconds(respawnTime); //respawn delay
        transform.position = spawnPoint.position;   //brings player's position to spawnPoint's position
        mesh.enabled = true;    //turn on mesh renderer
        //bCollider.enabled = true; //turn on box collider
        fadeOutUI();    //call fadeOutUI() to set fade out to true
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -spawnYAxis)  //respawn on out of bounds (y axis)
        {
            Respawn();
        }

        //gradually fade in the ui by Time.deltaTime
        if (fadeIn)
        {
            if (deadScreenUI.alpha < 1) 
            {
                deadScreenUI.alpha += Time.deltaTime;
                if (deadScreenUI.alpha == 1)
                {
                    fadeIn = false;
                }
            }
        }

        //gradually fade out the ui by Time.deltaTime
        if (fadeOut)
        {
            if (deadScreenUI.alpha >= 0)
            {
                deadScreenUI.alpha -= Time.deltaTime;
                if (deadScreenUI.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }







        //movement codes below are for testing, will not be included in the final build
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerRB.AddForce(-5, 0, 0, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            playerRB.AddForce(5, 0, 0, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            playerRB.AddForce(0, 0, 5, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerRB.AddForce(0, 0, -5, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.AddForce(0, 10, 0, ForceMode.Impulse);
        }
    }
}
