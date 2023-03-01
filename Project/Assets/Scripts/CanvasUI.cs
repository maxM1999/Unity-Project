using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour
{
    [SerializeField]
    private Text m_GameText;

    [SerializeField]
    private Text m_Countdown;

    [SerializeField]
    private Text m_ChopCnt;

    [SerializeField]
    private Image m_HacheImg;

    [SerializeField]
    private Image m_StaticCircle;

    [SerializeField]
    private Image m_MovingCircle;

    [SerializeField]
    private GameObject m_PauseMenu;

    void Start()
    {
        EventManager.StartListening("Wood_Zone_Entered", OnWoodZoneEntered);
        EventManager.StartListening("Mine_Zone_Entered", OnMineZoneEntered);

        EventManager.StartListening("Mine_Game_Begin", OnMineGameBegins);

        EventManager.StartListening("Wood_Completed", OnWoodCompleted);
        EventManager.StartListening("Mine_Completed", OnMineCompleted);
        EventManager.StartListening("Chop", OnPlayerChop);

        EventManager.StartListening("ShowPauseMenu", ShowPauseMenu);
        EventManager.StartListening("HidePauseMenu", HidePauseMenu);
        EventManager.StartListening("InventoryFull", EnableEndGameButton);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("Wood_Zone_Entered", OnWoodZoneEntered);
        EventManager.StopListening("Mine_Zone_Entered", OnMineZoneEntered);

        EventManager.StopListening("Mine_Game_Begin", OnMineGameBegins);

        EventManager.StopListening("Wood_Completed", OnWoodCompleted);
        EventManager.StopListening("Mine_Completed", OnMineCompleted);
        EventManager.StopListening("Chop", OnPlayerChop);

        EventManager.StopListening("ShowPauseMenu", ShowPauseMenu);
        EventManager.StopListening("HidePauseMenu", HidePauseMenu);
        EventManager.StopListening("InventoryFull", EnableEndGameButton);
    }

    public Text GetGameText()
    {
        return m_GameText;
    }

    public Text GetCountdown()
    {
        return m_Countdown;
    }

    public Image GetHacheImg()
    {
        return m_HacheImg;
    }

    public Text GetChopCntText()
    {
        return m_ChopCnt;
    }

    public void OnContinueButtonClicked()
    {
        EventManager.TriggerEvent("Continue_Button_Clicked", null);
    }

    private void OnPlayerChop(Dictionary<string, object> param)
    {
        object chopCount;
        if(param.TryGetValue("chopCount", out chopCount))
        {
            int chopCountInt = (int)chopCount;
            m_ChopCnt.text = chopCountInt.ToString();
        }
    }

    private void OnWoodZoneEntered(Dictionary<string, object> param)
    {
        m_ChopCnt.gameObject.SetActive(true);
        m_Countdown.gameObject.SetActive(true);
        m_GameText.gameObject.SetActive(true);
        m_HacheImg.gameObject.SetActive(true);
    }

    private void OnMineZoneEntered(Dictionary<string, object> param)
    {
        m_GameText.gameObject.SetActive(true);
    }

    private void OnMineGameBegins(Dictionary<string, object> param)
    {
        m_StaticCircle.gameObject.SetActive(true);
        m_MovingCircle.gameObject.SetActive(true);
        m_MovingCircle.GetComponent<MovingCircleUI>().SetCanMove(true);
    }

    private void OnWoodCompleted(Dictionary<string, object> param)
    {
        m_ChopCnt.text = '0'.ToString();
        m_ChopCnt.gameObject.SetActive(false);
        m_Countdown.gameObject.SetActive(false);
        m_GameText.gameObject.SetActive(false);
        m_HacheImg.gameObject.SetActive(false);
    }

    private void OnMineCompleted(Dictionary<string, object> param)
    {
        m_StaticCircle.gameObject.SetActive(false);
        m_MovingCircle.gameObject.SetActive(false);
    }

    private void ShowPauseMenu(Dictionary<string, object> param)
    {
        for(int i = 0; i < m_PauseMenu.transform.childCount; i++)
        {
            m_PauseMenu.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void HidePauseMenu(Dictionary<string, object> param)
    {
        for (int i = 0; i < m_PauseMenu.transform.childCount; i++)
        {
            m_PauseMenu.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void EnableEndGameButton(Dictionary<string, object> param)
    {
        for (int i = 0; i < m_PauseMenu.transform.childCount; i++)
        {
            if(m_PauseMenu.transform.GetChild(i).name == "EndGameButton")
            {
                Button endGameButton = m_PauseMenu.transform.GetChild(i).GetComponent<Button>();
                endGameButton.interactable = true;
            }
        }
    }
}
