using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public PlayerMovement player;
    //public Cinemachine.CinemachineVirtualCamera virtualCam;
    public GameObject hand;
    public Camera mainCamera;
    public Animator anim;
    public Slider taskProgress;
    public Slider taskProgress2;
    public Image taskOutline;
    public Light flashlight;
    public List<MeshRenderer> handObjects;
    public Text textBox;
    // hand = 0, dont mess with this
    // radio = 1
    // flashlight = 2
    // keys = 3
    // lightbulb = 4
    // plant = 5
    [FMODUnity.EventRef]
    public string flashlightTrack;
    [FMODUnity.EventRef]
    public string lightbulbScrewing;
    [FMODUnity.EventRef]
    public string plantSetup;
    [HideInInspector]
    public FMOD.Studio.EventInstance trackInstance;

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
    private RadioManager radManager;
    // Start is called before the first frame update
    void Start()
    {
        handObjects[0].enabled = false;
        handObjects[1].enabled = true;
        handObjects[2].enabled = false;
        handObjects[3].enabled = false;
        handObjects[4].enabled = false;
        currentItemInHand = 1;
        flashlight.enabled = false;
        tManager = FindObjectOfType<TaskManager>();
        radManager = FindObjectOfType<RadioManager>();
        taskProgress.value = 0.0f;
        taskProgress2.value = 0.0f;
        taskProgress.enabled = false;
        taskProgress2.enabled = false;
        taskOutline.enabled = false;
        textBox.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyInteracting)
        {
            if (Input.GetMouseButton(0))
            {
                taskProgress.value += Time.deltaTime;
                taskProgress2.value = taskProgress.value;
                
            }
            if (Input.GetMouseButtonDown(0))
            {
                currentTask.anim.SetBool("DoTask", true);
                taskCoroutine = StartCoroutine(WaitForInteraction(taskDuration));
                if (currentTask.requiredItem == 3)
                {
                    // lightbulb
                    //FMODUnity.RuntimeManager.PlayOneShot(lightbulbScrewing, currentTask.transform.position);
                    trackInstance = FMODUnity.RuntimeManager.CreateInstance(lightbulbScrewing);
                    trackInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(currentTask.transform.position));
                    trackInstance.start();
                }
                else if (currentTask.requiredItem == 4)
                {
                    // plant
                    //FMODUnity.RuntimeManager.PlayOneShot(plantSetup, currentTask.transform.position);
                    trackInstance = FMODUnity.RuntimeManager.CreateInstance(plantSetup);
                    trackInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(currentTask.transform.position));
                    trackInstance.start();
                }
                else
                {
                    Debug.LogError("Non existant task!");
                }
            }
            else if(Input.GetMouseButtonUp(0))
            {
                currentTask.anim.SetBool("DoTask", false);
                StopCoroutine(taskCoroutine);
                taskProgress.value = 0.0f;
                taskProgress2.value = taskProgress.value;
                //Stop playing sound
                trackInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                trackInstance.release();
            }
            if (Input.GetMouseButtonDown(1))
            {
                currentTask.anim.SetBool("DoTask", false);
                StopCoroutine(taskCoroutine);
                currentTask.anim.Play("Idle");
                StopInteracting(false);
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
            textBox.text = "";
        }
        if (readyForPickup && Input.GetKeyDown(KeyCode.E))
        {
            // Add that pickup to our array
            PickUp();
        }
        if (readyForKey && Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<LockedDoorManager>().UnlockAllDoors();
            keys.SetActive(false);
            Debug.Log("Got key!");
        }

        if(currentItemInHand == 2 && Input.GetMouseButtonDown(1))
        {
            flashlight.enabled = !flashlight.enabled;
            FMODUnity.RuntimeManager.PlayOneShot(flashlightTrack, transform.position);
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
        textBox.text = "";
    }

    void Equip(int itemNumber)
    {
        if(itemNumber == currentItemInHand)
        {
            return;
        }
        handObjects[currentItemInHand].enabled = false;
        handObjects[itemNumber].enabled = true;
        currentItemInHand = itemNumber;
        flashlight.enabled = false;
        radManager.RadioActive(itemNumber == 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Grabbable")
        {
            readyForPickup = true;
            currentPickup = other.gameObject.GetComponent<InteractableTask>();
            textBox.text = "Press E to pickup " + other.name;
        }
        if(other.tag == "Task" && !currentlyInteracting)
        {
            readyToInteract = true;
            currentTask = other.gameObject.GetComponent<InteractableTask>();
            if (!currentTask.completed && (currentTask.requiredItem == 0 || (currentTask.requiredItem == currentItemInHand)))
            {
                textBox.text = "Press E";
            }
        }
        if(other.tag == "Door")
        {
            currentDoor = other.gameObject.GetComponent<DoorScript>();
            readyForDoor = true;
            if (!currentDoor.locked)
            {
                textBox.text = "Press E to open";
            }
            else
            {
                textBox.text = "Locked...";
            }
        }
        if(other.tag == "Key")
        {
            keys = other.gameObject;
            readyForKey = true;
            textBox.text = "Press E to pickup keys";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Grabbable")
        {
            readyForPickup = false;
            currentPickup = null;
            textBox.text = "";
        }
        if (other.tag == "Task" && !currentlyInteracting)
        {
            readyToInteract = false;
            currentTask = null;
            textBox.text = "";
        }
        if (other.tag == "Door")
        {
            readyForDoor = false;
            currentDoor = null;
            textBox.text = "";
        }
        if (other.tag == "Keys")
        {
            readyForKey = false;
            textBox.text = "";
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
        }
        taskOutline.enabled = true;
        textBox.text = "Hold Left Click to complete task\nRight Click to stop";
    }

    public void StopInteracting(bool finished)
    {
        // Either call from anim or call from coroutine after anim length
        //virtualCam.enabled = true;
        //Stop playing sound
        trackInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        trackInstance.release();
        player.canMove = true;
        currentlyInteracting = false;
        currentTask.completed = finished;
        currentTask = null;
        handObjects[currentItemInHand].enabled = true;
        if (finished)
        {
            tManager.CompleteTask();
            textBox.text = "";
        }
        else
        {
            textBox.text = "Press E";
        }
        taskOutline.enabled = false;
        taskProgress.enabled = false;
        taskProgress2.enabled = false;
        taskProgress.value = 0.0f;
        taskProgress2.value = 0.0f;
        //anim.Play("Main");
    }

    IEnumerator WaitForInteraction(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopInteracting(true);
    }

    public int GetCurrentItem()
    {
        return currentItemInHand;
    }

}
