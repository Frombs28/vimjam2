using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string inputSound;
    public float walkSpeed = 1f;
    public float runSpeed = 0.3f;

    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayFootsteps());
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    IEnumerator PlayFootsteps()
    {
        while (true)
        {
            if (isMoving)
            {
                FMODUnity.RuntimeManager.PlayOneShot(inputSound, transform.position);
            }
            if (Input.GetKey(KeyCode.LeftShift)) yield return new WaitForSeconds(runSpeed);
            else yield return new WaitForSeconds(walkSpeed);
        }
    }

    private void OnDisable()
    {
        isMoving = false;
    }
}
