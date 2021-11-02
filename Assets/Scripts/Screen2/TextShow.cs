using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[DefaultExecutionOrder(-300)]
public class TextShow : AnimObject
{
    public float delay = 2f;
    public float duration = 0.8f;
    
    Vector2 center;
    Color targetColor;
    // Start is called before the first frame update
    void Start()
    {
        targetColor = GetComponent<Graphic>().color;
        center = GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Open(){
        GetComponent<Graphic>().enabled = false;
        StartCoroutine(_Show());
    }

    public float MyEase (float time, float duration, float _o, float _c){
        float c = time/duration;
        float fc = (Mathf.Pow(c*2-1, 5)+1)/2;
        return (fc + c*0.3f) / (1 + 0.3f);
    }

    public IEnumerator _Show(){

        yield return new WaitForSeconds(delay);

        GetComponent<RectTransform>().anchoredPosition = center + Vector2.right * 1000;
        GetComponent<RectTransform>().DOAnchorPos(center-Vector2.right*1000, duration).SetEase(MyEase);
        GetComponent<Graphic>().enabled = true;

        yield return new WaitForSeconds(duration);
        GetComponent<Graphic>().enabled = false;
    }
}
