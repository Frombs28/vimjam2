using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

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
            Debug.DrawRay(transform.position, portalToPlayer, Color.green);
            Debug.DrawRay(transform.position, transform.up, Color.red);
            if (angleBetween > 90.0f && primed)
            {
                // Teleport
                float rotDif = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotDif += 180.0f;
                player.Rotate(Vector3.up, rotDif);
                Vector3 posOffset = Quaternion.Euler(0.0f, rotDif, 0.0f) * portalToPlayer;
                Vector3 targetPos = receiver.position + posOffset;
                cc.enabled = false;
                player.position = new Vector3(targetPos.x, player.transform.position.y, targetPos.z);
                cc.enabled = true;
                playerIsHere = false;
                primed = false;
                Debug.Log("Teleport now!");
            }
            else
            {
                primed = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
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
}
