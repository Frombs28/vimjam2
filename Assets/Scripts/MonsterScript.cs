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
    public LightFlicker flashLightFlicker;
    public List<Transform> teleportLocations;


    private int tension;
    [SerializeField]
    private float currentPlayerHealth;
    private MusicManager mm;
    private bool inRange = false;
    private Light light;
    private float originalIntensity;
    private bool teleporting = false;
    private int lastRandom = -1;
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
        light = flashLightFlicker.trueLight;
        originalIntensity = light.intensity;
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
                if (flashLightFlicker.enabled)
                {
                    flashLightFlicker.enabled = false;
                }
                if(light.intensity != originalIntensity)
                {
                    light.intensity = originalIntensity;
                }
                agent.SetDestination(player.transform.position);
                break;
            case 1:
                // somewhat close by; maybe walk the other way
                if (flashLightFlicker.enabled)
                {
                    flashLightFlicker.enabled = false;
                }
                if (light.intensity != originalIntensity)
                {
                    light.intensity = originalIntensity;
                }
                agent.SetDestination(player.transform.position);
                break;
            case 2:
                // Probably in the next room over, start flickering light
                flashLightFlicker.numberOfSecondsBetweenEpisodes = 0.75f;
                if (!flashLightFlicker.enabled)
                {
                    flashLightFlicker.enabled = true;
                    flashLightFlicker.FlickerLight();

                }
                agent.SetDestination(player.transform.position);
                break;
            case 3:
                // in the same room, almost at kill distance, maybe start messing with camera if that's possible?
                flashLightFlicker.numberOfSecondsBetweenEpisodes = 0.35f;
                if (!flashLightFlicker.enabled)
                {
                    flashLightFlicker.enabled = true;
                    flashLightFlicker.FlickerLight();

                }
                agent.SetDestination(player.transform.position);
                break;
            case 4:
                // begin killing player; this is the highest tension is
                flashLightFlicker.numberOfSecondsBetweenEpisodes = 0.125f;
                if (!flashLightFlicker.enabled)
                {
                    flashLightFlicker.enabled = true;
                    flashLightFlicker.FlickerLight();

                }
                agent.SetDestination(player.transform.position);
                break;
            default:
                Debug.LogError("invalid tension value: " + tension);
                break;
        }
        bool dealDamage = false;
        if (inRange)
        {
            // raycast to make sure line of sight is active
        }

        if (dealDamage)
        {
            currentPlayerHealth -= (Time.deltaTime * damageMult);
            if (currentPlayerHealth <= 0.0f)
            {
                // Kill player if he spends enough time right next to ghost
                FindObjectOfType<ConditionManager>().Lose();
            }
        }
        else
        {
            if (currentPlayerHealth < maxPlayerHealth)
            {
                currentPlayerHealth += (Time.deltaTime);
                if (currentPlayerHealth > maxPlayerHealth)
                {
                    currentPlayerHealth = maxPlayerHealth;
                }
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

    public void InLight()
    {
        // either teleport away or more around the edge. try just teleporting for now
        if(!teleporting && teleportLocations.Count > 0)
        {
            teleporting = true;
            StartCoroutine(WaitToTeleport());
        }
    }

    IEnumerator WaitToTeleport()
    {
        //Wait a little
        yield return new WaitForSeconds(0.3f);
        // Not the sameplace twice in a row
        int index = Random.Range(0, teleportLocations.Count);
        while(index == lastRandom)
        {
            index = Random.Range(0, teleportLocations.Count);
        }
        //Teleport
        Vector3 newPos = teleportLocations[index].position;
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
        teleporting = false;
    }
}
