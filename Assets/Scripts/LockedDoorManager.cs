using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorManager : MonoBehaviour
{
    public List<DoorScript> lockedDoors;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockAllDoors()
    {
        foreach(DoorScript door in lockedDoors)
        {
            door.locked = false;
        }
    }
}
