using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TensionRanodmizer : MonoBehaviour
{
    public float waitTime = 15.0f;

    private MonsterScript monster;
    
    // Start is called before the first frame update
    void Start()
    {
        monster = FindObjectOfType<MonsterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitToRandomize(float time)
    {
        yield return new WaitForSeconds(time);
        // Set timer for 20 seconds for next time
        float nextTime = waitTime;
        // 20% chance to spike tension
        float percent = Random.Range(1, 101);
        if(percent <= 20)
        {
            // randomize, set timer for 35 seconds from now
            monster.RanomizeTension();
            nextTime += 15.0f;
        }
        Randomize(nextTime);
    }

    public void Randomize(float time)
    {
        StartCoroutine(WaitToRandomize(time));
    }
}
