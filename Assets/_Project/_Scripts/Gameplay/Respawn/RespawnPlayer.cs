using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

//This script is responsible for respawning the player
//UI fade screen is adapted from SpeedTutor

/// <summary>
/// UI fade screen is adapted from SpeedTutor
/// Call StartRespawn() when plaeyr dies to respawn player
/// </summary>
public class RespawnPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPoint;
    [SerializeField] CanvasGroup deadScreenUI;

    [Header("StartRespawn setting")]
    [SerializeField] float respawnTime = 3f;
    [SerializeField] float spawnYAxis =10;

    private bool fadeIn = false;
    private bool fadeOut = false;

    public static Action OnPlayerFinishedRespawn;
    public static Action OnPlayerStartRespawn;


    // Start is called before the first frame update
    void Start()
    {
        deadScreenUI.alpha = 0;

        OnPlayerFinishedRespawn += FinsihedRespawn;
        OnPlayerStartRespawn += StartRespawn;
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
    public void StartRespawn()
    {
        fadeInUI(); //call fadeOutUI() to set fade out to true
        StartCoroutine(RespawnDelay()); //start couroutine for a dynamic respawn mechanic
    }

    private void FinsihedRespawn()
    {
        player.transform.position = spawnPoint.position;
        
        // InverseTransformPoint equivalent:
        fadeOutUI();
    }

    //disable mesh renderer
    //delay respawn on a timer
    //brings player back to respawn point
    //reenable mesh renderer
    //calls fadeOutUI()
    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(respawnTime); //respawn delay
        OnPlayerFinishedRespawn?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (player.transform.position.y < -spawnYAxis)  //respawn on out of bounds (y axis)
        {
            StartRespawn();
        }*/

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
        
    }

    private void OnDisable()
    {
        OnPlayerFinishedRespawn -= FinsihedRespawn;
        OnPlayerStartRespawn -= StartRespawn;
    }
}
