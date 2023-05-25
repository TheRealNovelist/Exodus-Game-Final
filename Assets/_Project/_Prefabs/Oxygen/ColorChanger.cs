using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    

    private Renderer rend;
    private Color startColor;
    private Color middleColor;
    private Color endColor;
    private float timer = 0f;

    private void Start()
    {
        LifeForce.OnLifeTimerChangeMoreHalf += HandleLifeTimerChangeMoreHalf;
        LifeForce.OnLifeTimerChangeToHalf += HandleLifeTimerChangeToHalf;
        LifeForce.OnLifeTimerAlmostRunOut += HandleLifeTimerChangeRunOut;
        rend = GetComponent<Renderer>();
        startColor = Color.green;
        middleColor = Color.yellow;
        endColor = Color.red;
    }
    private void OnDestroy()
    {
        LifeForce.OnLifeTimerChangeMoreHalf -= HandleLifeTimerChangeMoreHalf;
        LifeForce.OnLifeTimerChangeToHalf -= HandleLifeTimerChangeToHalf;
        LifeForce.OnLifeTimerAlmostRunOut -= HandleLifeTimerChangeRunOut;
    }

    private void HandleLifeTimerChangeMoreHalf()
    {
        rend.material.color = startColor;
    }
    private void HandleLifeTimerChangeToHalf()
    {
        rend.material.color = middleColor;
    }
    private void HandleLifeTimerChangeRunOut()
    {
        rend.material.color = endColor;
    }
}