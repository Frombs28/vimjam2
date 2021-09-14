using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string inputSound;
    public float walkSpeed = 1f;

    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlayFootsteps", 0, walkSpeed);
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

    private void PlayFootsteps()
    {
        if (isMoving)
        {
            FMODUnity.RuntimeManager.PlayOneShot(inputSound, transform.position);
        }
    }

    private void OnDisable()
    {
        isMoving = false;
    }
}
