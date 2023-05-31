
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot:MonoBehaviour
{
    public EquipItem _item;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI nameTMP, keyTMP;
    [SerializeField] private KeyCode key;

    public void UpdateSlotUI()
    {
        if (_item != null)
        {
            _image.color += new Color(0,0,0,255);
            _image.sprite = _item.icon;
            nameTMP.text = _item.name;
        }
        else
        {
            _image.sprite = null;
            _image.color -= new Color(0,0,0,255);
            nameTMP.text = "";
        }

    }

    private void Start()
    {
        keyTMP.text = key.ToString();

        UpdateSlotUI();
    }
}
