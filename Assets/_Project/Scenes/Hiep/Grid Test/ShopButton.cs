using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText; 
    [SerializeField] private Image image;

    public PlacedObjectTypeSO Item;


    private void Start()
    {
        nameText.text = Item.nameString;
        priceText.text = Item.price.ToString();
        image.sprite = Item.icon;
    }
}
