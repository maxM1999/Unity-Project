using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool m_IsShown = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!m_IsShown)
            {
                m_IsShown = true;
                EventManager.TriggerEvent("ShowPauseMenu", null);
            }
            else
            {
                m_IsShown = false;
                EventManager.TriggerEvent("HidePauseMenu", null);
            }
        }
    }

    public void OnLoadGameClicked()
    {
        PlayerData data = SaveSystem.LoadInventory();

        Dictionary<string, object> param = new Dictionary<string, object>();
        param.Add("data", data);

        EventManager.TriggerEvent("LoadInventory", param);
        EventManager.TriggerEvent("RespawnCharacter", null);
        EventManager.TriggerEvent("HidePauseMenu", null);
    }

    public void OnEndGameClicked()
    {
        EventManager.TriggerEvent("Victory", null);
    }
}
