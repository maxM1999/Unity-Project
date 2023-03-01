using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPooling : MonoBehaviour
{
    private List<GameObject> m_AudioSources;

    void Start()
    {
        m_AudioSources = new List<GameObject>();

        for(int i = 0; i < 2; i++)
        {
            m_AudioSources.Add(CreateNewAudioSource());
        }
    }

    private GameObject CreateNewAudioSource()
    {
        GameObject audioSource = new GameObject("audioSource", typeof(AudioSource));
        return audioSource;
    }

    public GameObject GetAvailableAudioSource()
    {
        for(int i = 0; i < m_AudioSources.Count; i++)
        {
            if(!m_AudioSources[i].GetComponent<AudioSource>().isPlaying)
            {
                return m_AudioSources[i];
            }
        }
        
        m_AudioSources.Add(CreateNewAudioSource());
        return m_AudioSources[m_AudioSources.Count - 1];
    }
}
