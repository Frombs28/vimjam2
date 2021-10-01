using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConditionManager : MonoBehaviour
{
    public PlayerMovement player;
    public MonsterScript monster;
    public Image fadeOut;
    public string winScene;
    public string loseScene;
    public GameObject virtualCam;

    private MusicManager mm;
    private RadioManager rm;

    private void Awake()
    {
        //virtualCam.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        mm = FindObjectOfType<MusicManager>();
        rm = FindObjectOfType<RadioManager>();
        StartCoroutine(WaitToActivateCam());
        player.canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win()
    {
        // Fade to white, move to win scene
        player.enabled = false;
        monster.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        StartCoroutine(WinTheGame());
        rm.StopInstance();
        mm.StopInstance();
    }

    public void Lose()
    {
        //Already faded to black, just load lose scene
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(loseScene);
        rm.StopInstance();
        mm.StopInstance();
    }

    IEnumerator WinTheGame()
    {
        Color tempColor = Color.white;
        tempColor.a = 0.0f;
        fadeOut.color = tempColor;
        while(fadeOut.color.a < 1.0f)
        {
            Color yeet = fadeOut.color;
            yeet.a += (Time.deltaTime / 3.5f);
            fadeOut.color = yeet;
            yield return null;
        }
        // Load Win scene
        SceneManager.LoadScene(winScene);
    }

    IEnumerator WaitToActivateCam()
    {
        yield return new WaitForSeconds(1f);
        virtualCam.SetActive(true);
        player.canMove = true;
    }
}
