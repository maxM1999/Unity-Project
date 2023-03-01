using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineCamera : MonoBehaviour
{
    private float m_XAxisValue;
    private float m_YAxisValue;
 
    void Start()
    {
        m_XAxisValue = GetComponent<Cinemachine.CinemachineFreeLook>().m_XAxis.m_MaxSpeed;
        m_YAxisValue = GetComponent<Cinemachine.CinemachineFreeLook>().m_YAxis.m_MaxSpeed;

        EventManager.StartListening("Wood_Zone_Entered", FreezeCamera);
        EventManager.StartListening("Mine_Zone_Entered", FreezeCamera);
        EventManager.StartListening("ShowPauseMenu", FreezeCamera);
        EventManager.StartListening("Wood_Completed", UnFreezeCamera);
        EventManager.StartListening("Mine_Completed", UnFreezeCamera);
        EventManager.StartListening("HidePauseMenu", UnFreezeCamera);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("Wood_Zone_Entered", FreezeCamera);
        EventManager.StopListening("Mine_Zone_Entered", FreezeCamera);
        EventManager.StopListening("ShowPauseMenu", FreezeCamera);
        EventManager.StopListening("Wood_Completed", UnFreezeCamera);
        EventManager.StopListening("Mine_Completed", UnFreezeCamera);
        EventManager.StopListening("HidePauseMenu", UnFreezeCamera);
    }
    private void FreezeCamera(Dictionary<string, object> param)
    {
        GetComponent<Cinemachine.CinemachineFreeLook>().m_YAxis.m_MaxSpeed = 0;
        GetComponent<Cinemachine.CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 0;

        object gameName;
        if(param != null && param.TryGetValue("gameName", out gameName))
        {
            string gameNameStr = (string)gameName;

            switch(gameNameStr)
            {
                case "Wood Game":
                    
                    break;
                case "Mine Game":
                    break;
                case "Dig Game":
                    break;
            }
        }
    }

    private void UnFreezeCamera(Dictionary<string, object> param)
    {
        GetComponent<Cinemachine.CinemachineFreeLook>().m_YAxis.m_MaxSpeed = m_YAxisValue;
        GetComponent<Cinemachine.CinemachineFreeLook>().m_XAxis.m_MaxSpeed = m_XAxisValue;
    }
}
