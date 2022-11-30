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
    [SerializeField] private Button button;

    [SerializeField] private Image image;
    // Start is called before the first frame update
    public void SetupButton(PlacedObjectTypeSO type, UnityAction setButton)
    {
        nameText.text = type.nameString;
        priceText.text = type.price.ToString();
        image.sprite = type.icon;
        button.onClick.AddListener(setButton);
    }
}
