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
        StandbyRISetShow(false);
        CoroutineStarter(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoroutineStarter(bool b) {
        if (!b){
            StopAllCoroutines();
            StandbyRISetShow(b);
        }
        else {
            StopAllCoroutines();
            StartCoroutine(StandbyClipSelector());
        }
        
    }
    public void StandbyRISetShow(bool center) {
        centerStandbyRI.enabled = center;
    }

    IEnumerator StandbyClipSelector() {
        while (loop) {
            foreach (VideoClip vc in centerStandbyClips){
                yield return new WaitForSeconds(5.0f);
                GetComponent<VideoPlayer>().clip = vc;
                yield return new WaitUntil(()=> GetComponent<VideoPlayer>().isPrepared);
                yield return new WaitForSeconds(0.3F);
                StandbyRISetShow(true);
                yield return new WaitUntil(()=> !GetComponent<VideoPlayer>().isPlaying);
                StandbyRISetShow(false);
            }
        }
    }

}
