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
    public GameObject monster;
    public List<GameObject> uiImages;
    public List<GameObject> uiX;
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
    private bool currentLightState = true;
    private TensionRanodmizer tensionRando;
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
        monster.SetActive(false);
        
        uiImages[0].SetActive(true);
        uiImages[1].SetActive(true);
        uiImages[2].SetActive(false);
        uiImages[3].SetActive(false);
        
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
            else
            {
                currentTask.anim.SetBool("DoTask", false);
                StopCoroutine(taskCoroutine);
                currentTask.anim.Play("Idle");
                StopInteracting(false);
            }
            return;
        }
        if (readyToInteract && Input.GetMouseButtonDown(0) && !currentTask.completed && (currentTask.requiredItem == 0 || (currentTask.requiredItem == currentItemInHand)))
        {
            // Interact; lock player movement, begin interactable animation.
            BeginInteracting();
        }
        if(readyForDoor && Input.GetKeyDown(KeyCode.E) && !currentDoor.locked)
        {
            // Open door
            currentDoor.OpenDoor(player.transform.position,player.transform);
            textBox.text = "";
            currentDoor = null;
            readyForDoor = false;
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
            radManager.getKeys();
            tManager.StepUp(1);
            readyForKey = false;
            textBox.text = "";
        }

        if (currentItemInHand == 2 && Input.GetMouseButtonDown(1))
        {
            flashlight.enabled = !flashlight.enabled;
            currentLightState = flashlight.enabled;
            FMODUnity.RuntimeManager.PlayOneShot(flashlightTrack, transform.position);
        }

        if (hasRadio && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(1);
        }
        if (hasFlashlight && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Equip(2);
        }
        if (hasLightbulbs && hasPlants && Input.GetKeyDown(KeyCode.Alpha3))
        {
            Equip(3);
        }
        if (hasPlants && hasLightbulbs && Input.GetKeyDown(KeyCode.Alpha4))
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
        uiImages[itemNum - 1].SetActive(true);
        uiX[itemNum - 3].SetActive(true);
        Equip(itemNum);
        currentPickup.gameObject.SetActive(false);
        currentPickup = null;
        readyForPickup = false;
        textBox.text = "";
        if(hasLightbulbs && hasPlants)
        {
            // Truly begin the game!!
            radManager.getItems();
            tManager.StepUp(2);
            monster.SetActive(true);
            tensionRando.Randomize(tensionRando.waitTime + 20.0f);
            foreach(GameObject x in uiX)
            {
                x.SetActive(false);
            }
        }
    }

    void Equip(int itemNumber)
    {
        if(itemNumber == currentItemInHand)
        {
            return;
        }
        if(itemNumber == 2)
        {
            flashlight.enabled = currentLightState;
            if (flashlight.enabled)
            {
                FMODUnity.RuntimeManager.PlayOneShot(flashlightTrack, transform.position);
            }
        }
        else if(currentItemInHand == 2)
        {
            currentLightState = flashlight.enabled;
            flashlight.enabled = false;
            FMODUnity.RuntimeManager.PlayOneShot(flashlightTrack, transform.position);
        }
        handObjects[currentItemInHand].enabled = false;
        handObjects[itemNumber].enabled = true;
        currentItemInHand = itemNumber;
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
                textBox.text = "Hold Left Click to complete task";
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

    private void OnTriggerStay(Collider other)
    {
        // Maybe have text for tasks display here instead?
        //if (other.tag == "Task" && !currentlyInteracting)
        //{
        //    readyToInteract = true;
        //    currentTask = other.gameObject.GetComponent<InteractableTask>();
        //    if (!currentTask.completed && (currentTask.requiredItem == 0 || (currentTask.requiredItem == currentItemInHand)))
        //    {
        //        textBox.text = "Hold Left Click to complete task";
        //    }
        //}
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
        currentTask.anim.SetBool("DoTask", true);
        taskCoroutine = StartCoroutine(WaitForInteraction(taskDuration));
        if (currentTask.requiredItem == 3)
        {
            // lightbulb
            //FMODUnity.RuntimeManager.PlayOneShot(lightbulbScrewing, currentTask.transform.position);
            trackInstance = FMODUnity.RuntimeManager.CreateInstance(lightbulbScrewing);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(trackInstance, currentTask.transform);
            trackInstance.start();
        }
        else if (currentTask.requiredItem == 4)
        {
            // plant
            //FMODUnity.RuntimeManager.PlayOneShot(plantSetup, currentTask.transform.position);
            trackInstance = FMODUnity.RuntimeManager.CreateInstance(plantSetup);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(trackInstance, currentTask.transform);
            trackInstance.start();
        }
        else
        {
            Debug.LogError("Non existant task!");
        }
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
            textBox.text = "Hold Left Click to complete task";
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
