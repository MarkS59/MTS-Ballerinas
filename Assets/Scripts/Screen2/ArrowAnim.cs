using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[DefaultExecutionOrder(-300)]
public class ArrowAnim : AnimObject
{
    public float delay = 2f;
    public float duration = 5f;
    public float period = 2f;

    Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = transform.GetChild(0).GetComponent<Graphic>().color;
    }

    public override void Open(){
        GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine (_Anim());
    }

    public override void Close(){
        for(int i = 0; i < transform.childCount; i++){
            if(transform.GetChild(i).GetComponent<Graphic>())
                transform.GetChild(i).GetComponent<Graphic>().DOKill();
        }
        GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        StopAllCoroutines();
    }

    IEnumerator _Anim(){
        yield return new WaitForSeconds(delay);
        Anim();
        yield return new WaitForSeconds(duration);
        Close();
    }


    public void Anim(){
        GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        int childCount = transform.childCount;
        
        for(int i = 0; i < childCount; i++){
            transform.GetChild(i).GetComponent<Graphic>().color = new Color(color.r, color.g, color.b, 0);

            if(i < childCount && transform.GetChild(i).GetComponent<Graphic>()){
                transform.GetChild(i).GetComponent<Graphic>()
                .DOColor(color, period/childCount * 2)
                .SetDelay(period/childCount * i * 0.4f)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   

    }
}
