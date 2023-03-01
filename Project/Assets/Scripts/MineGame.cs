using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineGame : Games
{
    void Start()
    {
        base.Start();
        EventManager.StartListening("Mine_Zone_Entered", OnGameEntered);
        EventManager.StartListening("Mine_Completed", OnGameFinish);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("Mine_Zone_Entered", OnGameEntered);
        EventManager.StopListening("Mine_Completed", OnGameFinish);
    }

    protected override void OnGameEntered(Dictionary<string, object> param)
    {
        m_HasPlayerEnteredGame = true;
        FindObjectOfType<CanvasUI>().GetGameText().text = m_Model.attribute.initialMessage;
    }

    protected override void UpdateGame()
    {
    }

    private void OnGameFinish(Dictionary<string, object> param)
    {
        m_HasGameBegun = false;
        m_HasPlayerEnteredGame = false;
    }
}
