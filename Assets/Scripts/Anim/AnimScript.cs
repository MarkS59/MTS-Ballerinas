using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[DefaultExecutionOrder(-1000)]
public class AnimScript : AnimObject
{
    Vector2 startPoint;
    Vector3 startScale;
    Color targetColor;
    public Direction direction;
    public float duration = 0.3f;
    public float delay = 0;
    public float distance = 50;
    public float scale = 1;
    
    Vector2 direction2Vector2 (Direction direction){
        switch(direction){
            case Direction.fromBottom: return new Vector3(0, -1);
            case Direction.fromLeft: return new Vector3(-1, 0);
            case Direction.fromRight: return new Vector3(1, 0);
            case Direction.fromTop: return new Vector3(0, 1);
        }
        return Vector3.zero;
    }

    bool startFlag = false;
    CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        if(startFlag) return;
        startFlag = true;
        if(GetComponent<CanvasGroup>()) canvasGroup = GetComponent<CanvasGroup>();
        startPoint = GetComponent<RectTransform>().anchoredPosition;
        startScale = GetComponent<RectTransform>().localScale;
        
        if(!canvasGroup)
            targetColor = GetComponent<Graphic>().color;

        if(!show){
            gameObject.SetActive(false);
            if(canvasGroup)
                canvasGroup.alpha = 0;
            else
                GetComponent<Graphic>().color = new Color(targetColor.r, targetColor.g, targetColor.b, 0);
            
            GetComponent<RectTransform>().localScale = startScale*scale;
            GetComponent<RectTransform>().anchoredPosition = startPoint + direction2Vector2(direction) * distance; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    DG.Tweening.Tween callback;

    public override void OpenFast(){
        if(!startFlag) Start();
        gameObject.SetActive(true);
        GetComponent<RectTransform>().DOKill();

        if(direction != Direction.noAnim){
            GetComponent<RectTransform>().anchoredPosition = startPoint;
            GetComponent<RectTransform>().localScale = startScale;
        }

        if(canvasGroup){
            canvasGroup.DOKill();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }else{
            GetComponent<Graphic>().DOKill();
            GetComponent<Graphic>().color = targetColor;
            GetComponent<Graphic>().raycastTarget = true;
        }
        show = true;
        callback.Kill(false);
    }

    public override void Open(){
        if(!startFlag) Start();
        gameObject.SetActive(true);
        GetComponent<RectTransform>().DOKill();

        if(direction != Direction.noAnim){
            GetComponent<RectTransform>().DOAnchorPos(startPoint, duration).SetDelay(delay).SetEase(Ease.OutQuad);
            GetComponent<RectTransform>().DOScale(startScale, duration).SetDelay(delay).SetEase(Ease.OutQuad);
        }

        if(canvasGroup){
            canvasGroup.DOKill();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1, duration).SetEase(Ease.OutQuad).SetDelay(delay);
        }else{
            GetComponent<Graphic>().DOKill();
            GetComponent<Graphic>().DOColor(targetColor, duration).SetDelay(delay).SetEase(Ease.OutQuad);
            GetComponent<Graphic>().raycastTarget = true;
        }
        show = true;
        callback.Kill(false);
    }

    float k = 0.4f;
    public override void Close(){
        GetComponent<RectTransform>().DOKill();
        if(direction != Direction.noAnim){
            GetComponent<RectTransform>().DOAnchorPos(startPoint + direction2Vector2(direction) * distance, duration*k).SetEase(Ease.InQuad);
            GetComponent<RectTransform>().DOScale(startScale*scale, duration).SetDelay(delay).SetEase(Ease.OutQuad);
        }
        if(canvasGroup){
            canvasGroup.DOKill();
            canvasGroup.DOFade(0, duration*k);
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }else{
            GetComponent<Graphic>().DOKill();
            GetComponent<Graphic>().DOFade(0, duration*k).SetEase(Ease.InQuad);
            GetComponent<Graphic>().raycastTarget = false;
        }
        show = false;
        callback = DOVirtual.DelayedCall(duration*k+0.1f, () => {
            gameObject.SetActive(false);
        });
    }

}

public enum Direction {
    fromTop,
    fromLeft,
    fromRight,
    fromBottom,
    noAnim
}