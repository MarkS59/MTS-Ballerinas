using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Screen2Animation : MonoBehaviour
{
    public Text StayAtMarkText;
    public Text BeReadyText;

    public GameObject StayAtMarkObj;
    public float time = 0.5F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _TextAnimStart(){
        Debug.Log("_TextAnimStarted  " + time);
        StopAllCoroutines();
        StayAtMarkObj.SetActive(true);
        StartCoroutine(TextAnim(time));
    }

    IEnumerator TextAnim(float time){
        Debug.Log("TextAnimStart");
        yield return new WaitForSeconds(time);

        Vector2 center = new Vector2(0, 200);
        StayAtMarkText.rectTransform.anchoredPosition = center+Vector2.right*800;
        StayAtMarkText.rectTransform.DOAnchorPos(center, 0.5f).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(0.5f);
        StayAtMarkText.rectTransform.DOAnchorPos(center-Vector2.right*800, 0.5f).SetEase(Ease.InCubic);
    }

    
}
