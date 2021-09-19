using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public int numberTasksToWin = 20;
    private int numberTasks;
    // Start is called before the first frame update
    void Start()
    {
        numberTasks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompleteTask()
    {
        numberTasks++;
        if(numberTasks >= numberTasksToWin)
        {
            WinConMet();
        }
    }

    void WinConMet()
    {

    }
}
