using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class RadioManager : MonoBehaviour
{

    [StructLayout(LayoutKind.Sequential)]
    public class ClipInfo
    {
        public string name = "";
    }

    [FMODUnity.EventRef]
    public string staticBackground;
    [FMODUnity.EventRef]
    public string radioNoises;
    [FMODUnity.EventRef]
    public string voiceLines;

    [HideInInspector]
    public ClipInfo clipInfo;
    GCHandle clipHandle;

    [HideInInspector]
    public FMOD.Studio.EventInstance staticInstance;
    [HideInInspector]
    public FMOD.Studio.EventInstance noiseInstance;
    [HideInInspector]
    public FMOD.Studio.EventInstance voiceInstance;

    FMOD.Studio.EVENT_CALLBACK playCallback;
    string currentPlayingVoiceClip;
    // Start is called before the first frame update
    void Start()
    {
        playCallback = new FMOD.Studio.EVENT_CALLBACK(PlayEventCallback);


        staticInstance = FMODUnity.RuntimeManager.CreateInstance(staticBackground);
        noiseInstance = FMODUnity.RuntimeManager.CreateInstance(radioNoises);
        voiceInstance = FMODUnity.RuntimeManager.CreateInstance(voiceLines);
        staticInstance.start();
        noiseInstance.start();

        clipHandle = GCHandle.Alloc(clipInfo, GCHandleType.Pinned);
        voiceInstance.setUserData(GCHandle.ToIntPtr(clipHandle));

        voiceInstance.setCallback(playCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.SOUND_PLAYED);
        voiceInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        print(clipInfo.name);
    }

    void OnDestroy()
    {
        voiceInstance.setUserData(IntPtr.Zero);
        voiceInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        voiceInstance.release();
        clipHandle.Free();
    }

    public void RadioActive(bool t)
    {
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

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT PlayEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);
        // Retrieve the user data
        IntPtr clipInfoPtr;
        FMOD.RESULT result = instance.getUserData(out clipInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (clipInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle clipHandle = GCHandle.FromIntPtr(clipInfoPtr);
            ClipInfo clipInfo = (ClipInfo)clipHandle.Target;

            var parameter = (FMOD.Studio.SOUND_INFO)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.SOUND_INFO));
            clipInfo.name = parameter.name;
        }
        return FMOD.RESULT.OK;
    }
}
