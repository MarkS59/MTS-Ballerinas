using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;

public class ServerActivity : MonoBehaviour
{
    string url = "";
    string stand_id = "";
    string token = "";
    // Start is called before the first frame update
    void Start()
    {
        this.url = LoadSettings.instance.get("server_url");
        this.stand_id = LoadSettings.instance.get("stand_id");
        this.token = LoadSettings.instance.get("stand_token");
        lastTime = Time.time;
    }

    float lastTime = 0;
    // Update is called once per frame
    void Update()
    {
        if(Time.time > lastTime){
            StartCoroutine(UpdateActivity());
            lastTime = Time.time + 20 * 60;
        }
    }

    IEnumerator UpdateActivity() {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url+"/stands/"+stand_id+"/activity", "{\"data\": \"some data\"}"))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer "+token);
            yield return webRequest.SendWebRequest();
            
            if(webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError){
                Debug.Log(webRequest.downloadHandler.text);
                if(webRequest.result == UnityWebRequest.Result.ConnectionError)
                    Debug.Log("Host " + url + " unavailable");
                yield break;
            }
        }
    }
}
