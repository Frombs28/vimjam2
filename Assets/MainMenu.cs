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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
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
        FMODUnity.RuntimeManager.PlayOneShot(buttonSFX, transform.position);
        Application.Quit();
    }
}
