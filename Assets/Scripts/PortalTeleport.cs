using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PortalTeleport : MonoBehaviour
{
    public Transform player;
    public Transform receiver;
    public Transform cam;
    public Transform parentTransform;
    public Transform virtualCam;
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
                float rotDif = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotDif += 180.0f;
                Debug.Log("Rotate y axis " + player.transform.eulerAngles.y + " by " + rotDif + " to get " + (player.transform.eulerAngles.y + rotDif));
                //cam.enabled = false;
                //cam.transform.parent = player.transform;
                //parentTransform.position = 

                cc.enabled = false;
                player.transform.parent = this.transform.parent;
                Vector3 local = player.localPosition;
                player.transform.parent = receiver;
                player.localPosition = local;
                player.transform.parent = null;
                cc.enabled = true;

                cam.parent = null;
                player.parent = null;
                virtualCam.parent = null;
                parentTransform.position = player.position;
                cam.parent = parentTransform;
                player.parent = parentTransform;
                virtualCam.parent = parentTransform;
                parentTransform.Rotate(Vector3.up, rotDif);

                //player.Rotate(Vector3.up, rotDif);
                //Vector3 posOffset = Quaternion.Euler(0.0f, rotDif, 0.0f) * portalToPlayer;
                //Vector3 targetPos = receiver.position + posOffset;
                
                playerIsHere = false;
                primed = false;
                Debug.Log("Teleport now!");
                StartCoroutine(WaitToEnable());
                //cam.transform.parent = null;
                //cam.enabled = true;
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
