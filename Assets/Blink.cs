using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public float speed = 1;

    private Image i;
    private Color c;
    private Color a;
    // Start is called before the first frame update
    void Start()
    {
        i = GetComponent<Image>();
        c = i.color;
        a = new Color(0, 0, 0, 0);
        InvokeRepeating("BlinkColor", speed, speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BlinkColor()
    {
        if(i.color == c)
        {
            i.color = a;
        }
        else
        {
            i.color = c;
        }
    }
}
