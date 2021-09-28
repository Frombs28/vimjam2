using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCam : MonoBehaviour
{
    public Transform playerCam;
    public Transform myPortal;
    public Transform otherPortal;

    private Light lit;
    private Camera cam;
    private bool activated;
    private PlayerInteract player;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = myPortal;
        lit = GetComponentInChildren<Light>();
        cam = GetComponent<Camera>();
        player = FindObjectOfType<PlayerInteract>();
        lit.enabled = false;
        cam.enabled = false;
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        // Correct code: useful for any angle
        Vector3 playerOffsetFromOtherPortal = playerCam.position - otherPortal.position;
        transform.localPosition = playerOffsetFromOtherPortal;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Offset: " + playerOffsetFromOtherPortal);
        }
        //float angleDifBetweenPortalRot = Quaternion.Angle(myPortal.rotation, otherPortal.rotation);
        //Quaternion portalRotationalDifference = Quaternion.AngleAxis(angleDifBetweenPortalRot, Vector3.up);
        //Vector3 newCamDirection = portalRotationalDifference * playerCam.forward;
        //transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.up);
        //transform.eulerAngles = new Vector3(transform.rotation.x,transform.rotation.y+offset,transform.rotation.z);
        Vector3 rotOffset = playerCam.eulerAngles - otherPortal.eulerAngles;
        transform.localEulerAngles = rotOffset;


        // Code for more instance
        //Vector3 playerOffsetFromOtherPortal = playerCam.position - otherPortal.position;
        //transform.position = myPortal.position + playerOffsetFromOtherPortal;
        //transform.rotation = playerCam.rotation;

        if (activated)
        {
            lit.enabled = player.flashlight.enabled;
        }
    }

    public void Activate()
    {
        cam.enabled = true;
        activated = true;
    }

    public void Deactivate()
    {
        cam.enabled = false;
        lit.enabled = false;
        activated = false;
    }
}
