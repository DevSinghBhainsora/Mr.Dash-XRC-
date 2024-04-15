using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenus;
    public GameObject StopUI;

    public static bool GamePause = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePause)
            {
                resume();
                Cursor.lockState = CursorLockMode.Locked;
                StopUI.SetActive(true);
            }
            else
            {
                pause();
                Cursor.lockState = CursorLockMode.None;
                StopUI.SetActive(false);
            }
        }
    }
    public void pause()
    {
        PauseMenus.SetActive(true);
        Time.timeScale = 0f;
        GamePause = true;
    }
    public void restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public void quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void resume()
    {
        PauseMenus.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GamePause = false;
    }
}