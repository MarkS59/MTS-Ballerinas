using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class ScreenplaySelector : MonoBehaviour
{
    //Standby videos
    public VideoClip StandbyVideoCenter;
    public VideoClip StandbyVideoLeft;
    public VideoClip StandbyVideoRight;

    //Screenplay videos
    public VideoClip ScreenplayOne;
    public VideoClip ScreenplayTwo;
    public VideoClip ScreenplayThree;

    //Images for textures
    public RawImage StandbyRawImageCenter;
    public RawImage StandbyRawImageLeft;
    public RawImage StandbyRawImageRight;

    public RawImage ScreenplayRawImage;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWaitPlayerFunc(bool play)
    {   
        if (StandbyRawImageCenter.enabled == true)
            StandbyRawImageCenter.enabled = false;

        if (StandbyRawImageLeft.enabled == true)
            StandbyRawImageLeft.enabled = false;
        
        if (StandbyRawImageRight.enabled == true)
            StandbyRawImageRight.enabled = false;

        if (ScreenplayRawImage.enabled == true)
            ScreenplayRawImage.enabled = false;

        StopAllCoroutines();
        Resources.UnloadUnusedAssets();
        StartCoroutine(OnWaitPlayer(play));
    }

    public void SetFrame()
    {
        if (StandbyRawImageCenter.enabled == true)
            StandbyRawImageCenter.enabled = false;

        if (StandbyRawImageLeft.enabled == true)
            StandbyRawImageLeft.enabled = false;
        
        if (StandbyRawImageRight.enabled == true)
            StandbyRawImageRight.enabled = false;

        if (ScreenplayRawImage.enabled == true)
            ScreenplayRawImage.enabled = false;

        StopAllCoroutines();
        StartCoroutine(SetScreenplayFrame());
    }

    public void OnClose()
    {
        StopAllCoroutines();

        StandbyRawImageCenter.enabled = false;
        StandbyRawImageLeft.enabled = false;
        StandbyRawImageRight.enabled = false;
        AsyncOperation async = Resources.UnloadUnusedAssets();
    }


    IEnumerator OnWaitPlayer(bool play)
    {
        while (play)
        {
        StandbyRawImageCenter.enabled = false;
        StandbyRawImageLeft.enabled = false;
        StandbyRawImageRight.enabled = false;

        int frameNum = Random.Range(1, 4);
        Debug.Log(frameNum);
        switch (frameNum)
        {
            case 1:
                //Debug.Log("OnWaitPlayer case 1");
                GetComponent<VideoPlayer>().clip = StandbyVideoCenter;

                yield return new WaitUntil(()=> GetComponent<VideoPlayer>().isPrepared);
                yield return new WaitForSeconds(0.3F);
                
                StandbyRawImageCenter.enabled = true;
                break;

            case 2:
                //Debug.Log("OnWaitPlayer case 2");
                GetComponent<VideoPlayer>().clip = StandbyVideoLeft;

                yield return new WaitUntil(() => GetComponent<VideoPlayer>().isPrepared);
                yield return new WaitForSeconds(0.3F);
                StandbyRawImageLeft.enabled = true;
                break;

            case 3:
                //Debug.Log("OnWaitPlayer case 3");
                GetComponent<VideoPlayer>().clip = StandbyVideoRight;

                yield return new WaitUntil(() => GetComponent<VideoPlayer>().isPrepared);
                yield return new WaitForSeconds(0.3F);
                StandbyRawImageRight.enabled = true;
                break;

            default:
                Debug.Log("OnWaitPlayer case def expection" + frameNum);
                break;
        }
        yield return new WaitForSeconds(30.0f);
        }
    }

    IEnumerator SetScreenplayFrame() {
        
        int frameNum = Random.Range(1, 4);
        switch (frameNum)
        {
        case 1:
            //Debug.Log("SetFrame case 1");
            GetComponent<VideoPlayer>().clip = ScreenplayOne;

            yield return new WaitUntil(() => GetComponent<VideoPlayer>().isPrepared);
            ScreenplayRawImage.enabled = true;
            break;

        case 2:
            //Debug.Log("SetFrame case 2");
            GetComponent<VideoPlayer>().clip = ScreenplayTwo;

            yield return new WaitUntil(() => GetComponent<VideoPlayer>().isPrepared);
            ScreenplayRawImage.enabled = true;
            break;

        case 3:
            //Debug.Log("SetFrame case 3");
            GetComponent<VideoPlayer>().clip = ScreenplayThree;

            yield return new WaitUntil(() => GetComponent<VideoPlayer>().isPrepared);
            ScreenplayRawImage.enabled = true;
            break;

        default:
            Debug.Log("OnWaitPlayer case def expection" + frameNum);
            break;
        }
    }
}
