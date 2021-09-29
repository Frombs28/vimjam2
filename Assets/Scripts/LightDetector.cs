using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    public MonsterScript monster;
    public GameObject player;
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit, Mathf.Infinity, mask))
        {
            if(hit.transform.gameObject.tag == "Player")
            {
                monster.InLight();
            }
            else
            {
                // Do nothing; wall in the way
                Debug.Log("Hit: " + hit.transform.gameObject.name);
            }
        }
        else
        {
            Debug.LogError("Hit nothing??");
        }
    }
}
