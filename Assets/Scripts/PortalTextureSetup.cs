using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public List<Camera> cameras;
    public List<Material> materials;
    
    // Start is called before the first frame update
    void Start()
    {
        if(cameras.Count != materials.Count)
        {
            Debug.LogError("Cameras and material counts do not match!");
        }
        for(int i = 0; i < cameras.Count; i++)
        {
            Camera cam = cameras[i];
            Material mat = materials[i];
            if (cam.targetTexture != null)
            {
                cam.targetTexture.Release();
            }
            cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            mat.mainTexture = cam.targetTexture;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
