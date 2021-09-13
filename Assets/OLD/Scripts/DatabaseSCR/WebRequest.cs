using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebRequest : MonoBehaviour
{
    public Text rankText;
    void Start()
    {
        StartCoroutine(GetRank());
        //StartCoroutine(AddRank("Bruno", 8,100,400,20));
    }

    IEnumerator GetRank()
    {
        string uri = "https://beyondthedreamrts-default-rtdb.firebaseio.com/Ranking.json";
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);

        yield return webRequest.SendWebRequest();

        string[] pages = uri.Split('/');
        int page = pages.Length - 1;

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                WriteRank(webRequest.downloadHandler.text);

                break;
        }
    }

    IEnumerator AddRank(string username, int dmgtaken, float lvl1, float lvl2, float lvl3)
    {
        string uri = "https://beyondthedreamrts-default-rtdb.firebaseio.com/Ranking.json";
        using UnityWebRequest webRequest = new UnityWebRequest(uri, "POST");
        string json = @"{
            ""damagetaken"": " + dmgtaken + @",
            ""lvl1"": " + lvl1 + @",
            ""lvl2"": " + lvl2 + @",
            ""lvl3"": " + lvl3 + @",
            ""username"": """ + username + @"""
        }";
        byte[] payload = System.Text.Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(payload);
        webRequest.SetRequestHeader("Content-Type", "application/json");

        webRequest.downloadHandler = new DownloadHandlerBuffer();

        yield return webRequest.SendWebRequest();

        string[] pages = uri.Split('/');
        int page = pages.Length - 1;

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                break;
        }
    }
    void WriteRank(string json)
    {
        string data = json
            .Replace("{", "")
            .Replace("}", "")
            .Replace("\"", ""); 

        //string[] rows = data.Split(',');

        //foreach (string row in rows)
        //{
        //    string[] keyValue = row.Split(':');
            rankText.text = (data);
            //Debug.Log(keyValue[0] + ": " + keyValue[1]);
        //}
    }
}
