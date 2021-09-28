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
    public GameObject optionsSet;
    public GameObject pauseSet;
    public bool isPauseMenu = false;

    private MusicManager mm;
    private RadioManager rm;
    private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        mm = FindObjectOfType<MusicManager>();
        rm = FindObjectOfType<RadioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !paused)
            {
                OpenPauseMenu();
                return;
            }
            if (Input.GetKeyDown(KeyCode.Escape) && paused)
            {
                Unpause();
                return;
            }
            if(paused)
            {
                Cursor.visible = true;
            }
        }
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

    public void OpenOptions()
    {
        FMODUnity.RuntimeManager.PlayOneShot(buttonSFX, transform.position);
        optionsSet.SetActive(true);
        mainSet.SetActive(false);
    }

    public void OpenMain()
    {
        FMODUnity.RuntimeManager.PlayOneShot(buttonSFX, transform.position);
        creditsSet.SetActive(false);
        optionsSet.SetActive(false);
        mainSet.SetActive(true);
    }

    public void Exit()
    {
        mm.StopInstance();
        FMODUnity.RuntimeManager.PlayOneShot(buttonSFX, transform.position);
        Application.Quit();
    }

    public void OpenPauseMenu()
    {
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        if (mm != null) mm.Pause(true);
        mainSet.SetActive(false);
        pauseSet.SetActive(true);
    }

    public void PauseBackToMain()
    {
        FMODUnity.RuntimeManager.PlayOneShot(buttonSFX, transform.position);
        mainSet.SetActive(false);
        optionsSet.SetActive(false);
        pauseSet.SetActive(true);
    }

    public void Unpause()
    {
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (mm != null) mm.Pause(false);
        Time.timeScale = 1;
        mainSet.SetActive(true);
        pauseSet.SetActive(false);
        optionsSet.SetActive(false);
        if (PlayerPrefs.HasKey("Subtitles"))
        {
            rm.SubActive(PlayerPrefs.GetInt("Subtitles"));
        }
    }

    public void ClosePauseMenu()
    {
        FMODUnity.RuntimeManager.PlayOneShot(buttonSFX, transform.position);
        Unpause();
    }

    public void OpenOptionsPaused()
    {
        pauseSet.SetActive(false);
        OpenOptions();
    }
}