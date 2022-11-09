using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeForce : MonoBehaviour , IDamagable
{
    public float timeValue = 60;
    public bool timerOn = false;
    //public Text timeText;
    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            DisplayTime(timeValue);
        }
        else
        {
            Debug.Log("Time up!");
            timeValue = 0;
            timerOn = false;
            gameOverText.SetActive(true);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        Debug.Log(minutes + ":" + seconds);

        //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Damage(int amount)
    {
        
    }

    public void AddHeatlh(int amount)
    {
        
    }
}
