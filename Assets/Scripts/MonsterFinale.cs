using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFinale : MonoBehaviour
{
    public BoxCollider thisBox;
    public SphereCollider hurtbox;
    public GameObject lightDetector;


    private bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!done && other.tag == "Player")
        {
            // Remove hurtbox and light detector
            done = true;
            thisBox.enabled = false;
            hurtbox.enabled = false;
            lightDetector.SetActive(false);
        }
    }
}
