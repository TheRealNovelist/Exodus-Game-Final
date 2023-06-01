using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene_n_Button : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] bool pauseMenued = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !pauseMenued) 
        {
            pauseMenu.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pauseMenued = true;
            Time.timeScale = 0f;
        }

        else if (Input.GetKeyDown(KeyCode.P) && pauseMenued)
        {
            pauseMenu.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenued = false;
            Time.timeScale = 1f;
        }
    }

    public void ContinueGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenued = false;
        Time.timeScale = 1f;
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
