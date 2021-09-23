using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public int maxTension = 4;
    public List<float> tensionDistances;
    //public float normalMoveSpeed = 1.0f;
    //public float walkMoveSpeed = 0.5f;
    //public float slowMoveSpeed = 0.25f;
    public float maxPlayerHealth = 5.0f;
    public float damageMult = 1.0f;
    public Image injuredScreen;
    public List<float> speeds;


    private int tension;
    private float currentPlayerHealth;
    private MusicManager mm;
    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        mm = FindObjectOfType<MusicManager>();
        tension = 0;
        if(tensionDistances.Count - 1 != maxTension)
        {
            Debug.LogError("Wrong tension number for the number of tension distances!");
        }
        currentPlayerHealth = maxPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, player.transform.position);
        if(tension < 4 && currentDistance <= tensionDistances[tension + 1])
        {
            tension++;
            mm.UpdateTension(tension);
        }
        if(tension > 0 && currentDistance > tensionDistances[tension])
        {
            tension--;
            mm.UpdateTension(tension);
        }
        agent.speed = speeds[tension];
        switch (tension)
        {
            case 0:
                // Just move towards player; really far, needs to get closer
                agent.SetDestination(player.transform.position);
                if(currentPlayerHealth < maxPlayerHealth)
                {
                    currentPlayerHealth += (Time.deltaTime);
                }
                break;
            case 1:
                // somewhat close by; maybe walk the other way
                agent.SetDestination(player.transform.position);
                if(currentPlayerHealth < maxPlayerHealth)
                {
                    currentPlayerHealth += (Time.deltaTime * 0.5f);
                }
                break;
            case 2:
                // Probably in the next room over, start flickering light
                agent.SetDestination(player.transform.position);
                break;
            case 3:
                // in the same room, almost at kill distance, maybe start messing with camera if that's possible?
                agent.SetDestination(player.transform.position);
                break;
            case 4:
                // begin killing player; this is the highest tension is
                agent.SetDestination(player.transform.position);
                break;
            default:
                Debug.LogError("invalid tension value: " + tension);
                break;
        }
        if (inRange)
        {
            currentPlayerHealth -= (Time.deltaTime * damageMult);
            if (currentPlayerHealth <= 0.0f)
            {
                // Kill player if he spends enough time right next to ghost
                FindObjectOfType<ConditionManager>().Lose();
            }
        }

        Color newColor = injuredScreen.color;
        newColor.a = (maxPlayerHealth - currentPlayerHealth)/maxPlayerHealth;
        injuredScreen.color = newColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }

    public int GetTension()
    {
        return tension;
    }
}
