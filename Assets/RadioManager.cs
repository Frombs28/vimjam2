using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string staticBackground;
    [FMODUnity.EventRef]
    public string radioNoises;

    [HideInInspector]
    public FMOD.Studio.EventInstance staticInstance;
    [HideInInspector]
    public FMOD.Studio.EventInstance noiseInstance;
    // Start is called before the first frame update
    void Start()
    {
        staticInstance = FMODUnity.RuntimeManager.CreateInstance(staticBackground);
        noiseInstance = FMODUnity.RuntimeManager.CreateInstance(radioNoises);
        staticInstance.start();
        noiseInstance.start();
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
        }
        else
        {
            staticInstance.setVolume(0);
            noiseInstance.setVolume(0);
        }
    }
}
