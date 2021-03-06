using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;

public class ServerCommunicator : MonoBehaviour
{
    public string currentStatus = "available";
    string url = "";
    string stand_id = "";
    string token = "";
    public float delay = 0.2f;

    public Action[] actions = {
        new Action("available"),
        new Action("photo")
    }; 

    // Start is called before the first frame update
    void Start()
    {
        this.url = LoadSettings.instance.get("server_url");
        this.stand_id = LoadSettings.instance.get("stand_id");
        this.token = LoadSettings.instance.get("stand_token");
        StartCoroutine(AjaxRequest());
    }

    public float lastRequestTime = 1;
    // Update is called once per frame
    void Update()
    {
        if(lastRequestTime > 0 && Time.time > lastRequestTime+delay)
            StartCoroutine(AjaxRequest());
    }

    public void SendPhoto(Texture2D image) {
        StartCoroutine(SendPhotoRequest(image));
    }

    IEnumerator AjaxRequest() {
        lastRequestTime = -1;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url+"/stands/"+stand_id))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer "+token);
            yield return webRequest.SendWebRequest();
            lastRequestTime = Time.time;

            if(webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(webRequest.downloadHandler.text);
                if(webRequest.result == UnityWebRequest.Result.ConnectionError)
                    Debug.Log("Host " + url + " unavailable");
                yield break;
            }
            
            string text = webRequest.downloadHandler.text;
            AjaxResponse response = JsonUtility.FromJson<AjaxResponse>(text);
            //response.status = "photo";
            if (response.status != currentStatus){
                foreach(var action in actions){
                    if(action.status == response.status)
                        action.action.Invoke();
                }
                currentStatus = response.status;
            }
            //Debug.Log(text);
            if(WelcomeScreen.instance)
                WelcomeScreen.instance.ChangeScenario(response.scenario/*, response.status*/);

            if(response.status == "photo" && response.scenario != null){
                ScreenplaySelector.instance.ChangeScreenplay(response.scenario);
            }

        }
            
    }

    IEnumerator SendPhotoRequest (Texture2D image){

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("title", "yet one photo"));
        formData.Add(new MultipartFormFileSection("file", image.EncodeToJPG(90), "photo.jpg", "image/jpeg"));

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url+"/files?fields=id", formData))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer "+token);
            yield return webRequest.SendWebRequest();
            string text = webRequest.downloadHandler.text;
            
            byte[] utf8text = Encoding.UTF8.GetBytes(text);

            using (UnityWebRequest webRequest2 = new UnityWebRequest(url+"/stands/"+stand_id+"/upload", "POST"))
            {
                webRequest2.uploadHandler = new UploadHandlerRaw(utf8text);
                webRequest2.downloadHandler = new DownloadHandlerBuffer();
                webRequest2.SetRequestHeader("Authorization", "Bearer "+token);
                webRequest2.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
                
                yield return webRequest2.SendWebRequest();
                string resp = webRequest2.downloadHandler.text;

                Debug.Log(resp);
            }
        }
    
    }
}

[Serializable]
public class Action {
    public string status;
    public UnityEvent action;

    public Action(string status){
        this.status = status;
    }
}

[Serializable]
public class AjaxResponse {
    public string status;
    public string photo;
    public string scenario;
}