using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panels : MonoBehaviour
{
    private GameObject previousPanel;
    [SerializeField] private string gameScene;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void HidePanel(GameObject panel)
    {
     panel.SetActive(false);   
    }

    public void SavePreviousPanel(GameObject panel)
    {
        previousPanel = panel;
    }
    
    public void ComebackTo()
    {
        previousPanel.SetActive(true);
        previousPanel = null;
    }
    
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);   
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(string gameScene)
    {
        SceneManager.LoadScene(gameScene);
    }

    public void RestartLevel()
    {
        
    }
}
