using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[DefaultExecutionOrder(-500)]
public class VideoController : MonoBehaviour
{   
    public VideoObject[] videoObjects;
    public float fullTime = 10;
    public int rand;
    public VideoPlayer videoPlayer;
    public RawImage screenplayOneRawImage, screenplayTwoRawImage, screenplayThreeRawImage;
    public GameObject actors;
    
    public AnimObject overlay;
    public CountdownScript countdownScript;

    // Start is called before the first frame update
    void Start()
    {
        //videoPlayer = GameObject.FindObjectOfType<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float startTime = 0;
    public void Open(){
        rand = Random.Range(0, videoObjects.Length);
        VideoObject videoObject = videoObjects[0];
        Debug.Log(videoObject.clip.name);
        videoPlayer.clip = videoObject.clip;
        Debug.Log(videoPlayer.clip.name);
        videoPlayer.Stop();
        videoPlayer.Prepare();
        startTime = Time.time;
        Debug.Log(actors);
        actors.SetActive(true);
        screenplayOneRawImage.enabled = false;
        screenplayTwoRawImage.enabled = false;
        screenplayThreeRawImage.enabled = false;
        StartCoroutine(_Open(videoObject));
    }

    IEnumerator _Open(VideoObject videoObject){
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        float time = fullTime - videoObject.key - (Time.time - startTime);
        if(time > 0)
            yield return new WaitForSeconds(time);
        videoPlayer.Play();

        yield return new WaitForSeconds(0.1f);
        if (rand == 0) {
            screenplayOneRawImage.enabled = true;
            //screenplayOneRawImage.SetActive(true);
        } else if (rand == 1) {
            screenplayTwoRawImage.enabled = true;
            //screenplayTwoRawImage.SetActive(true);
        } else {
            screenplayThreeRawImage.enabled = true;
            //screenplayThreeRawImage.SetActive(true);
        }
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

        //screenPlayRawImage.enabled = true;
        screenplayOneRawImage.enabled = false;
        screenplayTwoRawImage.enabled = false;
        screenplayThreeRawImage.enabled = false;
        int r = Random.Range(0, videoObjects.Length);
        VideoObject videoObject = videoObjects[r];
        if (r == 0) {
            screenplayOneRawImage.enabled = true;
            //screenplayOneRawImage.SetActive(true);
        } else if (r == 1) {
            screenplayTwoRawImage.enabled = true;
            //screenplayTwoRawImage.SetActive(true);
        } else {
            screenplayThreeRawImage.enabled = true;
            //screenplayThreeRawImage.SetActive(true);
        }
        videoPlayer.clip = videoObject.clip;
        videoPlayer.time = videoObject.key;

    }

    public void CloseVideo() {
        //screenPlayRawImage.enabled = false;
        
        screenplayOneRawImage.enabled = false;
        screenplayTwoRawImage.enabled = false;
        screenplayThreeRawImage.enabled = false;
    }
}

[System.Serializable]
public class VideoObject {
    public VideoClip clip;
    public float key;
}