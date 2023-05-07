using System.Collections;
using UnityEngine;

//This script is responsible for respawning the player
//UI fade screen is adapted from SpeedTutor

/// <summary>
/// UI fade screen is adapted from SpeedTutor
/// Call Respawn() when plaeyr dies to respawn player
/// </summary>
public class RespawnPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPoint;
    [SerializeField] CanvasGroup deadScreenUI;

    [Header("Respawn setting")]
    [SerializeField] float respawnTime = 3f;
    [SerializeField] float spawnYAxis = 10;

    private bool fadeIn = false;
    private bool fadeOut = false;


    // Start is called before the first frame update
    void Start()
    {
        deadScreenUI.alpha = 0;
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
    public void Respawn()
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
        yield return new WaitForSeconds(respawnTime); //respawn delay
        player.transform.position = spawnPoint.position;   //brings player's position to spawnPoint's position
        fadeOutUI();    //call fadeOutUI() to set fade out to true
    }

    // Update is called once per frame
    void Update()
    {
        ForceDie();
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
        
        void ForceDie()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Respawn();
            }
        }
    }
}
