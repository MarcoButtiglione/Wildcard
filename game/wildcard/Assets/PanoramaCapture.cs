using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanoramaCapture : MonoBehaviour
{
    public Camera targetCamera;
    public RenderTexture cubeMapLeft;
    public RenderTexture equirectRT;
    public string sceneName;
    private bool first = true;


    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Photo");
    }

    public void Capture()
    {
        targetCamera.RenderToCubemap(cubeMapLeft);
        cubeMapLeft.ConvertToEquirect(equirectRT);
        Save(equirectRT);
    }
    public void Save(RenderTexture rt)
    {
        Texture2D tex = new Texture2D(rt.width, rt.height);

        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        RenderTexture.active = null;

        byte[] bytes = tex.EncodeToPNG();
        string path = Application.dataPath + "/Panorama" + sceneName + ".png";
        Debug.Log("Writing in "+path);
        System.IO.File.WriteAllBytes(path, bytes);
    }

    IEnumerator Photo(){
        yield return new WaitForSecondsRealtime(1);
        if (first)
        {
            Capture();
            first = false;
        }

    }
}
