using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Net;

public class MMWSServer : Singleton<MMWSServer>
{
    protected MMWSServer() { }
    public string host = "ws://127.0.0.1:5001";
    //bool readyForNextRequest = true;
    WebSocket ws;
    public List<MakeyMakey> makeyMakeys;
    // Use this for initialization
    void Start()
    {
        //Debug.Log("Starting coroutine");
        //StartCoroutine(updateMakeyMakeys());
        /*
        ws = new WebSocket(host);
        ws.OnOpen += (sender, e) => Debug.Log("Connected!");
        ws.OnMessage += (sender, e) => MessageReceived(e.Data);
        ws.OnError += (sender, e) => Debug.Log(e.Message);
        ws.OnClose += (sender, e) => Debug.Log("Connection Closed");
        ws.Connect();
        */
        //nf.Notify(
        //      new NotificationMessage
        //      {
        //          Summary = "WebSocket Error",
        //          Body = e.Message,
        //          Icon = "notification-message-im"
        //      }
        //    );
        //nf.Notify(
        //  new NotificationMessage
        //  {
        //      Summary = "WebSocket Message",
        //      Body = !e.IsPing ? e.Data : "Received a ping.",
        //      Icon = "notification-message-im"
        //  }
        //);

    }


    public void InitConnection()
    {
        ws = new WebSocket(host);
        ws.OnOpen += (sender, e) => Debug.Log("Connected!");
        ws.OnMessage += (sender, e) => MessageReceived(e.Data);
        ws.OnError += (sender, e) => Debug.Log(e.Message);
        ws.OnClose += (sender, e) => Debug.Log("Connection Closed");
        ws.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        //if (readyForNextRequest == true)
        //{
        //    Debug.Log("Starting next request");
        //    StartCoroutine(updateMakeyMakeys());
        //}
    }

    void Connected(object sender, MessageEventArgs e)
    {

    }

    void MessageReceived(string newData)
    {
        makeyMakeys = JsonConvert.DeserializeObject<List<MakeyMakey>>(newData);
    }

    IEnumerator updateMakeyMakeys()
    {
        //readyForNextRequest = false;
        WWW www = new WWW(host);
        yield return www;
        Debug.Log(www.text);
        makeyMakeys = JsonConvert.DeserializeObject<List<MakeyMakey>>(www.text);
        //readyForNextRequest = true;
    }
}
