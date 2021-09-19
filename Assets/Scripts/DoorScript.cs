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

    private Transform currentPlayerTransform;
    private bool primedToClose = false;

    // Start is called before the first frame update
    void Start()
    {
        if (locked)
        {
            FindObjectOfType<LockedDoorManager>().lockedDoors.Add(this);
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
            }
        }
    }

    public void OpenDoor(Vector3 playerPos,Transform playerTrans)
    {
        if (locked)
        {
            return;
        }
        if (finale)
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
            otherLinkedDoor.OpenDoor(dir);
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
        if (linkedToDoor)
        {
            otherLinkedDoor.OpenDoor(forward);
        }
    }

    public void Close()
    {
        anim.SetBool("OpenBack", false);
        anim.SetBool("OpenFor", false);
        box.enabled = true;
        primedToClose = false;
    }

    IEnumerator WaitToClose()
    {
        yield return new WaitForSeconds(0.2f);
        primedToClose = true;
    }
}
