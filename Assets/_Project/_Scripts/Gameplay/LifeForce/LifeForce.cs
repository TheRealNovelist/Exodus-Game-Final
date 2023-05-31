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
    
    public static Action OnLifeTimerChangeMoreHalf;
    public static Action OnLifeTimerChangeToHalf;
    public static Action OnLifeTimerAlmostRunOut;
    
    private float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
        maxTime = timeValue;
    }

    public void AddTime(float time)
    {
        timeValue += time;
    }


    // Update is called once per frame
    void Update()
    {
        if(!timerOn) {return;}
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            DisplayTime(timeValue);
            if (timeValue > maxTime * 0.5f)
            {
                OnLifeTimerChangeMoreHalf?.Invoke();
            }
            if (timeValue <= maxTime * 0.5f)
            {
                OnLifeTimerChangeToHalf?.Invoke();
            }

            if (timeValue <= maxTime * 0.3f)
            {
                OnLifeTimerAlmostRunOut?.Invoke();
            }
        }
        else
        {
            timeValue = 0;
            DisplayTime(timeValue);
            timerOn = false;
            WinLoseCondition.OnGameLose?.Invoke();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Damage(float amount, Transform source = null)
    {
        timeValue -= amount;
        
        //ADD EFFECT AND SOUND
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
