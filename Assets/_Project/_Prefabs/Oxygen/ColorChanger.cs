using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public float totalTime = 10f; // Thời gian tổng để chuyển màu

    private Renderer rend;
    private Color startColor;
    private Color middleColor;
    private Color endColor;
    private float timer = 0f;

    private void Start()
    {
        timer = totalTime;
        rend = GetComponent<Renderer>();
        startColor = Color.green;
        middleColor = Color.yellow;
        endColor = Color.red;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        Debug.Log(timer);
        if (timer <= 0f)
        {
            timer = totalTime; // Đặt lại timer
            rend.material.color = startColor; // Đặt lại màu ban đầu
        }
        if (timer <= totalTime * 0.5f)
        {
            rend.material.color = middleColor; // Chuyển sang màu vàng
        }
        if (timer < totalTime * 0.2f)
        {
            rend.material.color = endColor; // Chuyển sang màu đỏ
        }
        if(timer > totalTime * 0.5f)
        {
            rend.material.color = startColor; // Màu ban đầu
        }
    }
}