using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string buttonSFX;
    public GameObject creditsSet;
    public GameObject mainSet;

    private MusicManager mm;

    // Start is called before the first frame update
    void Start()
    {
        mm = FindObjectOfType<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMain()
    {
        if(mm != null) mm.StopInstance();
        FMODUnity.RuntimeManager.PlayOneShot(buttonSFX, transform.position);
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        if (mm != null) mm.StopInstance();
        FMODUnity.RuntimeManager.PlayOneShot(buttonSFX, transform.position);
        SceneManager.LoadScene(1);
    }

    public void OpenCredits()
    {
        FMODUnity.RuntimeManager.PlayOneShot(buttonSFX, transform.position);
        creditsSet.SetActive(true);
        mainSet.SetActive(false);
    }

    public void OpenMain()
    {
        FMODUnity.RuntimeManager.PlayOneShot(buttonSFX, transform.position);
        creditsSet.SetActive(false);
        mainSet.SetActive(true);
    }

    public void Exit()
    {
        mm.StopInstance();
        FMODUnity.RuntimeManager.PlayOneShot(buttonSFX, transform.position);
        Application.Quit();
    }
}
