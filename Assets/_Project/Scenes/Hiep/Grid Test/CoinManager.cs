using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class CoinManager : MonoBehaviour
{
    public int CoinAmount =100;
    
    [SerializeField] private TMP_Text moneyAmountText;
    // Start is called before the first frame update


    private void Update()
    {
        moneyAmountText.SetText("Money " + CoinAmount.ToString());
    }
}
