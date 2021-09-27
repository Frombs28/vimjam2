using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string trackName;

    private RadioManager rm;

    [HideInInspector]
    public FMOD.Studio.EventInstance trackInstance;
    // Start is called before the first frame update
    void Start()
    {
        rm = FindObjectOfType<RadioManager>();

        trackInstance = FMODUnity.RuntimeManager.CreateInstance(trackName);
        trackInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateTension(int t)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Tension", t);
    }

    public void StopInstance()
    {
        trackInstance.release();
        trackInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void Pause(bool state)
    {
        trackInstance.setPaused(state);
        rm.PauseInstance(state);
    }
}
