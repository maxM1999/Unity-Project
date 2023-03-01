using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerStateMachine m_StateMachine;

    private Vector3 m_InitialPosition;

    private Quaternion m_InitialRotation;

    [SerializeField]
    private AudioClip m_VictoryClip;

    [SerializeField]
    private AudioClip m_DefeatClip;

    private void Start()
    {
        m_InitialPosition = transform.position;
        m_InitialRotation = transform.rotation;

        EventManager.StartListening("RespawnCharacter", RespawnCharacter);
        EventManager.StartListening("MineGameResult", OnGameCompleted);
        EventManager.StartListening("MineMineGameResult", OnGameCompleted);
        EventManager.StartListening("Wood_Completed", OnGameCompleted);
    }

    private void OnTriggerEnter(Collider other)
    {
        object gameName;
        Dictionary<string, object> param = new Dictionary<string, object>();

        switch (other.gameObject.tag)
        {
            case "Bucheron":
                gameName = "Wood Game";
                param.Add("gameName", gameName);
                EventManager.TriggerEvent("Wood_Zone_Entered", param);
                break;
            case "Mineur":
                gameName = "Mine Game";
                param.Add("gameName", gameName);
                EventManager.TriggerEvent("Mine_Zone_Entered", param);
                break;
        }
    }

    private void RespawnCharacter(Dictionary<string, object> param)
    {
        transform.position = m_InitialPosition;
        transform.rotation = m_InitialRotation;
    }

    private void OnGameCompleted(Dictionary<string, object> param)
    {
        object objHasWin;
        if(param != null && param.TryGetValue("hasWin", out objHasWin))
        {
            bool hasWin = (bool)objHasWin;

            if(hasWin)
            {
                AudioManager.SpawnAudioAtLocation(m_VictoryClip, transform.position);
            }
            else
            {
                AudioManager.SpawnAudioAtLocation(m_DefeatClip, transform.position);
            }
        }
    }
}
