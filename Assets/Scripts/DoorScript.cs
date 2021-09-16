using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator anim;
    public BoxCollider box;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor(Vector3 playerPos)
    {
        Vector3 doorToPlayer = new Vector3(playerPos.x, transform.position.y, playerPos.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float angleBetween = Vector3.Angle(transform.forward, doorToPlayer);
        if (angleBetween > 90.0f)
        {
            // Open "forwards" (left door knob swings back on single doors)
            anim.SetBool("OpenFor", true);
        }
        else
        {
            // Open "backwards" (right door knob swings back on single doors)
            anim.SetBool("OpenBack", true);
        }
        box.enabled = false;
        StartCoroutine(WaitToClose());
    }

    IEnumerator WaitToClose()
    {
        yield return new WaitForSeconds(3.0f);
        anim.SetBool("OpenBack", false);
        anim.SetBool("OpenFor", false);
        box.enabled = true;
    }
}
