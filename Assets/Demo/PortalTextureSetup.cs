using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera camera_two;
    public Material cameraMat_two;
    
    // Start is called before the first frame update
    void Start()
    {
        if(camera_two.targetTexture != null)
        {
            camera_two.targetTexture.Release();
        }
        camera_two.targetTexture = new RenderTexture(Screen.width,Screen.height,24);
        cameraMat_two.mainTexture = camera_two.targetTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
