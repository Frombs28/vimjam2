using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTeleport : MonoBehaviour
{
    public Transform location;
    public DoorScript finalDoor;
    public DoorScript otherDoor;
    public CharacterController cc;

    private bool finished = false;
    private GameObject monster;

    // Start is called before the first frame update
    void Start()
    {
        monster = FindObjectOfType<MonsterScript>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit the pool!");
        if (!finished && other.tag == "Player")
        {
            finalDoor.Close();
            otherDoor.Close();
            Debug.Log("Now shmoving!");
            cc.enabled = false;
            other.gameObject.transform.position = location.transform.position;
            cc.enabled = true;
            finished = true;
            otherDoor.locked = true;
            finalDoor.portalCamera = null;
            monster.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
