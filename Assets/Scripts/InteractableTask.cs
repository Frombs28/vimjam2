using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTask : MonoBehaviour
{
    public float duration = 5.0f;
    public int requiredItem = 0;
    public Animator anim;
    public int pickupNum = 2;
    public bool completed = false;
    // Start is called before the first frame update
    void Start()
    {
        completed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
