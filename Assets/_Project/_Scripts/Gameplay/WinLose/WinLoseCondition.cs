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
   [SerializeField] private GameOverPanel gameOverScreen;
   [SerializeField] private GameOverPanel gameWinScreen;
   [SerializeField] private int totalBoss =2;
   private int _defeatedBoss = 0;
   [SerializeField] private TextMeshProUGUI defeatedTmp;

   private void PopUpGameOver()
   {
      gameOverScreen.gameObject.SetActive(true);
      gameOverScreen.statusText.text = $"Defeated: {_defeatedBoss}/{totalBoss}";
   }
   
   private void PopUpGameWin()
   {
      gameWinScreen.gameObject.SetActive(true);
      
      float minutes = Mathf.FloorToInt(LifeForce.TimeRemaining / 60);
      float seconds = Mathf.FloorToInt(LifeForce.TimeRemaining % 60);
      gameOverScreen.statusText.text = $"Remaining time: {string.Format("{0:00}:{1:00}", minutes, seconds)}";
   }

   private void Start()
   {
      OnGameLose += PopUpGameOver;
      OnGameWon += PopUpGameWin;
      OnBossDefeated += DefeatedBoss;
      
      defeatedTmp.text = $"{_defeatedBoss}/{totalBoss}";
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
