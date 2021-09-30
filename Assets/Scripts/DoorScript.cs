using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator anim;
    public BoxCollider box;
    public bool locked = false;
    public bool linkedToDoor = false;
    public float distanceToClose = 7.0f;
    public DoorScript otherLinkedDoor;
    public bool finale = false;
    [FMODUnity.EventRef]
    public string lockedDoorNoise;
    [FMODUnity.EventRef]
    public string openDoorNoise;
    public bool canBePortal = false;
    public bool forwardPortal = true;
    public PortalCam portalCamera;

    private Transform currentPlayerTransform;
    private bool primedToClose = false;
    private PortalManager pm;
    private bool secondOpen = false;
    private bool linkedDir;
    private bool teleporterActive = false;

    // Start is called before the first frame update
    void Start()
    {
        if (locked)
        {
            FindObjectOfType<LockedDoorManager>().lockedDoors.Add(this);
        }
        if (finale)
        {
            FindObjectOfType<TaskManager>().finalDoor = this;
        }
        if(canBePortal)
        {
            pm = FindObjectOfType<PortalManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (primedToClose)
        {
            float distance = Vector3.Distance(transform.position, currentPlayerTransform.position);
            if(distance >= distanceToClose)
            {
                Close();
                if (teleporterActive)
                {
                    pm.CloseDoor(this);
                    teleporterActive = false;
                }
                if (linkedToDoor)
                {
                    // Compare current player pos to linked door. If it is too far from it, close it, otherwise keep it open.
                    float linkDistance = Vector3.Distance(otherLinkedDoor.gameObject.transform.position, currentPlayerTransform.position);
                    if(linkDistance >= distanceToClose)
                    {
                        otherLinkedDoor.Close();
                    }
                    else
                    {
                        otherLinkedDoor.WaitClose(currentPlayerTransform);
                    }
                }
            }
        }
    }

    public void OpenDoor(Vector3 playerPos,Transform playerTrans)
    {
        if (locked)
        {
            FMODUnity.RuntimeManager.PlayOneShot(lockedDoorNoise, transform.position);
            return;
        }
        if(finale && !secondOpen)
        {
            secondOpen = true;
        }
        else if (finale && secondOpen)
        {
            FindObjectOfType<ConditionManager>().Win();
        }
        Vector3 doorToPlayer = new Vector3(playerPos.x, transform.position.y, playerPos.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float angleBetween = Vector3.Angle(transform.forward, doorToPlayer);
        bool dir;
        if (angleBetween > 90.0f)
        {
            // Open "forwards" (left door knob swings back on single doors)
            anim.SetBool("OpenFor", true);
            dir = true;
        }
        else
        {
            // Open "backwards" (right door knob swings back on single doors)
            anim.SetBool("OpenBack", true);
            dir = false;
        }
        box.enabled = false;
        currentPlayerTransform = playerTrans;
        StartCoroutine(WaitToClose());
        if (linkedToDoor)
        {
            //otherLinkedDoor.OpenDoor(dir);
            linkedDir = dir;
        }
        FMODUnity.RuntimeManager.PlayOneShot(openDoorNoise, transform.position);
        if(dir == forwardPortal && canBePortal)
        {
            pm.DoorOpen(this);
        }
        if(portalCamera != null)
        {
            portalCamera.Activate();
        }
    }

    public void OpenDoor(bool forward)
    {
        if (locked)
        {
            return;
        }
        if (forward)
        {
            // Open "forwards" (left door knob swings back on single doors)
            anim.SetBool("OpenFor", true);
        }
        else
        {
            // Open "backwards" (right door knob swings back on single doors)
            anim.SetBool("OpenBack", true);
        }
        box.enabled = false;
        
        //if (linkedToDoor)
        //{
        //    otherLinkedDoor.OpenDoor(forward);
        //}
        
    }

    public void TeleporterActive()
    {
        otherLinkedDoor.OpenDoor(linkedDir);
        teleporterActive = true;
    }

    public void Close()
    {
        anim.SetBool("OpenBack", false);
        anim.SetBool("OpenFor", false);
        box.enabled = true;
        primedToClose = false;
        if (portalCamera != null)
        {
            portalCamera.Deactivate();
        }
    }

    IEnumerator WaitToClose()
    {
        yield return new WaitForSeconds(0.2f);
        primedToClose = true;
    }

    public void WaitClose(Transform playerTrans)
    {
        currentPlayerTransform = playerTrans;
        StartCoroutine(WaitToClose());
    }
}
