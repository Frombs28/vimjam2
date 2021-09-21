using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public int numberTasksToWin = 15;
    public DoorScript finalDoor;
    public Text textBox;

    private RadioManager radManager;
    [SerializeField]
    private int numberTasks;
    private int step = 0;
    // Start is called before the first frame update
    void Start()
    {
        numberTasks = 0;
        radManager = FindObjectOfType<RadioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (step == 0)
        {
            textBox.text = "Find the keys";
        }
        else if(step == 1)
        {
            textBox.text = "Find the lightbulbs and plants";
        }
        else if(step == 2)
        {
            textBox.text = "Deliminalize: " + numberTasks.ToString() + " out of " + numberTasksToWin;
        }
        else if(step == 3)
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
        if(numberTasks >= numberTasksToWin)
        {
            return;
        }
        numberTasks++;
        if(numberTasks >= numberTasksToWin)
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
        step = newStep;
    }
}
