using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationText : MonoBehaviour
{
    public TextMeshProUGUI notiTMP;
    
    public void PopUp(string text, float duration)
    {
        notiTMP.text = text;
        StartCoroutine(WaitToHideNoti(duration));
    }

    IEnumerator WaitToHideNoti( float duration)
    {
        yield return new WaitForSeconds(duration);
        notiTMP.text = "";
    }


}