using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingCircleUI : MonoBehaviour
{
    [SerializeField]
    private float m_MoveSpeed;

    [SerializeField]
    private float m_StaticCirclePositionX;

    private Vector3 m_InitialPosition;

    private bool m_CanMove = false;

    private float m_AcceptanceRange = 15f;

    private void Start()
    {
        m_InitialPosition = GetComponent<RectTransform>().localPosition;
        EventManager.StartListening("StopBlueCircle", OnStopBlueCircle);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("StopBlueCircle", OnStopBlueCircle);
    }

    private void Update()
    {
        if(!m_CanMove)
        {
            return;
        }

        GetComponent<RectTransform>().Translate(Vector3.right * m_MoveSpeed * Time.deltaTime);

        if (GetComponent<RectTransform>().localPosition.x > m_StaticCirclePositionX + 50f)
        {
            GetComponent<RectTransform>().localPosition = m_InitialPosition;
        }
    }

    public void SetCanMove(bool state)
    {
        m_CanMove = state;
    }

    private void OnStopBlueCircle(Dictionary<string, object> param)
    {
        bool hasPlayerWin = false;
        m_CanMove = false;

        Dictionary<string, object> param2 = new Dictionary<string, object>();
        

        if (GetComponent<RectTransform>().localPosition.x < m_StaticCirclePositionX + m_AcceptanceRange && GetComponent<RectTransform>().localPosition.x > m_StaticCirclePositionX - m_AcceptanceRange)
        {
            hasPlayerWin = true;
            param2.Add("hasWin", hasPlayerWin);
            GetComponent<Animator>().SetBool("mustFlashGreen", true);
        }
        else
        {
            param2.Add("hasWin", hasPlayerWin);
            GetComponent<Animator>().SetBool("mustFlashBlack", true);
        }

        EventManager.TriggerEvent("MineGameResult", param2);

        StartCoroutine(StopMineGameCoroutine(param2));
    }

    private void ResetPosition()
    {
        GetComponent<RectTransform>().localPosition = m_InitialPosition;
    }

    private IEnumerator StopMineGameCoroutine(Dictionary<string, object> param)
    {
        yield return new WaitForSeconds(2);
        EventManager.TriggerEvent("Mine_Completed", param);
        ResetPosition();
        GetComponent<Image>().color = Color.white;
        GetComponent<Animator>().SetBool("mustFlashGreen", false);
        GetComponent<Animator>().SetBool("mustFlashBlack", false);
    }
}
