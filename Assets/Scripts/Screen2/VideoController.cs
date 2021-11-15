using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[DefaultExecutionOrder(-500)]
public class VideoController : MonoBehaviour
{   
    public VideoObject[] videoObjects;
    public Dictionary<string, VideoObject> clips = new Dictionary<string, VideoObject>();
    public float fullTime = 10;
    public int rand;
    public VideoPlayer videoPlayer;
    public GameObject actors;
    
    public AnimObject overlay;
    public CountdownScript countdownScript;
    VideoObject videoObject;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float startTime = 0;
    public void Open(){
        rand = Random.Range(0, videoObjects.Length);
        VideoObject videoObject = videoObjects[0];
        videoPlayer.clip = videoObject.clip;
        videoPlayer.Stop();
        videoPlayer.Prepare();
        startTime = Time.time;
        actors.SetActive(true);
        //RI enabled false
        StartCoroutine(_Open(videoObject));
    }

    IEnumerator _Open(VideoObject videoObject){
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        float time = fullTime - videoObject.key - (Time.time - startTime);
        if(time > 0)
            yield return new WaitForSeconds(time);
        videoPlayer.Play();

        yield return new WaitForSeconds(0.1f);
        //RI enabler 
        //screenPlayRawImage.enabled = true;

        yield return new WaitForSeconds(videoObject.key - 1 - 0.1f);
        
        overlay.Open();

        yield return new WaitForSeconds(1f);
        countdownScript.StartCountdown();
        videoPlayer.Pause();

        yield return new WaitForSeconds(countdownScript.duration);
        GameObject.FindObjectOfType<Screenshot>().TakeScreen();
        yield return new WaitForSeconds(0.5f);

        videoPlayer.Play();
    }
    public void OpenVideo() {
        GameObject.FindObjectOfType<ScreenplaySelector>().Refresh();
        

        string scenario = GameObject.FindObjectOfType<ScreenplaySelector>().current_scenario;
        //GameObject.FindObjectOfType<ScreenplaySelector>().scenario.GetComponent<RawImage>().enabled = true;
        Debug.Log(scenario);
        switch (scenario){
            case "show":
                videoObject = videoObjects[0];
                break;
            case "theatre":
                videoObject = videoObjects[1];
                break;
            case "museum":
                videoObject = videoObjects[2];
                break;
            case "concert":
                videoObject = videoObjects[3];
                break;
        }
        //Here should be scenario RI changer

        videoPlayer.clip = videoObject.clip;
        videoPlayer.time = videoObject.key;

    }

    public void CloseVideo() {
        //screenPlayRawImage.enabled = false;
        GameObject.FindObjectOfType<ScreenplaySelector>().Refresh();

    }


}

[System.Serializable]
public class VideoObject {
    public VideoClip clip;
    public float key;
}

//