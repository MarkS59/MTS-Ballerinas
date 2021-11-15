using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ScreenplaySelector : MonoBehaviour
{
    
    public VideoPlayer videoPlayer;
    public static ScreenplaySelector instance;
    public string current_scenario;
    public Dictionary<string, GameObject> scenarios = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;   
        for(int i = 0; i < transform.childCount; i++){
            scenarios.Add(transform.GetChild(i).gameObject.name, transform.GetChild(i).gameObject);
            for(int j = 0; j < transform.GetChild(i).childCount; j++)
                transform.GetChild(i).GetChild(j).GetComponent<RawImage>().texture = videoPlayer.targetTexture;
            
        }

        foreach(var obj in scenarios.Values){
            //Debug.Log(obj.name);
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Refresh(){
        for(int i = 0; i < scenarios[current_scenario].transform.childCount; i++)
            scenarios[current_scenario].transform.GetChild(i).GetComponent<RawImage>().enabled = false;
    }
    public void ChangeScreenplay(string scenario){
        //Debug.Log(scenario);
        if(scenario == current_scenario) return;

        if(scenarios.ContainsKey(current_scenario))
            scenarios[current_scenario].SetActive(false);
        
        current_scenario = scenario;
        Refresh();

        if(scenarios.ContainsKey(current_scenario)){
            GameObject _scenario = scenarios[current_scenario].transform.GetChild(0).gameObject;
            scenarios[current_scenario].SetActive(true);
            _scenario.GetComponent<RawImage>().enabled = enabled;
        }
            
    }

    /*public void ChangeLayout(){
        GameObject scenario = scenarios[current_scenario].transform.GetChild(0).gameObject;
        Debug.Log(scenario.name);
        foreach (GameObject go in scenarios.Values) {
            if(current_scenario != go.name){
                Debug.Log(go.name);
                scenario.GetComponent<RawImage>().enabled = false;
            }
            scenario.GetComponent<RawImage>().enabled = enabled;
        }
    }*/

}
