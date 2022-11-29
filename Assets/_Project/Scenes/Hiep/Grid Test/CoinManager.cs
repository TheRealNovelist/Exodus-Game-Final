using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int CoinAmuont;

    [SerializeField] private TMP_Text moneyAmountText;
    // Start is called before the first frame update
    void Start()
    {
        CoinAmuont = 100;
    }

    private void Update()
    {
        moneyAmountText.SetText("Money " + CoinAmuont.ToString());
    }
}
