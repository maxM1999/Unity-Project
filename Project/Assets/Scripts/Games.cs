using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Games : MonoBehaviour
{
    [SerializeField]
    protected GameModel m_Model;

    protected bool m_HasPlayerEnteredGame = false;

    protected bool m_HasGameBegun = false;

    protected void Start()
    {
        EventManager.StartListening("Continue_Button_Clicked", OnContinueButtonClicked);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("Continue_Button_Clicked", OnContinueButtonClicked);
    }

    protected abstract void OnGameEntered(Dictionary<string, object> param);

    protected void UpdateRulesPresentation()
    {
        if (!m_HasPlayerEnteredGame)
        {
            return;
        }

        int currRuleIdx = -1;
        string currRuleDisplayed = FindObjectOfType<CanvasUI>().GetGameText().text.ToString();

        for (int i = 0; i < m_Model.attribute.rules.Length; i++)
        {
            if (currRuleDisplayed == m_Model.attribute.rules[i])
            {
                currRuleIdx = i;
            }
        }

        int newIdx = currRuleIdx + 1;
        if (newIdx == m_Model.attribute.rules.Length)
        {
            m_HasGameBegun = true;
            DesactivateCanvasGameText();
            TriggerGameBegin();
            return;
        }

        FindObjectOfType<CanvasUI>().GetGameText().text = m_Model.attribute.rules[newIdx];
    }

    protected abstract void UpdateGame();

    protected void OnContinueButtonClicked(Dictionary<string, object> param)
    {
        if (!m_HasPlayerEnteredGame)
        {
            return;
        }

        UpdateRulesPresentation();
    }

    private void TriggerGameBegin()
    {
        switch(m_Model.attribute.name)
        {
            case "Wood Game":
                EventManager.TriggerEvent("Wood_Game_Begin", null);
                break;
            case "Dig Game":
                EventManager.TriggerEvent("Dig_Game_Begin", null);
                break;
            case "Mine Game":
                EventManager.TriggerEvent("Mine_Game_Begin", null);
                break;
        }
    }

    private void DesactivateCanvasGameText()
    {
        FindObjectOfType<CanvasUI>().GetGameText().text = "";
        FindObjectOfType<CanvasUI>().GetGameText().gameObject.SetActive(false);
    }
}
