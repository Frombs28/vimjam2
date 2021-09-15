using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFPS : MonoBehaviour
{
    Camera cam;
    public float fps = 24;
    private float framerate = 0;
    // Start is called before the first frame update
    void Start()
    {
        framerate = 1/fps;
        cam = GetComponent<Camera>();
        cam.enabled = false;
        StartCoroutine("RenderCamera");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator RenderCamera()
    {
        while (true)
        {
            cam.Render();
            print(framerate);
            yield return new WaitForSeconds(framerate);
        }
    }
}
