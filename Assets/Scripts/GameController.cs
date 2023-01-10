using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    public float CurrentTime;
    public bool CountingDown;
    public AudioMixer MasterMixer;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject FailMenu;
    [SerializeField] GameObject OptionsMenu;
    public int MenuID = 1;
    public int LevelID = 0;
    Resolution[] Resolutions;
    public Dropdown ResolutionDropdown;
    public KeyCode Pause = KeyCode.Escape;
    public static bool Paused = false;
    void Start()
    {
        Resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();
        List<string> Options = new List<string>();
        int CurrentResolutionIndex = 0;
        for (int i = 0; i < Resolutions.Length; i++)
        {
            string Option = Resolutions[i].width + "x" + Resolutions[i].height;
            Options.Add(Option);
            if (Resolutions[i].width == Screen.currentResolution.width && Resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolutionIndex = i;
            }
        }
        ResolutionDropdown.AddOptions(Options);
        ResolutionDropdown.value = CurrentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
    }
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
        CurrentTime = CountingDown ? CurrentTime -= Time.deltaTime : CurrentTime += Time.deltaTime;
        TimerText.text = CurrentTime.ToString("0.00");
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
    public void SetMasterVolume(float volume)
    {
        MasterMixer.SetFloat("MasterVolume", volume);
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
    public void GraphicSettings(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}