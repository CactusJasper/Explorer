using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private SaveManager saveManager;
    // TODO: Look at PlayerPrefs for fixing the ui inconsitencies
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        System.Array.Reverse(resolutions);
        List<string> options = new List<string>();
        int currentResIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            options.Add($"{resolutions[i].width} X {resolutions[i].height} {resolutions[i].refreshRate}hz");
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
                currentResIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
    }

    public void StartNewGame()
    {
        saveManager.SetNewSave(true);
        SceneManager.LoadScene("Level001");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetGraphicsQuality(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }
}
