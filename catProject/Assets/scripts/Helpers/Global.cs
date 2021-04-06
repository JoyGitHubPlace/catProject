using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour
{
    static Global _instance = null;
    public static Global Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(Global)) as Global;
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<Global>();
                }
                _instance.gameObject.name = "Common";
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
}

