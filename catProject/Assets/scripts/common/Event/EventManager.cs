using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Common.Global;
using Common.Base;

public class EventManager : Singleton<EventManager>
{
    public delegate void EventCallback();
    public delegate void EventCallback1<T>(T arg1);
    public delegate void EventCallback2<T, U>(T arg1, U arg2);
    public delegate void EventCallback3<T, U, V>(T arg1, U arg2, V arg3);
    public delegate void EventCallback4<T, U, V, W>(T arg1, U arg2, V arg3, W arg4);

    private Dictionary<string, Delegate> registedCallbacks = new Dictionary<string, Delegate>();

    //  private Dictionary<string, GameStateType> registedCallFunGameState = new Dictionary<string, GameStateType>();           //注册消息时保存当时的游戏状态
    // private Dictionary<UIEventListener, UIEventListener.UIEventProxy> registedProxyCallbacks = new Dictionary<UIEventListener, UIEventListener.UIEventProxy>();
   
    public bool IsHaveEvent(string eventType)
    {
        return registedCallbacks.ContainsKey(eventType);
    }

    public void AddEventListener(string eventType, EventCallback handler)
    {
        lock (this)
        {
            if (!registedCallbacks.ContainsKey(eventType))
            {
                registedCallbacks.Add(eventType, handler);
              //  registedCallFunGameState.Add(eventType, GameState.Instance.GetState());
            }
        }
    }
    public void AddEventListener<T>(string eventType, EventCallback1<T> handler)
    {
        lock (this)
        {
            if (!registedCallbacks.ContainsKey(eventType))
            {
                registedCallbacks.Add(eventType, handler);
              //  registedCallFunGameState.Add(eventType, GameState.Instance.GetState());
            }
        }
    }
    public void AddEventListener<T, U>(string eventType, EventCallback2<T, U> handler)
    {
        lock (this)
        {
            if (!registedCallbacks.ContainsKey(eventType))
            {
                registedCallbacks.Add(eventType, handler);
             //   registedCallFunGameState.Add(eventType, GameState.Instance.GetState());
            }
        }
    }
    public void AddEventListener<T, U, V>(string eventType, EventCallback3<T, U, V> handler)
    {
        lock (this)
        {
            if (!registedCallbacks.ContainsKey(eventType))
            {
                registedCallbacks.Add(eventType, handler);
             //   registedCallFunGameState.Add(eventType, GameState.Instance.GetState());
            }
        }
    }
    public void AddEventListener<T, U, V, W>(string eventType, EventCallback4<T, U, V, W> handler)
    {
        lock (this)
        {
            if (!registedCallbacks.ContainsKey(eventType))
            {
                registedCallbacks.Add(eventType, handler);
              //  registedCallFunGameState.Add(eventType, GameState.Instance.GetState());
            }
        }
    }
    public void Cleanup()
    {
        registedCallbacks.Clear();
      //  registedCallFunGameState.Clear();
    }


    //清除指定游戏状态类型的注册消息
    //public void RemoveAllEventListenerByteGameState( GameStateType _type )
    //{
    //    List<string> tempdel = new List<string>();
    //    //foreach (KeyValuePair<string, GameStateType> kv in registedCallFunGameState)
    //    //{
    //    //    if(kv.Value == _type)
    //    //    {
    //    //        tempdel.Add(kv.Key);               
    //    //    }
    //    //}        
    //    foreach( string strkey in tempdel )
    //    {
    //        RemoveEventListener(strkey);
    //    }
    //    tempdel.Clear();
    //}

    public void RemoveEventListener(string eventType)
    {
        if (registedCallbacks[eventType] != null)
        {
            registedCallbacks.Remove(eventType);
           // registedCallFunGameState.Remove(eventType);
        }
            
    }


    public void TriggerEvent(string eventType)
    {
        lock (this)
        {
            if (!registedCallbacks.ContainsKey(eventType))
            {
                return;
            }
            Delegate d;
            d = registedCallbacks[eventType];
            EventCallback ecb = d as EventCallback;
            if (ecb == null)
            {
                return;
            }
            ecb();
        }
    }
    public void TriggerEvent<T>(string eventType, T arg1)
    {
        lock (this)
        {
            if (!registedCallbacks.ContainsKey(eventType))
            {
                return;
            }
            Delegate d;
            d = registedCallbacks[eventType];

            EventCallback1<T> ecb = (EventCallback1<T>)d;
            if (ecb == null)
            {
                return;
            }
            ecb(arg1);
        }
    }
    public void TriggerEvent<T, U>(string eventType, T arg1, U arg2)
    {
        lock (this)
        {
            if (!registedCallbacks.ContainsKey(eventType))
            {
                return;
            }
            Delegate d;
            d = registedCallbacks[eventType];

            EventCallback2<T, U> ecb = (EventCallback2<T, U>)d;
            if (ecb == null)
            {
                return;
            }
            ecb(arg1, arg2);
        }
    }
    public void TriggerEvent<T, U, V>(string eventType, T arg1, U arg2, V arg3)
    {
        lock (this)
        {
            if (!registedCallbacks.ContainsKey(eventType))
            {
                return;
            }
            Delegate d;
            d = registedCallbacks[eventType];

            EventCallback3<T, U, V> ecb = (EventCallback3<T, U, V>)d;
            if (ecb == null)
            {
                return;
            }
            ecb(arg1, arg2, arg3);
        }
    }
    public void TriggerEvent<T, U, V, W>(string eventType, T arg1, U arg2, V arg3, W arg4)
    {
        lock (this)
        {
            if (!registedCallbacks.ContainsKey(eventType))
            {
                return;
            }
            Delegate d;
            d = registedCallbacks[eventType];

            EventCallback4<T, U, V, W> ecb = (EventCallback4<T, U, V, W>)d;
            if (ecb == null)
            {
                return;
            }
            ecb(arg1, arg2, arg3, arg4);
        }
    }
    //public void AddButtonListener(GameObject obj, UIEventListener.UIEventProxy eventCallback)
    //{
    //    UnityEngine.UI.Button btn = obj.GetComponent<UnityEngine.UI.Button>();
    //    UIEventListener btnListener = btn.gameObject.AddComponent<UIEventListener>();
    //    btnListener.OnClick += eventCallback;
    //    registedProxyCallbacks.Add(btnListener, eventCallback);
    //}
    //public void AddButtonSeletedListener(GameObject obj, UIEventListener.UIEventProxy eventCallback)
    //{
    //    UnityEngine.UI.Button btn = obj.GetComponent<UnityEngine.UI.Button>();
    //    UIEventListener btnListener = btn.gameObject.AddComponent<UIEventListener>();
    //    btnListener.OnSelected += eventCallback;
    //    registedProxyCallbacks.Add(btnListener, eventCallback);
    //}

