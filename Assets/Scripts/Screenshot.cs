using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screenshot : MonoBehaviour
{
    public GameObject ramka;
    public RectTransform preview;
    public GameObject[] screenplayGO;
    public RectTransform actors;
    RectTransform rawImage;
    public GameObject logo;
    public int photoCount;
    // Start is called before the first frame update
    void Start()
    {
        ramka.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Texture2D scr;
    public bool grab = false;

    public void CloseLogo() {
        logo.SetActive(false);
    }

    public void TakeScreen()
    {
        logo.SetActive(true);
        grab = true;
    }
    private void OnPostRender()
    {
        if (grab)
        {
            int width = 400;
            int height = 800;
            
            RenderTexture activeRenderTexture = RenderTexture.active;
            RenderTexture.active = GetComponent<Camera>().activeTexture;

            scr = new Texture2D(width, height, TextureFormat.RGB24, false);
            scr.ReadPixels(new Rect(400, 0, width, height), 0, 0, false);

            scr.Apply();
            GameObject.FindObjectOfType<ImageLoader>().LoadImage(scr);
            grab = false;
            RenderTexture.active = activeRenderTexture;
            logo.SetActive(false);
           
            photoCount += 1;
        }
    }
}
