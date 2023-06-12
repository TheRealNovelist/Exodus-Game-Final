using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementSound : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    private Vector3 lastPosition;
    private float timeCounter = 0f;
    public float delayTime = 0.1f;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= delayTime)
            {
                if (transform.position != lastPosition)
                {
                    audioManager.PlayOneShot("FootStep");
                    lastPosition = transform.position;
                }
                timeCounter = 0f;
            }
        }
        else
        {
            timeCounter = 0f;
        }
    }
}
