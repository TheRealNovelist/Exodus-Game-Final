using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene_n_Button : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] bool paused = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        paused = true;
        Time.timeScale = 0f;

        PlayerInputManager.Input.Disable();
    }
    
    public void ContinueGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        paused = false;
        Time.timeScale = 1f;

        PlayerInputManager.Input.Enable();
    }

    public void ExitButton(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1f;
    }

    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
