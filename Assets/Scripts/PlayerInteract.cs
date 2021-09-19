using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerMovement player;
    //public Cinemachine.CinemachineVirtualCamera virtualCam;
    public GameObject hand;
    public Camera mainCamera;
    public Animator anim;
    public Light flashlight;
    public List<MeshRenderer> handObjects;
    // hand = 0, dont mess with this
    // radio = 1
    // flashlight = 2
    // keys = 3
    // lightbulb = 4
    // plant = 5

    private int currentItemInHand;
    private bool readyForPickup = false;
    private bool readyForDoor = false;
    private bool readyToInteract = false;
    private bool currentlyInteracting = false;
    private InteractableTask currentPickup;
    private InteractableTask currentTask;
    private DoorScript currentDoor;
    private Coroutine taskCoroutine;
    private float taskDuration;
    private bool hasRadio = true;
    private bool hasFlashlight = true;
    private bool hasKeys = false;
    private bool hasLightbulbs = false;
    private bool hasPlants = false;
    private bool readyForKey = false;
    private GameObject keys;
    private TaskManager tManager;
    // Start is called before the first frame update
    void Start()
    {
        handObjects[0].enabled = false;
        handObjects[1].enabled = true;
        handObjects[2].enabled = false;
        handObjects[3].enabled = false;
        handObjects[3].gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        handObjects[4].enabled = false;
        currentItemInHand = 1;
        flashlight.enabled = false;
        tManager = FindObjectOfType<TaskManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyInteracting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentTask.anim.SetBool("DoTask", true);
                taskCoroutine = StartCoroutine(WaitForInteraction(taskDuration));
            }
            else if(Input.GetMouseButtonUp(0))
            {
                currentTask.anim.SetBool("DoTask", false);
                StopCoroutine(taskCoroutine);
            }
            return;
        }
        if (readyToInteract && Input.GetKeyDown(KeyCode.E) && !currentTask.completed && (currentTask.requiredItem == 0 || (currentTask.requiredItem == currentItemInHand)))
        {
            // Interact; lock player movement, begin interactable animation.
            BeginInteracting();
        }
        if(readyForDoor && Input.GetKeyDown(KeyCode.E))
        {
            // Open door
            currentDoor.OpenDoor(player.transform.position,player.transform);
        }
        if (readyForPickup && Input.GetMouseButtonDown(0))
        {
            // Add that pickup to our array
            PickUp();
        }
        if (readyForKey && Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<LockedDoorManager>().UnlockAllDoors();
            keys.SetActive(false);
        }

        if(currentItemInHand == 2 && Input.GetMouseButtonDown(1))
        {
            flashlight.enabled = !flashlight.enabled;
        }
        if(hasRadio && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(1);
        }
        if (hasFlashlight && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Equip(2);
        }
        if (hasLightbulbs && Input.GetKeyDown(KeyCode.Alpha3))
        {
            Equip(3);
        }
        if (hasPlants && Input.GetKeyDown(KeyCode.Alpha4))
        {
            Equip(4);
        }

        //anim stuff
        anim.SetFloat("Speed",player.animSpeed);
    }

    void PickUp()
    {
        int itemNum = currentPickup.pickupNum;
        if(itemNum == 3)
        {
            hasLightbulbs = true;
        }
        else if(itemNum == 4)
        {
            hasPlants = true;
        }
        else
        {
            Debug.LogError("Wrong current pickup number: " + itemNum);
        }
        Equip(itemNum);
        currentPickup.gameObject.SetActive(false);
        currentPickup = null;
        readyForPickup = false;
    }

    void Equip(int itemNumber)
    {
        if(itemNumber == currentItemInHand)
        {
            return;
        }
        handObjects[currentItemInHand].enabled = false;
        for(int i = 0; i < handObjects[currentItemInHand].gameObject.transform.childCount; i++)
        {
            MeshRenderer ren = handObjects[currentItemInHand].gameObject.transform.GetChild(i).GetComponent<MeshRenderer>();
            if (ren)
            {
                ren.enabled = false;
            }
        }
        handObjects[itemNumber].enabled = true;
        for (int i = 0; i < handObjects[itemNumber].gameObject.transform.childCount; i++)
        {
            MeshRenderer ren = handObjects[itemNumber].gameObject.transform.GetChild(i).GetComponent<MeshRenderer>();
            if (ren)
            {
                ren.enabled = true;
            }
        }
        currentItemInHand = itemNumber;
        flashlight.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Grabbable")
        {
            readyForPickup = true;
            currentPickup = other.gameObject.GetComponent<InteractableTask>();
        }
        if(other.tag == "Task" && !currentlyInteracting)
        {
            readyToInteract = true;
            currentTask = other.gameObject.GetComponent<InteractableTask>();
        }
        if(other.tag == "Door")
        {
            currentDoor = other.gameObject.GetComponent<DoorScript>();
            readyForDoor = true;
        }
        if(other.tag == "Keys")
        {
            keys = other.gameObject;
            readyForKey = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Grabbable")
        {
            readyForPickup = false;
            currentPickup = null;
        }
        if (other.tag == "Task" && !currentlyInteracting)
        {
            readyToInteract = false;
            currentTask = null;
        }
        if (other.tag == "Door")
        {
            readyForDoor = false;
            currentDoor = null;
        }
        if (other.tag == "Keys")
        {
            readyForKey = false;
        }
    }

    void BeginInteracting()
    {
        player.canMove = false;
        //virtualCam.enabled = false;
        readyToInteract = false;
        taskDuration = currentTask.duration;
        currentlyInteracting = true;
        currentTask.anim.Play("TaskStart");
        if(currentTask.requiredItem != 0)
        {
            handObjects[currentItemInHand].enabled = false;
            for (int i = 0; i < handObjects[currentItemInHand].gameObject.transform.childCount; i++)
            {
                MeshRenderer ren = handObjects[currentItemInHand].gameObject.transform.GetChild(i).GetComponent<MeshRenderer>();
                if (ren)
                {
                    ren.enabled = false;
                }
            }
        }
    }

    public void StopInteracting()
    {
        // Either call from anim or call from coroutine after anim length
        //virtualCam.enabled = true;
        player.canMove = true;
        currentlyInteracting = false;
        currentTask = null;
        handObjects[currentItemInHand].enabled = true;
        for (int i = 0; i < handObjects[currentItemInHand].gameObject.transform.childCount; i++)
        {
            MeshRenderer ren = handObjects[currentItemInHand].gameObject.transform.GetChild(i).GetComponent<MeshRenderer>();
            if (ren)
            {
                ren.enabled = true;
            }
        }
        tManager.CompleteTask();
        //anim.Play("Main");
    }

    IEnumerator WaitForInteraction(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopInteracting();
    }

}
