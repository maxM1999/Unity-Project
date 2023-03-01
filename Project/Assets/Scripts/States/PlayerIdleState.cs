using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private bool m_IsPauseMenuShown = false;
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        if(stateMachine != null)
        {
            stateMachine.GetCurrentCharacter().GetComponent<Animator>().SetBool("IsRunning", false);
        }

        EventManager.StartListening("ShowPauseMenu", LockPlayerMovement);
        EventManager.StartListening("HidePauseMenu", UnlockPlayerMovement);
        EventManager.StartListening("Victory", OnVictory);
    }

    ~PlayerIdleState()
    {
        EventManager.StopListening("ShowPauseMenu", LockPlayerMovement);
        EventManager.StopListening("HidePauseMenu", UnlockPlayerMovement);
        EventManager.StopListening("Victory", OnVictory);
    }
    public override void Execute()
    {
        if(m_IsPauseMenuShown)
        {
            return;
        }

        bool isKeyAPressed = Input.GetKey(KeyCode.A);
        bool isKeyWPressed = Input.GetKey(KeyCode.W);
        bool isKeySPressed = Input.GetKey(KeyCode.S);
        bool isKeyDPressed = Input.GetKey(KeyCode.D);
        if (isKeyAPressed || isKeyWPressed || isKeySPressed || isKeyDPressed)
        {
            m_StateMachine.SetState(new PlayerRunState(m_StateMachine));
        }
    }

    private void LockPlayerMovement(Dictionary<string, object> param)
    {
        m_IsPauseMenuShown = true;
    }

    private void UnlockPlayerMovement(Dictionary<string, object> param)
    {
        m_IsPauseMenuShown = false;
    }

    private void OnVictory(Dictionary<string, object> param)
    {
        m_StateMachine.SetState(new PlayerVictoryState(m_StateMachine));
    }
}
