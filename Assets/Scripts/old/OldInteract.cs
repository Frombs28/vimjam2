using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldInteract : MonoBehaviour
{
    //public PlayerMovement player;
    ////public Cinemachine.CinemachineVirtualCamera virtualCam;
    //public GameObject hand;
    //public Camera mainCamera;
    //public Animator anim;
    //public GameObject radio;
    //public GameObject flashLight;
    //public GameObject lightBulb;
    //public GameObject plant;

    //private bool readyForDoor = false;
    //private bool readyToInteract = false;
    //private bool currentlyInteracting = false;
    //private bool holdingSomething = false;
    //private GameObject objectBeingHeld;
    //private InteractableTask currentTask;
    //private DoorScript currentDoor;
    //private Coroutine taskCoroutine;
    //private float taskDuration;
    //private bool hasRadio = true;
    //private bool hasFlashlight = true;
    //private bool hasLightbulbs = false;
    //private bool hasPlants = false;
    //[SerializeField]
    //private List<GameObject> objectsInRange;
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (currentlyInteracting)
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            currentTask.anim.SetBool("DoTask", true);
    //            taskCoroutine = StartCoroutine(WaitForInteraction(taskDuration));
    //        }
    //        else if (Input.GetMouseButtonUp(0))
    //        {
    //            currentTask.anim.SetBool("DoTask", false);
    //            StopCoroutine(taskCoroutine);
    //        }
    //        return;
    //    }
    //    if (readyToInteract && Input.GetKeyDown(KeyCode.E) && (currentTask.requiredItem == "" || (objectBeingHeld != null && currentTask.requiredItem == objectBeingHeld.name)))
    //    {
    //        // Interact; lock player movement, begin interactable animation.
    //        BeginInteracting();
    //    }
    //    if (readyForDoor && Input.GetKeyDown(KeyCode.E))
    //    {
    //        // Open door
    //        currentDoor.OpenDoor(player.transform.position);
    //    }
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (holdingSomething)
    //        {
    //            Drop(false);
    //        }
    //        else
    //        {
    //            PickUp();
    //        }
    //    }
    //}

    //void PickUp()
    //{
    //    if (objectsInRange.Count == 0)
    //    {
    //        return;
    //    }
    //    float minDistance = 10.0f;
    //    Vector3 center = new Vector3(0.5f, 0.5f, 0.0f);
    //    foreach (GameObject thing in objectsInRange)
    //    {
    //        Vector3 viewPos = mainCamera.WorldToViewportPoint(thing.transform.position);
    //        float distance = Vector3.Distance(viewPos, center);
    //        if (distance < minDistance)
    //        {
    //            minDistance = distance;
    //            //Debug.Log("Distance of " + thing.name + " : " + distance);
    //            objectBeingHeld = thing;
    //        }
    //    }
    //    holdingSomething = true;
    //    hand.GetComponent<MeshRenderer>().enabled = false;
    //    objectBeingHeld.transform.position = hand.transform.position;
    //    objectBeingHeld.transform.rotation = hand.transform.rotation;
    //    objectBeingHeld.transform.parent = hand.transform;
    //    objectBeingHeld.layer = 9;
    //    objectBeingHeld.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //}

    //void Drop(bool deleteItem)
    //{
    //    holdingSomething = false;
    //    objectBeingHeld.transform.parent = null;
    //    objectBeingHeld.layer = 11;
    //    objectBeingHeld.transform.eulerAngles = Vector3.zero;
    //    objectBeingHeld.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    //    if (deleteItem)
    //    {
    //        objectBeingHeld.SetActive(false);
    //    }
    //    objectBeingHeld = null;
    //    hand.GetComponent<MeshRenderer>().enabled = true;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.tag);
    //    if (other.tag == "Grabbable")
    //    {
    //        if (!objectsInRange.Contains(other.gameObject))
    //        {
    //            objectsInRange.Add(other.gameObject);
    //        }
    //    }
    //    if (other.tag == "Task" && !currentlyInteracting)
    //    {
    //        readyToInteract = true;
    //        currentTask = other.gameObject.GetComponent<InteractableTask>();
    //    }
    //    if (other.tag == "Door")
    //    {
    //        currentDoor = other.gameObject.GetComponent<DoorScript>();
    //        readyForDoor = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Grabbable")
    //    {
    //        objectsInRange.Remove(other.gameObject);
    //    }
    //    if (other.tag == "Task" && !currentlyInteracting)
    //    {
    //        readyToInteract = false;
    //        currentTask = null;
    //    }
    //    if (other.tag == "Door")
    //    {
    //        readyForDoor = false;
    //        currentDoor = null;
    //    }
    //}

    //void BeginInteracting()
    //{
    //    player.canMove = false;
    //    //virtualCam.enabled = false;
    //    readyToInteract = false;
    //    taskDuration = currentTask.duration;
    //    currentlyInteracting = true;
    //    currentTask.anim.Play("TaskStart");
    //    if (currentTask.requiredItem != "")
    //    {
    //        Drop(true);
    //    }
    //    //anim.Play(animName);
    //    //StartCoroutine(WaitForInteraction(duration));
    //}

    //public void StopInteracting()
    //{
    //    // Either call from anim or call from coroutine after anim length
    //    //virtualCam.enabled = true;
    //    player.canMove = true;
    //    currentlyInteracting = false;
    //    currentTask = null;
    //    //anim.Play("Main");
    //}

    //IEnumerator WaitForInteraction(float duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //    StopInteracting();
    //}

}