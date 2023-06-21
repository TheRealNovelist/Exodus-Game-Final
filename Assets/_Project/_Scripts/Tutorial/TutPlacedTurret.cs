using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPlacedTurret : MonoBehaviour
{
    [SerializeField] private GameObject endTutorialGO;
    public static Action OnPlaceTurret;

    private void OnDisable()
    {
        OnPlaceTurret -= BoughtTurret;
    }

    public void LockCursor()
    {
        PlayerCursor.ToggleCursor(false);
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        OnPlaceTurret += BoughtTurret;
    }

    public void BoughtTurret()
    {
        StartCoroutine(WaitToEndTut());
    }


    IEnumerator WaitToEndTut()
    {
        yield return new WaitForSeconds(3f);

        endTutorialGO.SetActive(true);
        PlayerCursor.ToggleCursor(true);
    }
}