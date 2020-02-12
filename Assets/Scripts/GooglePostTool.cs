using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GooglePostTool : MonoBehaviour
{
    public static GooglePostTool instance;
    public string postUrl = "https://docs.google.com/forms/u/0/d/e/{0}/formResponse";  //url for posting data
    public string formID = "1FAIpQLSdwxSPhrLCdLK3rvEBC0opTvqlOQXyAl702xMXGxQ4KaAmmOw";  //form id for post using

    public List<string> formCells;  // search "entry." form form html code

    public static void Post(object[] data){
        try {
            instance.StartPost(data);
        }
        catch {
            Debug.Log("Post data fail");
        }
    }

    void Awake(){
        instance = this;
    }

    public void StartPost(object[] data){
        StartCoroutine(PostAsync(data));
    }

    IEnumerator PostAsync(object[] data){
        WWWForm form = new WWWForm();
        for (int i = 0; i < formCells.Count; i++)
        {
            string str = "";
            if(i < data.Length)
                str = data[i].ToString();

            form.AddField(formCells[i], str);
        }
        string postURL = string.Format (postUrl, formID);

        using (UnityWebRequest www = UnityWebRequest.Post(postURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
