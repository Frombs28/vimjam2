using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject hand;
    public Camera mainCamera;
    public Animator anim;

    private bool readyForDoor = false;
    private bool readyToInteract = false;
    private bool currentlyInteracting = false;
    private bool holdingSomething = false;
    private GameObject objectBeingHeld;
    private InteractableTask currentTask;
    private DoorScript currentDoor;
    [SerializeField]
    private List<GameObject> objectsInRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyInteracting)
        {
            return;
        }
        if(readyToInteract && Input.GetKeyDown(KeyCode.E))
        {
            // Interact; lock player movement, begin interactable animation.
            BeginInteracting();
        }
        if(readyForDoor && Input.GetKeyDown(KeyCode.E))
        {
            // Open door
            currentDoor.OpenDoor(player.transform.position);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (holdingSomething)
            {
                Drop();
            }
            else
            {
                PickUp();
            }
        }
    }

    void PickUp()
    {
        if(objectsInRange.Count == 0)
        {
            return;
        }
        float minDistance = 10.0f;
        Vector3 center = new Vector3(0.5f,0.5f,0.0f);
        foreach (GameObject thing in objectsInRange)
        {
            Vector3 viewPos = mainCamera.WorldToViewportPoint(thing.transform.position);
            float distance = Vector3.Distance(viewPos, center);
            if(distance < minDistance)
            {
                minDistance = distance;
                //Debug.Log("Distance of " + thing.name + " : " + distance);
                objectBeingHeld = thing;
            }
        }
        holdingSomething = true;
        hand.GetComponent<MeshRenderer>().enabled = false;
        objectBeingHeld.transform.position = hand.transform.position;
        objectBeingHeld.transform.rotation = hand.transform.rotation;
        objectBeingHeld.transform.parent = hand.transform;
        objectBeingHeld.layer = 9;
    }

    void Drop()
    {
        holdingSomething = false;
        objectBeingHeld.transform.parent = null;
        objectBeingHeld.layer = 11;
        objectBeingHeld.transform.eulerAngles = Vector3.zero;
        objectBeingHeld = null;
        hand.GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Grabbable")
        {
            if (!objectsInRange.Contains(other.gameObject))
            {
                objectsInRange.Add(other.gameObject);
            }
        }
        if(other.tag == "Interactable")
        {
            readyToInteract = true;
            currentTask = other.gameObject.GetComponent<InteractableTask>();
        }
        if(other.tag == "Door")
        {
            currentDoor = other.gameObject.GetComponent<DoorScript>();
            readyForDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Grabbable")
        {
            objectsInRange.Remove(other.gameObject);
        }
        if (other.tag == "Interactable")
        {
            readyToInteract = false;
        }
        if (other.tag == "Door")
        {
            readyForDoor = false;
            currentDoor = null;
        }
    }

    void BeginInteracting()
    {
        player.canMove = false;
        readyToInteract = false;
        currentlyInteracting = true;
        string animName = currentTask.animationName;
        anim.Play(animName);
        // Play anim
    }

    public void StopInteracting()
    {
        // Either call from anim or call from coroutine after anim length
        player.canMove = true;
        currentlyInteracting = false;
        currentTask = null;
    }

}