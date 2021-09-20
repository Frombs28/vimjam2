using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

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
        SceneManager.LoadScene(1);
    }

    public void OpenCredits()
    {
        creditsSet.SetActive(true);
        mainSet.SetActive(false);
    }

    public void OpenMain()
    {
        creditsSet.SetActive(false);
        mainSet.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
