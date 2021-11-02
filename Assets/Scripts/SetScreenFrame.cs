using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SetScreenFrame : MonoBehaviour
{
    //public Image Frame;
    public RawImage CameraPreview;
    public RawImage ScreenPlayRawImage;
    public GameObject FrameObj;
    //public GameObject CameraPreviewObj;
    //public bool show = false;

    // Start is called before the first frame update
    void Start()
    {
        //Frame.enabled = false;
        FrameObj.SetActive(false);
        Debug.Log("Frame SetActive(false);");
        Debug.Log(FrameObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void _FrameTimer()
    {
        StartCoroutine(FrameTimer());
    }

    public void EraseFrame()
    {
        FrameObj.SetActive(false);
        CameraPreview.rectTransform.DOAnchorPosY(0.0F, 0);
        ScreenPlayRawImage.rectTransform.DOAnchorPosY(0.0F, 0);
        StopAllCoroutines();
    }

    IEnumerator FrameTimer()
    {
        yield return new WaitForSeconds(0.95F);
        CameraPreview.rectTransform.DOAnchorPosY(105.0F, 0);
        ScreenPlayRawImage.rectTransform.DOAnchorPosY(105.0F, 0);

        //Frame.enabled = true;
        FrameObj.SetActive(true);

        yield return new WaitForSeconds(0.2F);

        EraseFrame();
    }
}
