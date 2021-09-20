using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        staticInstance = FMODUnity.RuntimeManager.CreateInstance(staticBackground);
        noiseInstance = FMODUnity.RuntimeManager.CreateInstance(radioNoises);
        voiceInstance = FMODUnity.RuntimeManager.CreateInstance(voiceLines);
        staticInstance.start();
        noiseInstance.start();
        voiceInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RadioActive(bool t)
    {
        if(t)
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
}
