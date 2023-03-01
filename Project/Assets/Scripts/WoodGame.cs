using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodGame : Games
{
    /* Le minimum de coup de hache à avoir pour gagner. */
    [SerializeField]
    private int m_ChopCountToHave = 5;

    private float m_MaxTime = 5f;

    private float m_Elapsed = 5f;

    private int m_PlayerChopCount = 0;

    void Start()
    {
        base.Start();
        EventManager.StartListening("Wood_Zone_Entered", OnGameEntered);
        EventManager.StartListening("Chop", OnPlayerChop);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("Wood_Zone_Entered", OnGameEntered);
        EventManager.StopListening("Chop", OnPlayerChop);
    }

    void Update()
    {
        UpdateGame();
    }

    protected override void OnGameEntered(Dictionary<string, object> param)
    {
        m_HasPlayerEnteredGame = true;
        SetUiComponents();
    }

    protected override void UpdateGame()
    {
        if(!m_HasGameBegun)
        {
            return;
        }

        m_Elapsed -= Time.deltaTime;
        if(m_Elapsed <= 0f)
        {
            m_Elapsed = m_MaxTime;
            m_HasPlayerEnteredGame = false;
            m_HasGameBegun = false;

            FindObjectOfType<CanvasUI>().GetCountdown().text = m_MaxTime.ToString();
            TerminateWoodGame();
        }
        else
        {
            int elapsedTimeCeiled = Mathf.CeilToInt(m_Elapsed);
            FindObjectOfType<CanvasUI>().GetCountdown().text = elapsedTimeCeiled.ToString();
        }
    }

    private void SetUiComponents()
    {
        CanvasUI ui = FindObjectOfType<CanvasUI>();
        ui.GetGameText().text = m_Model.attribute.initialMessage;
        ui.GetCountdown().text = m_Elapsed.ToString();
    }

    private void OnPlayerChop(Dictionary<string, object> param)
    {
        object chopCount;
        if(param.TryGetValue("chopCount", out chopCount))
        {
            m_PlayerChopCount = (int)chopCount;
        }
    }

    private void TerminateWoodGame()
    {
        bool hasPlayerWin = false;
        if (m_PlayerChopCount >= 5)
        {
            hasPlayerWin = true;
        }
        Dictionary<string, object> param = new Dictionary<string, object>();
        param.Add("hasWin", hasPlayerWin);
        EventManager.TriggerEvent("Wood_Completed", param); 
    }
}
