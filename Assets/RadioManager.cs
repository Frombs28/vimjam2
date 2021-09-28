using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class RadioManager : MonoBehaviour
{


    [FMODUnity.EventRef]
    public string staticBackground;
    [FMODUnity.EventRef]
    public string radioNoises;
    [FMODUnity.EventRef]
    public string voiceLines;

    [HideInInspector]
    public FMOD.Studio.EventInstance staticInstance;
    [HideInInspector]
    public FMOD.Studio.EventInstance noiseInstance;
    [HideInInspector]
    public FMOD.Studio.EventInstance voiceInstance;

    FMOD.Studio.EVENT_CALLBACK playCallback;
    string currentPlayingVoiceClip;

    private GameObject radioText;
    // Start is called before the first frame update
    void Start()
    {
        radioText = GameObject.Find("SubtitleBox");

        staticInstance = FMODUnity.RuntimeManager.CreateInstance(staticBackground);
        noiseInstance = FMODUnity.RuntimeManager.CreateInstance(radioNoises);
        voiceInstance = FMODUnity.RuntimeManager.CreateInstance(voiceLines);
        staticInstance.start();
        noiseInstance.start();
        voiceInstance.start();

        if (PlayerPrefs.HasKey("Subtitles"))
        {
            if (PlayerPrefs.GetInt("Subtitles") == 1)
            {
                radioText.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SubActive(int t)
    {
        if (t == 0)
        {
            float vol = 0;
            voiceInstance.getVolume(out vol);
            if (vol > 0.1f)
            {
                radioText.SetActive(true);
            }
        }
        else radioText.SetActive(false);
    }

    public void RadioActive(bool t)
    {
        if (PlayerPrefs.HasKey("Subtitles"))
        {
            if (PlayerPrefs.GetInt("Subtitles") == 1)
            {
                radioText.SetActive(false);
            }
            else
            {
                radioText.SetActive(t);
            }
        }
        else
        {
            radioText.SetActive(t);
        }
        if (t)
        {
            staticInstance.setVolume(1);
            noiseInstance.setVolume(1);
            voiceInstance.setVolume(1);
        }
        else
        {
            staticInstance.setVolume(0);
            noiseInstance.setVolume(0);
            voiceInstance.setVolume(0);
        }
    }

    public void getKeys()
    {
        voiceInstance.setParameterByName("haskeys", 1);
    }

    public void getItems()
    {
        voiceInstance.setParameterByName("hasitems", 1);
    }

    public void tasksDone()
    {
        voiceInstance.setParameterByName("isdone", 1);
    }

    public void StopInstance()
    {
        staticInstance.release();
        noiseInstance.release();
        voiceInstance.release();
        staticInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        noiseInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        voiceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PauseInstance(bool b)
    {
        staticInstance.setPaused(b);
        noiseInstance.setPaused(b);
        voiceInstance.setPaused(b);
    }
}
