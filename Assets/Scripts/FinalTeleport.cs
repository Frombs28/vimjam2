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
    private MonsterScript monster;

    public SphereCollider hurtbox;
    public GameObject lightDetector;

    // Start is called before the first frame update
    void Start()
    {
        monster = FindObjectOfType<MonsterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!finished && other.tag == "Player")
        {
            finalDoor.Close();
            otherDoor.Close();
            cc.enabled = false;
            other.gameObject.transform.position = location.transform.position;
            cc.enabled = true;
            finished = true;
            finalDoor.portalCamera = null;
            hurtbox.enabled = true;
            lightDetector.SetActive(true);
            monster.InLight();
            gameObject.SetActive(false);
        }
    }
}
