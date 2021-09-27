using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Dropdown resDropdown;
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
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
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
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
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
    }

    public void SetSense(float s)
    {
        PlayerPrefs.SetFloat("Sensetivity", s);
    }
}
