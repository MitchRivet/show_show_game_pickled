using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Net;

public class MMHTTPServer : Singleton<MMHTTPServer>
{
    protected MMHTTPServer() { }
    public string host = "http://127.0.0.1:5000";
    bool readyForNextRequest = true;

    public List<MakeyMakey> makeyMakeys;
    // Use this for initialization
    void Start () {
        Debug.Log("Starting coroutine");
        StartCoroutine(updateMakeyMakeys());
	}
	
	// Update is called once per frame
	void Update () {
		if(readyForNextRequest == true)
        {
            Debug.Log("Starting next request");
            StartCoroutine(updateMakeyMakeys());
        }
	}

    IEnumerator updateMakeyMakeys()
    {
        readyForNextRequest = false;
        WWW www = new WWW(host);
        yield return www;
        Debug.Log(www.text);
        makeyMakeys = JsonConvert.DeserializeObject<List<MakeyMakey>>(www.text);
        readyForNextRequest = true;
    }
}

[System.Serializable]
public class MakeyMakey
{
    public int[] keys;
    public int cps;
}
