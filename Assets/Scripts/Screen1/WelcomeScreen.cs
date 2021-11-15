using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[DefaultExecutionOrder(-900)]
public class WelcomeScreen : MonoBehaviour
{
    public static WelcomeScreen instance;
    public string current_scenario;
    public VideoPlayer player;
    Dictionary<string, GameObject> scenarios = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer player = GameObject.FindObjectOfType<VideoPlayer>();
        instance = this;   
        for(int i = 0; i < transform.childCount; i++){
            scenarios.Add(transform.GetChild(i).gameObject.name, transform.GetChild(i).gameObject);
            for(int j = 0; j < transform.GetChild(i).childCount; j++)
                transform.GetChild(i).GetChild(j).GetComponent<RawImage>().texture = player.targetTexture;
            
        }

        foreach(var obj in scenarios.Values)
            obj.SetActive(false);
    }

    float lastTime = -1;
    // Update is called once per frame
    void Update()
    {
        if(lastTime > 0 && scenarios.ContainsKey(current_scenario) && Time.time > lastTime){
            StartCoroutine(_PlayVideo(/*true*/));
        }
    }

    IEnumerator _PlayVideo(/*bool show*/){
        lastTime = -1;
        GameObject.FindObjectOfType<VideoPlayer>().Stop();
        int rnd = UnityEngine.Random.Range(0, scenarios[current_scenario].transform.childCount);

        GameObject scenario = scenarios[current_scenario].transform.GetChild(rnd).gameObject;

        VideoClip clip = scenario.GetComponent<VideoContainer>().clip;
        /*if (!show){
            scenario.GetComponent<RawImage>().enabled = false;
            StopAllCoroutines();
        }*/
        player.clip = clip;
        player.Play();

        yield return new WaitUntil(() => player.isPrepared);
        yield return new WaitForSeconds(0.05f);
        scenario.GetComponent<RawImage>().enabled = true;

        yield return new WaitForSeconds((float)clip.length);
        scenario.GetComponent<RawImage>().enabled = false;

        //lastTime = Time.time + 2f;
    }

    public void Refresh(){
        for(int i = 0; i < scenarios[current_scenario].transform.childCount; i++)
            scenarios[current_scenario].transform.GetChild(i).GetComponent<RawImage>().enabled = false;
    }

    public void ChangeScenario(string scenario/*, string status*/){
        //Debug.Log(scenario);
        /*if (status == "photo")
            StopCoroutine(_PlayVideo(false));*/
        if(scenario == current_scenario) return;

        if(scenarios.ContainsKey(current_scenario))
            scenarios[current_scenario].SetActive(false);
        
        current_scenario = scenario;
        Refresh();

        if(scenarios.ContainsKey(current_scenario))
            scenarios[current_scenario].SetActive(true);

        lastTime = Time.time;
    }
}