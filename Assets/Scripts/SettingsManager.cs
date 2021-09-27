using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Dropdown resDropdown;
    public Dropdown qualityDropdown;
    public Dropdown fullscreenDropdown;
    public Dropdown volumeDrowdown;
    public Slider senseSlider;
    Resolution[] resolutions;
    FMOD.Studio.Bus masterBus;
    FMOD.Studio.Bus distoBus;

    private void Start()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        distoBus = FMODUnity.RuntimeManager.GetBus("bus:/Disto");
        distoBus.setVolume(0);

        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        int currentResIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();

        //set player prefs
        if (PlayerPrefs.HasKey("Resolution")) resDropdown.value = PlayerPrefs.GetInt("Resolution");
        else PlayerPrefs.SetInt("Resolution", resDropdown.value);
        if (PlayerPrefs.HasKey("Quality")) qualityDropdown.value = PlayerPrefs.GetInt("Quality");
        else PlayerPrefs.SetInt("Quality", qualityDropdown.value);
        if (PlayerPrefs.HasKey("Fullscreen")) fullscreenDropdown.value = PlayerPrefs.GetInt("Fullscreen");
        else PlayerPrefs.SetInt("Fullscreen", fullscreenDropdown.value);
        if (PlayerPrefs.HasKey("Volume")) volumeDrowdown.value = PlayerPrefs.GetInt("Volume");
        else PlayerPrefs.SetInt("Volume", volumeDrowdown.value);
        if (PlayerPrefs.HasKey("Sensetivity")) senseSlider.value = PlayerPrefs.GetFloat("Sensetivity");
        else PlayerPrefs.SetFloat("Sensetivity", senseSlider.value);
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("Quality", index);
    }

    public void SetFullscreen(int mode)
    {
        Screen.fullScreenMode = (FullScreenMode)mode;
        if (mode >= 2)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }
        PlayerPrefs.SetInt("Fullscreen", mode);
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", index);
    }

    public void SetVolume(int index)
    {
        switch (index)
        {
            case 0:
                distoBus.setVolume(0f);
                masterBus.setVolume(0.6f);
                break;
            case 1:
                distoBus.setVolume(0f);
                masterBus.setVolume(0.8f);
                break;

            case 2:
                distoBus.setVolume(0f);
                masterBus.setVolume(1f);
                break;

            case 3:
                distoBus.setVolume(1f);
                masterBus.setVolume(0.5f);
                break;
        }
        PlayerPrefs.SetInt("Volume", index);
    }

    public void SetSense(float s)
    {
        PlayerPrefs.SetFloat("Sensetivity", s);
    }
}
