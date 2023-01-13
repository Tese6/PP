using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ButtonHandler : MonoBehaviour
{
    public KeyCode Pause = KeyCode.Escape;
    public static bool Paused = false;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject FailMenu;
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject WinMenu;
    void Update()
    {
        if (Input.GetKeyDown(Pause))
        {
            if (Paused == false)
            {
                pause();
            }
            else
            {
                resume();
            }
        }
        if (jumpKing.Won == true)
        {
            OpenWinMenu();
        }
        StartCoroutine(WaitToEnd());
    }
    public void pause()
    {
        PauseMenu.SetActive(true);
        Paused = true;
        Time.timeScale = 0f;
    }
    public void resume()
    {
        PauseMenu.SetActive(false);
        FailMenu.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }
    public void ReturnToMenu(int MenuID)
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        Time.timeScale = 1f;
        SceneManager.LoadScene(MenuID);
    }
    public void fail()
    {
        FailMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Restart()
    {
        FailMenu.SetActive(false);
        PauseMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Options()
    {
        OptionsMenu.SetActive(true);
    }
    public void Play(int LevelID)
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        Time.timeScale = 1f;
        SceneManager.LoadScene(LevelID);
    }
    public void CloseOptionsMenu()
    {
        OptionsMenu.SetActive(false);
    }
    public void OpenWinMenu()
    {
        WinMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    private IEnumerator WaitToEnd()
    {
        if (jumpKing.PlayerHealth <= 0)
        {
            yield return new WaitForSeconds(1);
            fail();
        }
    }
}
