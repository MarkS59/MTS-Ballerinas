using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : AnimObject
{
    public AnimObject[] counters;
    public AnimObject text; 
    public float duration = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Close(){
        text.Close();
        for(int i = 0; i < counters.Length; i++)
            counters[i].Close();
    }

    public void StartCountdown(){
        text.Open();
        StartCoroutine(_StartCountdown());
    }

    IEnumerator _StartCountdown(){
        for(int i = 0; i < counters.Length; i++){
            counters[i].Open();
            yield return new WaitForSeconds(duration / counters.Length * 0.8f);
            counters[i].Close();
            yield return new WaitForSeconds(duration / counters.Length * 0.2f);
        }
    }
}
