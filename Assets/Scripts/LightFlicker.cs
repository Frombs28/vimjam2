using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float numberOfSecondsBetweenEpisodes = 5.0f;
    public float minNumberOfSecondsBetweenFlicks = 0.1f;
    public float maxNumberOfSecondsBetweenFlicks = 0.3f;
    public MeshRenderer rend;
    public Light trueLight;
    public Material lightOn;
    public Material lightOff;
    public bool beginOnStart = false;
    public bool loop = false;

    private float trueNumBetweenEps;
    private int numberOfFlicks;
    private float timeBetweenFlicks;
    // Start is called before the first frame update
    void Start()
    {
        if (beginOnStart)
        {
            PrepFlick();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrepFlick()
    {
        trueNumBetweenEps = Random.Range(numberOfSecondsBetweenEpisodes / 1.5f, numberOfSecondsBetweenEpisodes * 1.5f);
        Invoke("BeginFlicking", trueNumBetweenEps);
        Debug.Log("Prepped for " + trueNumBetweenEps + " seconds.");
    }

    void BeginFlicking()
    {
        numberOfFlicks = Random.Range(3, 9);
        StartCoroutine(Flicker(numberOfFlicks));
        Debug.Log("Now do " + numberOfFlicks + " flickers");
    }

    IEnumerator Flicker(int numberOfTimes)
    {
        for(int i = 0; i < numberOfTimes; i++)
        {
            rend.material = lightOff;
            trueLight.enabled = false;
            yield return new WaitForSeconds(Random.Range(minNumberOfSecondsBetweenFlicks, maxNumberOfSecondsBetweenFlicks));
            rend.material = lightOn;
            trueLight.enabled = true;
            yield return new WaitForSeconds(Random.Range(minNumberOfSecondsBetweenFlicks, maxNumberOfSecondsBetweenFlicks));
        }
        if (loop)
        {
            PrepFlick();
        }
    }

    public void FlickerLight()
    {
        PrepFlick();
    }

}
