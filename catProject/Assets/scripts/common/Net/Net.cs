using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System;
using Common.Global;
using Common.Base;

public class Net : Singleton<Net>
{
    //Dictionary<string, JSONObject> mapSendJsonList = new Dictionary<string, JSONObject>();
    JSONObject sendJson=new JSONObject();
    string szUrl = ""; 

    ALWWWOprator www = null;
    public void InitLogic()
    {
        //ALWWWOprator.Instance.InitLogic();          //初使化WWW网络模块
        InitMsgHead();
    }

    public void SetWWW(ALWWWOprator _www, string _url)
    {
        www = _www;
        szUrl = _url;
    }

    public void SendMsg()
    {
        if (null == www)
            return;
        ALHttpParamForm paramForm = new ALHttpParamForm();       
        paramForm.AddParam("channel", 333333);    

        paramForm.AddParam("data", sendJson.ToString());
        InitMsgHead();
        //ALHttpRequest.HttpReq(ALGameUrl.GetResMgrUrl(), ref paramForm, OnRequestAck_ResourcesManager, false);
        www.HTTPRequest(szUrl, ref paramForm, OnReceive_Msg, false);
    }
    public void SendMsgwithCallBack(HTTPReqAckDelegate  callback)
    {
        if (null == www)
            return;
        ALHttpParamForm paramForm = new ALHttpParamForm();
        paramForm.AddParam("channel", 333333);

        paramForm.AddParam("data", sendJson.ToString());
        InitMsgHead();
        www.HTTPRequest(szUrl, ref paramForm, OnReceive_Msg, false);
    }
    //初使化消息头
    public void InitMsgHead()
    {
        //mapSendJsonList.Clear();
        sendJson.Clear();
   
    }

    public void AddMsg(string _cmd, JSONObject _msg)
    {       
        JSONObject tempjson = new JSONObject();
        tempjson.AddField("cmd", _cmd);
        tempjson.AddField("data", _msg);

        sendJson.Add(tempjson);
    }
    


    void OnReceive_Msg(string data, string error)
    {
        if (!string.IsNullOrEmpty(error))
        {
            Debug.LogError(error);
            return;
        }

        try
        {
            //Debug.Log(data);
            JSONObject sendJson = new JSONObject(data);
            string cmdStr = "";
            foreach (var bb in sendJson.list)
            {
                bb.GetField(ref cmdStr, "cmd");
                if (false == EventManager.Instance.IsHaveEvent(cmdStr))
                {
                    Debug.LogError("netmsg not find!!  id is: " + cmdStr);
                    continue;
                }
                JSONObject retmsg = bb.GetField("data");
                EventManager.Instance.TriggerEvent<JSONObject>(cmdStr, retmsg);
            }
        }
        catch
        {          
            throw;
        }
       
    }

    //网络错误
    public void Net_Error(string szError)
    {
        //Debug.LogError("[Error]我去 error!!!: " + szError);
        EventManager.Instance.TriggerEvent<string>("UI_Net_Error", szError);
    }

}