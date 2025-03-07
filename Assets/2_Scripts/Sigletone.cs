using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get 
        {
            if(instance ==  null)
            {
                instance = new GameObject(typeof(T).Name).AddComponent<T>();
                
            }
            return instance;
        
        }
        set { instance = value; }
    }

    protected virtual void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = (T)this;
            DontDestroyOnLoad(this);
        }
    }
   
}
