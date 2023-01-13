using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameController : MonoBehaviour
{
    public static int PlayerGraphics;
    public static int PlayerResolutionIndex;
    public static float PlayerMasterVolume;
    public static bool PlayerFullscreen;
    public AudioMixer MasterMixer;
    Resolution[] Resolutions;
    public Dropdown ResolutionDropdown;
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
                PlayerResolutionIndex = i;

            }
        }
        ResolutionDropdown.AddOptions(Options);
        ResolutionDropdown.value = CurrentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
    }
    public void SetMasterVolume(float volume)
    {
        MasterMixer.SetFloat("MasterVolume", volume);
        PlayerMasterVolume = volume;
    }
    public void GraphicSettings(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
        PlayerGraphics = QualityIndex;
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerFullscreen = isFullscreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}