using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveAndFade : MonoBehaviour
{
    public float speed = 0.02f;

    Material m;

    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<MeshRenderer>().material;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeThenTurnOff()
    {
        gameObject.SetActive(true);
        m.SetFloat("Alpha", 1);
        StartCoroutine(Fade());
    }
    
    IEnumerator Fade()
    {
        while (m.GetFloat("Alpha") > 0)
        {
            float t = m.GetFloat("Alpha") - speed;
            m.SetFloat("Alpha", t);
            yield return null;
        }
        if(m.GetFloat("Alpha") < 0)
        {
            m.SetFloat("Alpha", 0);
        }
        gameObject.SetActive(false);
    }
}
