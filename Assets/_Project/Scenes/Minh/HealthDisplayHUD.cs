using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplayHUD : MonoBehaviour
{
    public PlayerHealth playerHealth;
    [SerializeField] TextMeshProUGUI hpText;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Character").GetComponent<PlayerHealth>();
        hpText = GameObject.Find("HP").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            playerHealth._playerHealth -= 1f;
        }
        UpdateHPText();
    }

    void UpdateHPText()
    {
        hpText.text = " " + playerHealth._playerHealth;
    }
}
