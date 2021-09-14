using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCookie : MonoBehaviour
{
    public float distance = 100;
    public float minRadius = 1f;
    public float maxRadius = 20f;
    public GameObject lightSprite;

    private Color c;
    private Color noAlpha;
    private SpriteRenderer lightRend;
    // Start is called before the first frame update
    void Start()
    {
        lightRend = lightSprite.GetComponent<SpriteRenderer>();
        c = lightRend.color;
        noAlpha = new Color(c.r, c.g, c.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            lightSprite.SetActive(true);
            lightSprite.transform.position = hit.point + hit.normal * 0.01f;
            lightSprite.transform.rotation = Quaternion.LookRotation(hit.normal);
            lightSprite.transform.localScale = Vector3.one * Mathf.Lerp(minRadius, maxRadius, (hit.distance / distance));
            lightRend.color = Color.Lerp(c, noAlpha, (hit.distance / distance));
        }
        else
        {
            lightSprite.SetActive(false);
        }
    }
}
