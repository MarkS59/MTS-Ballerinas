using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StandbyScreenplaySelector : MonoBehaviour
{
    public VideoClip[] centerStandbyClips;
    public RawImage centerStandbyRI;
    bool loop = true;

    // Start is called before the first frame update
    void Start()
    {
        centerStandbyRI.enabled = false;
        CoroutineStarter(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoroutineStarter(bool b) {
        if (!b){
            StopAllCoroutines();
            centerStandbyRI.enabled = false;
        }
        else {
            StopAllCoroutines();
            StartCoroutine(StandbyClipSelector());
        }
        
    }

    IEnumerator StandbyClipSelector() {
        while (loop) {
            foreach (VideoClip vc in centerStandbyClips){
                yield return new WaitForSeconds(5.0f);
                GetComponent<VideoPlayer>().clip = vc;
                yield return new WaitUntil(()=> GetComponent<VideoPlayer>().isPrepared);
                yield return new WaitForSeconds(0.3F);
                centerStandbyRI.enabled = true;
                yield return new WaitUntil(()=> !GetComponent<VideoPlayer>().isPlaying);
                centerStandbyRI.enabled = false;
            }
        }
    }

}
