using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[ExecuteInEditMode]
public class VideoContainer : MonoBehaviour
{   
    public VideoClip clip;
    public float keyForPreview = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Generate Preview")]
    public void GeneratePreview(){
        if(clip == null) return;
        VideoPlayer player = GameObject.FindObjectOfType<VideoPlayer>();
        player.clip = clip;
        
        player.time = keyForPreview;
        player.Prepare();
        player.Pause();
        StartCoroutine(_WaitForPrepare(player));
    }
    
    IEnumerator _WaitForPrepare(VideoPlayer source){
        Debug.Log(source.isPrepared);
        yield return new WaitForSeconds(1f);

        var currentActiveRT = RenderTexture.active;
        RenderTexture.active = source.targetTexture;
        Debug.Log(source.targetTexture.width);
        Texture2D tex = new Texture2D(source.targetTexture.width, source.targetTexture.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();

        RenderTexture.active = currentActiveRT;

        GetComponent<RawImage>().texture = tex;
    }
}
