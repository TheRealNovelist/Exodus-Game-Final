using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public SoundPlayExam soundTest;
    
    [SerializeField] private KeyCode shootKey = KeyCode.D;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(shootKey))
        {
            soundTest.PlayRunningSound();
            Debug.Log("Here");
        }
    }
}
