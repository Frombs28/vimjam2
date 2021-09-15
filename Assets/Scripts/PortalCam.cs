using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCam : MonoBehaviour
{
    public Transform playerCam;
    public Transform myPortal;
    public Transform otherPortal;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Correct code: useful for any angle
        Vector3 playerOffsetFromOtherPortal = playerCam.position - otherPortal.position;
        transform.position = myPortal.position + playerOffsetFromOtherPortal;

        float angleDifBetweenPortalRot = Quaternion.Angle(myPortal.rotation, otherPortal.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angleDifBetweenPortalRot, Vector3.up);
        Vector3 newCamDirection = portalRotationalDifference * playerCam.forward;
        transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.up);
        */

        // Code for more instance
        Vector3 playerOffsetFromOtherPortal = playerCam.position - otherPortal.position;
        transform.position = myPortal.position + playerOffsetFromOtherPortal;
        transform.rotation = playerCam.rotation;
    }
}
