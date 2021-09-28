using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string radioBeep = "event:/RadioIndicator";
    [FMODUnity.EventRef]
    public string radioOff = "event:/RadioOff";

    public Image radioIcon;

    private int numberTasksToWin = 15;
    public DoorScript finalDoor;
    public Text textBox;

    private RadioManager radManager;
    private Text subtitleBox;
    [SerializeField]
    private int numberTasks;
    private int step = 0;
    private int currentVoiceLine = 0;
    private Color startColor;
    // Start is called before the first frame update
    void Start()
    {
        subtitleBox = GameObject.Find("SubtitleBox").GetComponent<Text>();
        numberTasks = 0;
        radManager = FindObjectOfType<RadioManager>();

        startColor = radioIcon.color;
        radioIcon.color = Color.red;

        FMODUnity.RuntimeManager.PlayOneShot(radioBeep, radManager.transform.position);
        Invoke("PlaySubtitle", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (step == 0)
        {
            textBox.text = "Find the keys";
        }
        else if (step == 1)
        {
            textBox.text = "Find the lightbulbs and plants";
        }
        else if (step == 2)
        {
            textBox.text = "Deliminalize: " + numberTasks.ToString() + " out of " + numberTasksToWin;
        }
        else if (step == 3)
        {
            textBox.text = "Get out";
        }
        else
        {
            Debug.LogError("Incorrect step number: " + step);
        }
    }

    public void CompleteTask()
    {
        if (numberTasks >= numberTasksToWin)
        {
            return;
        }
        numberTasks++;
        if (numberTasks >= numberTasksToWin)
        {
            WinConMet();
        }
        Debug.Log("Complete");
    }

    void WinConMet()
    {
        // Allow outside door to be open
        finalDoor.locked = false;
        radManager.tasksDone();
        StepUp(3);
    }

    public void StepUp(int newStep)
    {
        FMODUnity.RuntimeManager.PlayOneShot(radioBeep, radManager.transform.position);
        step = newStep;
        CancelInvoke();
        radioIcon.color = Color.red;
        PlaySubtitleAt(step);
    }

    void PlaySubtitleAt(int i)
    {
        if (i == 1)
        {
            currentVoiceLine = 24;
            PlaySubtitle();
        }
        if (i == 2)
        {
            currentVoiceLine = 36;
            PlaySubtitle();
        }
        if (i == 3)
        {
            currentVoiceLine = 39;
            PlaySubtitle();
        }
    }

    public int NumberTasksDone()
    {
        return numberTasks;
    }

    public void PlaySubtitle()
    {
        switch (currentVoiceLine)
        {
            case 0:
                subtitleBox.text = "hey is this thing on? can you hear me alright kid?";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3.2f);
                break;
            case 1:
                subtitleBox.text = "alright good, welcome to your first solo deliminalizing";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3.8f);
                break;
            case 2:
                subtitleBox.text = "this is probably the hardest night on the job, so";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 2.8f);
                break;
            case 3:
                subtitleBox.text = "I'll warn you now it just feels a lot more real when you're on your own";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 4f);
                break;
            case 4:
                subtitleBox.text = "but don't worry you'll get used to it kid, no problem";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3.4f);
                break;
            case 5:
                subtitleBox.text = "so while you're getting your equipment put together, I'll do a run down of the site";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 4f);
                break;
            case 6:
                subtitleBox.text = "pretty standard liminal space here, this one is a public pool house";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 4f);
                break;
            case 7:
                subtitleBox.text = "first thing in the morning and late at night";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 2f);
                break;

            case 8:
                subtitleBox.text = "all the staff and patrons keep complaining about the building just feeling wrong";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3.5f);
                break;
            case 9:
                subtitleBox.text = "saying it's kinda like, slipping outa wack";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 2.8f);
                break;
            case 10:
                subtitleBox.text = "so, uh... I figured I'd put you on an easy one tonight";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 11:
                subtitleBox.text = "I picked a spot with no alleged supernatural sightings";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 12:
                subtitleBox.text = "just the... regular unease that most of our sites have";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 4f);
                break;
            case 13:
                subtitleBox.text = "uh... once you get workin' into some of those tougher ones though";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 14:
                subtitleBox.text = "oof, you'll be thanking me you're starting out so easy";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 15:
                subtitleBox.text = "so... in any case, this is gonna be your, uh, routine labor";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 5f);
                break;
            case 16:
                subtitleBox.text = "fixing up some of the flickering lights and putting some decorations around";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 17:
                subtitleBox.text = "you know the drill by now";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 18:
                subtitleBox.text = "";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 19:
                subtitleBox.text = "this place is locked down pretty tight here";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 2f);
                break;
            case 20:
                subtitleBox.text = "so you're gonna want to go into the uh womens locker room";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 21:
                subtitleBox.text = "on the far side of the complex and grab the master key ring";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 22:
                subtitleBox.text = "sould be in the janitor's closet in the back of the room";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 23:
                subtitleBox.text = "";
                radioIcon.color = startColor;
                FMODUnity.RuntimeManager.PlayOneShot(radioOff, radManager.transform.position);
                currentVoiceLine++;
                break;
            case 24:
                subtitleBox.text = "alright kid you got the keys?";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 25:
                subtitleBox.text = "okay great";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 1.5f);
                break;
            case 26:
                subtitleBox.text = "so the next two things that you're gonna want to get is";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 2.5f);
                break;
            case 27:
                subtitleBox.text = "a box of lightbulbs, and couple of potted plants";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 28:
                subtitleBox.text = "so the plants should be to the right, on the other side of the hallway, right inside the stairwell";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 5.5f);
                break;
            case 29:
                subtitleBox.text = "should be a couple in there, and you can place those on the designated markers";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 4f);
                break;
            case 30:
                subtitleBox.text = "should be kinda some x's drawn on the ground for where you can place those";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 4f);
                break;
            case 31:
                subtitleBox.text = "and then, there should be a box of lightbulbs in the mens bathroom";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 4f);
                break;
            case 32:
                subtitleBox.text = "on the other side in the janitor's closet over there";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 2.5f);
                break;
            case 33:
                subtitleBox.text = "and you can use those to uh, fix any kinda light bulbs that might be flickering or starting to go out";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 6f);
                break;
            case 34:
                subtitleBox.text = "and that should really uh, bring the space back to life a little bit";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 35:
                subtitleBox.text = "";
                radioIcon.color = startColor;
                FMODUnity.RuntimeManager.PlayOneShot(radioOff, radManager.transform.position);
                currentVoiceLine++;
                break;
            case 36:
                subtitleBox.text = "alright great, now uh, just get to work puttin' down those plants and fixing them lights";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 6f);
                break;
            case 37:
                subtitleBox.text = "and you should be good to go in a few minutes";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 38:
                subtitleBox.text = "";
                radioIcon.color = startColor;
                FMODUnity.RuntimeManager.PlayOneShot(radioOff, radManager.transform.position);
                currentVoiceLine++;
                break;
            case 39:
                subtitleBox.text = "Alright great work today kid, um";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 40:
                subtitleBox.text = "ya know, feel free to just head out through them exit doors";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 41:
                subtitleBox.text = "and you should be all good to go";
                currentVoiceLine++;
                Invoke("PlaySubtitle", 3f);
                break;
            case 42:
                subtitleBox.text = "";
                radioIcon.color = startColor;
                FMODUnity.RuntimeManager.PlayOneShot(radioOff, radManager.transform.position);
                currentVoiceLine++;
                break;
        }
        subtitleBox.text = subtitleBox.text.ToLower();
    }

    public int getMaxTasks() { return numberTasksToWin; }
}
