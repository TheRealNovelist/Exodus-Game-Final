using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class CoinManager : Singleton<CoinManager>
{
    private int _coinAmount = 100;

    public int CoinAmount
    {
        get { return _coinAmount; }
        set
        {
            if (value < 0)
            {
                _coinAmount = 0;
                return;
            }
            _coinAmount = value;
        }
    }

    [SerializeField] private TMP_Text moneyAmountText;

    public void SpendCoin(int amount)
    {
        CoinAmount -= amount;
        moneyAmountText.SetText("Money " + _coinAmount.ToString());
    }

    public void GainCoin(int amount)
    {
        CoinAmount += amount;
        moneyAmountText.SetText("Money " + _coinAmount.ToString());
    }

}