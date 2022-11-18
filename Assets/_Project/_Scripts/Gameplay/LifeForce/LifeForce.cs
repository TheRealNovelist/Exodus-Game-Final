using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeForce : MonoBehaviour , IDamageable, IHeal
{
    [SerializeField] private float timeValue = 60;
    private bool timerOn = false;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject gameOverText;

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
            timeValue = 0;
            timerOn = false;
            gameOverText.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Damage(float amount)
    {
        timeValue -= amount;
    }

    public void AddHealth(float amount)
    {
        timeValue += amount;
    }

    //TESTING
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage(10);
        }
    }
}
