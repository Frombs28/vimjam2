using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string trackName;

    [HideInInspector]
    public FMOD.Studio.EventInstance trackInstance;
    // Start is called before the first frame update
    void Start()
    {
        trackInstance = FMODUnity.RuntimeManager.CreateInstance(trackName);
        trackInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateTension(int t)
    {
        trackInstance.setParameterByName("Tension", t);
    }
}
