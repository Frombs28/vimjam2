using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public int maxTension = 4;
    public List<float> tensionDistances;
    public float normalMoveSpeed = 1.0f;
    public float walkMoveSpeed = 0.5f;
    public float slowMoveSpeed = 0.25f;

    private int tension;
    // Start is called before the first frame update
    void Start()
    {
        tension = 0;
        if(tensionDistances.Count - 1 != maxTension)
        {
            Debug.LogError("Wrong tension number for the number of tension distances!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, player.transform.position);
        if(tension < 4 && currentDistance <= tensionDistances[tension + 1])
        {
            tension++;
        }
        if(tension > 0 && currentDistance > tensionDistances[tension])
        {
            tension--;
        }
        switch (tension)
        {
            case 0:
                // Just move towards player
                agent.SetDestination(player.transform.position);
                agent.speed = normalMoveSpeed;
                break;
            case 1:
                // start messing with radio
                agent.SetDestination(player.transform.position);
                agent.speed = normalMoveSpeed;
                break;
            case 2:
                // start flickering light
                agent.SetDestination(player.transform.position);
                agent.speed = walkMoveSpeed;
                break;
            case 3:
                // make something appear, mess with camera
                agent.SetDestination(player.transform.position);
                agent.speed = walkMoveSpeed;
                break;
            case 4:
                // begin killing player
                agent.SetDestination(player.transform.position);
                agent.speed = slowMoveSpeed;
                break;
            default:
                Debug.LogError("invalid tension value: " + tension);
                break;
        }
        Debug.Log("Tension is: " + tension);
    }
}
