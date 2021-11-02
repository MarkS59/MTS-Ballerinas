using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class AnimGroup : AnimObject
{   
    public float delay = 0;
    public List <AnimObject> children = new List<AnimObject>();
    public float delayForOpen = 0.2f;
    public float delayForClose = 0.1f;
    
    void Start (){
        if(children.Count == 0) FillObject();
    }

    [ContextMenu("Fill object")]
    void FillObject(){
        children.Clear();
        for(int i = 0; i < transform.childCount; i++){
            if(transform.GetChild(i).GetComponent<AnimObject>())
                children.Add(transform.GetChild(i).GetComponent<AnimObject>());
        }
    }

    public override void Open(){
        StopAllCoroutines();
        StartCoroutine(_Open());

    }

    IEnumerator _Open(){
        show = true;
        yield return new WaitForSeconds(delay);

        foreach(var child in children){
            child.Open();
            yield return new WaitForSeconds(delayForOpen);
        }
    }

    public override void Close(){
        if(!show) return;
        StopAllCoroutines();
        StartCoroutine(_Close());
        show = false;
    }

    IEnumerator _Close(){
        foreach(var child in children){
            child.Close();
            yield return new WaitForSeconds(delayForClose);
        }
    }
}
