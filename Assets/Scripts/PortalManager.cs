using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public List<GameObject> allDoorTeleporters;
    public int lowPercent = 33;
    public int highPercent = 66;

    private TaskManager tm;
    private int numTasksDone;
    private int maxTasks;
    private List<DoorScript> doors;
    // Start is called before the first frame update
    void Start()
    {
        tm = FindObjectOfType<TaskManager>();
        maxTasks = tm.getMaxTasks();
        doors = new List<DoorScript>();
        foreach(GameObject door in allDoorTeleporters)
        {
            doors.Add(door.transform.parent.gameObject.GetComponent<DoorScript>());
            door.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoorOpen(DoorScript door)
    {
        int index = doors.IndexOf(door);
        numTasksDone = tm.NumberTasksDone();
        if(numTasksDone < 5)
        {
            // nothing
        }
        else if(numTasksDone >= 5 && numTasksDone < 10)
        {
            // 33% chance
            float percent = Random.Range(1, 101);
            if(percent <= lowPercent)
            {
                // Create portal
                allDoorTeleporters[index].SetActive(true);
                door.TeleporterActive();
            }
        }
        else if(numTasksDone >= 10 && numTasksDone < 15)
        {
            // 66% chance
            float percent = Random.Range(1, 101);
            if (percent <= highPercent)
            {
                // Create portal
                allDoorTeleporters[index].SetActive(true);
                door.TeleporterActive();
            }
        }
        else
        {
            // 80% chance
            float percent = Random.Range(1, 101);
            if (percent <= 80)
            {
                // Create portal
                allDoorTeleporters[index].SetActive(true);
                door.TeleporterActive();
            }
        }
    }

    public void CloseDoor(DoorScript door)
    {
        int index = doors.IndexOf(door);
        allDoorTeleporters[index].SetActive(false);
    }
}