    //public void AddButtonListener_Param(GameObject obj, UIEventListener.UIEventProxy_Param eventCallback)
    //{
    //    UnityEngine.UI.Button btn = obj.GetComponent<UnityEngine.UI.Button>();
    //    UIEventListener btnListener = btn.gameObject.AddComponent<UIEventListener>();
    //    btnListener.OnClick_Param += eventCallback;
    //}

    //public void AddToggleListener(GameObject obj, UIEventListener.UIEventProxy eventCallback)
    //{

    //    UnityEngine.UI.Toggle btn = obj.GetComponent<UnityEngine.UI.Toggle>();
    //    UIEventListener btnListener = btn.gameObject.AddComponent<UIEventListener>();
    //    btnListener.OnClick += eventCallback;
    //    registedProxyCallbacks.Add(btnListener, eventCallback);
    //}

    //public void AddImageListener(GameObject obj, UIEventListener.UIEventProxy eventCallback)
    //{

    //    UnityEngine.UI.Image btn = obj.GetComponent<UnityEngine.UI.Image>();
    //    UIEventListener btnListener = btn.gameObject.AddComponent<UIEventListener>();
    //    btnListener.OnClick += eventCallback;
    //    registedProxyCallbacks.Add(btnListener, eventCallback);
    //}
    //public void AddTextListener(GameObject obj, UIEventListener.UIEventProxy eventCallback)
    //{

    //    UnityEngine.UI.Text btn = obj.GetComponent<UnityEngine.UI.Text>();
    //    UIEventListener btnListener = btn.gameObject.AddComponent<UIEventListener>();
    //    btnListener.OnClick += eventCallback;
    //    registedProxyCallbacks.Add(btnListener, eventCallback);
    //}

    //public void RemoveButtonListener(GameObject obj, UIEventListener.UIEventProxy eventCallback)
    //{

    //    UnityEngine.UI.Button btn = obj.GetComponent<UnityEngine.UI.Button>();
    //    UIEventListener btnListener = btn.gameObject.GetComponent<UIEventListener>();
    //    btnListener.OnClick -= eventCallback;
    //    if (registedProxyCallbacks[btnListener] != null)
    //        registedProxyCallbacks.Remove(btnListener);
    //}

    //public void RemoveButtonListener_Param(GameObject obj, UIEventListener.UIEventProxy_Param eventCallback)
    //{
    //    UnityEngine.UI.Button btn = obj.GetComponent<UnityEngine.UI.Button>();
    //    UIEventListener btnListener = btn.gameObject.GetComponent<UIEventListener>();
    //    btnListener.OnClick_Param -= eventCallback;
    //}

    //public void RemoveToggleListener(GameObject obj, UIEventListener.UIEventProxy eventCallback)
    //{

    //    UnityEngine.UI.Toggle btn = obj.GetComponent<UnityEngine.UI.Toggle>();
    //    UIEventListener btnListener = btn.gameObject.GetComponent<UIEventListener>();
    //    btnListener.OnClick -= eventCallback;
    //    if (registedProxyCallbacks[btnListener] != null)
    //        registedProxyCallbacks.Remove(btnListener);
    //}

    //public void RemoveImageListener(GameObject obj, UIEventListener.UIEventProxy eventCallback)
    //{

    //    UnityEngine.UI.Image btn = obj.GetComponent<UnityEngine.UI.Image>();
    //    UIEventListener btnListener = btn.gameObject.GetComponent<UIEventListener>();
    //    btnListener.OnClick -= eventCallback;
    //    if (registedProxyCallbacks[btnListener] != null)
    //        registedProxyCallbacks.Remove(btnListener);
    //}
    //public void RemoveTextListener(GameObject obj, UIEventListener.UIEventProxy eventCallback)
    //{

    //    UnityEngine.UI.Text btn = obj.GetComponent<UnityEngine.UI.Text>();
    //    UIEventListener btnListener = btn.gameObject.GetComponent<UIEventListener>();
    //    btnListener.OnClick -= eventCallback;
    //    if (registedProxyCallbacks[btnListener] != null)
    //        registedProxyCallbacks.Remove(btnListener);
    //}
}
