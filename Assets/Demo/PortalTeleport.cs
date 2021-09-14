using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool playerIsHere = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsHere)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dot = Vector3.Dot(transform.up, portalToPlayer);
            Debug.Log("Dot product: " + dot);
            if(dot < 0)
            {
                // Teleport
                float rotDif = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotDif += 180.0f;
                player.Rotate(Vector3.up, rotDif);
                Vector3 posOffset = Quaternion.Euler(0.0f, rotDif, 0.0f) * portalToPlayer;
                player.position = receiver.position + posOffset;
                playerIsHere = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIsHere = true;
            Debug.Log("Hit!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsHere = false;
            Debug.Log("X");
        }
    }
}
