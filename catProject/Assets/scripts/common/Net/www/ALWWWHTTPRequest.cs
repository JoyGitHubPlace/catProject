using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System;
using System.Text;


public class ALHttpParam
{
    public ALHttpParam(string _szKey, string _szData)
    {
        m_szKey = _szKey;
        m_szData = _szData;
    }

    public string m_szKey;
    public string m_szData;
}

public class ALHttpParamForm
{
    public ALHttpParamForm()
    {
        m_lstParam = new List<ALHttpParam>();
    }

    public List<ALHttpParam> m_lstParam = null;
    public void AddParam(string _szKey, string _Data)
    {
        ALHttpParam param = new ALHttpParam(_szKey, _Data);
        m_lstParam.Add(param);
    }

    public void AddParam(string _szKey, int _Data)
    {
        ALHttpParam param = new ALHttpParam(_szKey, "" + _Data);
        m_lstParam.Add(param);
    }

    public void CloneParam(ref ALHttpParamForm _Param)
    {
        if (_Param == null)
        {
            Debug.LogError("##CloneParam _Param == null");
            return;
        }

        for (int i = 0; i < _Param.m_lstParam.Count; ++i)
        {
            m_lstParam.Add(_Param.m_lstParam[i]);
        }
    }

    public string GetFinalParam()
    {
        string szFinalData = "";

        for (int i = 0; i < m_lstParam.Count; ++i)
        {
            string szKey = m_lstParam[i].m_szKey;
            string szData = m_lstParam[i].m_szData;
            if (i == m_lstParam.Count - 1)
                szFinalData += (szKey + "=" + szData);
            else
                szFinalData += (szKey + "=" + szData + "&");
        }
        return szFinalData;
    }

    public void GetFinalParam(out WWWForm _form)
    {
        _form = new WWWForm();
        for (int i = 0; i < m_lstParam.Count; ++i)
        {
            string szKey = m_lstParam[i].m_szKey;
            string szData = m_lstParam[i].m_szData;
            _form.AddField(szKey, szData);
        }
    }
}

public delegate void HTTPReqAckDelegate(string _szData, string _szError);

public class ALWWWHTTPRequest : ALWWWRequest
{
    public ALHttpParamForm      m_Param;            //http请求的参数
    public HTTPReqAckDelegate   m_ReqDelegate;      //http请求回调
    public bool                 m_bPost;

    override public void Update()
    {
        switch(m_eOptState)
        {
            case EHTTPOptState.eOptState_Ready:
                {
                    OnReady();
                }
                break;
            case EHTTPOptState.eOptState_Doing:
                {
                    OnDoing();
                }
                break;
            case EHTTPOptState.eOptState_Failed:
                {
                    OnFailed();
                }
                break;
            case EHTTPOptState.eOptState_Complete:
                {
                    OnComplete();
                }
                break;
        }
    }


    public void OnReady() 
    {
        try
        {
            string szRealUrl = m_szRequestUrl;
            if (m_bPost)
            {
                WWWForm form;
                m_Param.GetFinalParam(out form);
                m_www = new WWW(szRealUrl, form);
            }
            else
            {
                string szPostParam = m_Param.GetFinalParam();
                szRealUrl = m_szRequestUrl + "?" + szPostParam;
                m_www = new WWW(szRealUrl);
            }
            m_eOptState = EHTTPOptState.eOptState_Doing;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }

    public void OnDoing() 
    {

        if (m_www == null || m_www.error != null)
        {
            ++m_dwErrorNum;
            if (m_dwErrorNum >= 3)
            {
                string szError = "[Error] OnDoing() 未知错误";
                if (m_www != null)
                {
                    szError = m_www.error;
                }
                m_ReqDelegate(null, szError);
               // Debug.LogError("[Error]请求3次依然出错: " + szError);
                Net.Instance.Net_Error(szError);
                m_eOptState = EHTTPOptState.eOptState_Failed;
            }
            else
            {
                m_eOptState = EHTTPOptState.eOptState_Ready;
            }

            if (m_www != null)
            {
                m_www.Dispose();
                m_www = null;
            }
        }
        else
        {
            if (m_www.isDone)
            {
                try
                {
                    m_ReqDelegate(m_www.text, null);
                }
                 catch (System.Exception ex)
                {
                    Debug.LogError(ex.ToString());
                }                
                m_eOptState = EHTTPOptState.eOptState_Complete;
                m_www.Dispose();
                m_www = null;
            }
        }
    }

    public void OnComplete() 
    {
    }

    public void OnFailed() 
    {
    }

}


