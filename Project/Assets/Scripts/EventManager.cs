using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    private static EventManager m_Instance;

    private Dictionary<string, Action<Dictionary<string, object>>> m_EventDictionary;
    
    void Awake()
    {
        if(m_Instance == null)
        {
            m_Instance = this;
            m_Instance.m_EventDictionary = new Dictionary<string, Action<Dictionary<string, object>>> ();
        }
    }

    public static EventManager GetInstance()
    {
        return m_Instance;
    }

    public static void StartListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        Action<Dictionary<string, object>> thisEvent;
        if(m_Instance.m_EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            m_Instance.m_EventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            m_Instance.m_EventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        if(!m_Instance)
        {
            return;
        }

        Action<Dictionary<string, object>> thisEvent;
        if(m_Instance.m_EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            m_Instance.m_EventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, Dictionary<string, object> param)
    {
        Action<Dictionary<string, object>> thisEvent;
        if(m_Instance.m_EventDictionary.TryGetValue(eventName,out thisEvent))
        {
            thisEvent?.Invoke(param);
        }
    }
}
