using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class WinLoseCondition : MonoBehaviour
{
   public static Action OnGameLose;
   public static Action OnGameWon;
   
   public static Action OnBossDefeated;
   [SerializeField] private GameObject gameOverScreen;
   [SerializeField] private GameObject gameWinScreen;
   [SerializeField] private int totalBoss =2;
   private int _defeatedBoss = 0;
   [SerializeField] private TextMeshProUGUI defeatedTmp;

   private void PopUpGameOver()
   {
      gameOverScreen.SetActive(true);
      Time.timeScale = 0;
   }
   
   private void PopUpGameWin()
   {
      gameWinScreen.SetActive(true);
      Time.timeScale = 0;
   }

   private void Start()
   {
      OnGameLose += PopUpGameOver;
      OnGameWon += PopUpGameWin;
      OnBossDefeated += DefeatedBoss;
   }

   private void OnDisable()
   {
      OnGameLose -= PopUpGameOver;
      OnBossDefeated -= DefeatedBoss;
   }

   private void DefeatedBoss()
   {
      _defeatedBoss++;
      defeatedTmp.text = $"{_defeatedBoss}/{totalBoss}";
      if (_defeatedBoss >= totalBoss)
      {
         OnGameWon?.Invoke();
      }
   }
}
