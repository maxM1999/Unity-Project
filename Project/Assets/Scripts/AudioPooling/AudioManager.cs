using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_Instance;

    [SerializeField]
    private AudioPooling m_AudioPool;
    void Start()
    {
        if(m_Instance == null)
        {
            m_Instance = this;
        }
    }

    public static void SpawnAudioAtLocation(AudioClip toPlay, Vector3 location)
    {
        Debug.Log("je veux jouer un son");
        GameObject audioSoucre = m_Instance.m_AudioPool.GetAvailableAudioSource();
        audioSoucre.transform.position = location;
        audioSoucre.GetComponent<AudioSource>().clip = toPlay;
        audioSoucre.GetComponent<AudioSource>().Play();
    }
}
