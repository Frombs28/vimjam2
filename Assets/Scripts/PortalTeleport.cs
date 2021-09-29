using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PortalTeleport : MonoBehaviour
{
    public Transform player;
    public Transform sender;
    public Transform receiver;
    public Transform cam;
    public Transform parentTransform;
    public Transform virtualCam;
    public DoorScript teleportedDoor = null;
    public GameObject portalObject = null;
    public float offset = 180.0f;
    //public CinemachineBrain cam;

    private bool playerIsHere = false;
    private CharacterController cc;
    bool primed = false;

    // Start is called before the first frame update
    void Start()
    {
        cc = player.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerIsHere)
        {
            Vector3 portalToPlayer = new Vector3(player.position.x, transform.position.y, player.position.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z);
            float angleBetween = Vector3.Angle(transform.up, portalToPlayer);
            if (angleBetween > 90.0f && primed)
            {
                // Teleport
                float rotDif = Vector3.Angle(sender.forward, parentTransform.forward);

                cc.enabled = false;
                player.transform.parent = sender;
                Vector3 local = player.localPosition;
                player.transform.parent = receiver;
                player.localPosition = local;
                player.transform.parent = null;
                cc.enabled = true;

                cam.parent = null;
                player.parent = null;
                virtualCam.parent = null;
                parentTransform.position = player.position;
                parentTransform.forward = receiver.forward;
                cam.parent = parentTransform;
                player.parent = parentTransform;
                virtualCam.parent = parentTransform;
                parentTransform.Rotate(Vector3.up, rotDif);
                
                playerIsHere = false;
                primed = false;
                StartCoroutine(WaitToEnable());

                if(teleportedDoor != null)
                {
                    teleportedDoor.WaitClose(player);
                }
                if(portalObject != null)
                {
                    portalObject.SetActive(false);
                }
            }
            else
            {
                primed = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsHere = true;
            primed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsHere = false;
        }
    }
    
    IEnumerator WaitToEnable()
    {
        yield return new WaitForSeconds(0.1f);
        //cam.parent = null;
        //player.parent = null;
        //virtualCam.parent = null;
    }
    

}
