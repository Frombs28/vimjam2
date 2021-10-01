using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public Transform spot;
    public float time = 0.0f;
    public int numTimes = 1;

    private MonsterScript monster;
    private BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        monster = FindObjectOfType<MonsterScript>();
        box = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            // Begin waiting to Teleport
            StartCoroutine(TeleportDelay());
        }
    }

    IEnumerator TeleportDelay()
    {
        yield return new WaitForSeconds(time);
        Teleport();
    }

    void Teleport()
    {
        if (!monster.gameObject.activeSelf)
        {
            return;
        }
        monster.agent.enabled = false;
        monster.transform.position = spot.position;
        monster.transform.forward = spot.forward;
        monster.agent.enabled = true;
        if(numTimes != 99)
        {
            numTimes--;
        }
        if(numTimes <= 0 && numTimes != 99)
        {
            box.enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
