using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMineState : PlayerState
{
    private bool m_CanPressSpace = true;
    public PlayerMineState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        m_OwnerRb = m_StateMachine.GetComponent<Rigidbody>();
        m_OwnerRb.velocity = Vector3.zero;
        EventManager.StartListening("Mine_Game_Begin", OnMineGameBegin);
    }

    ~PlayerMineState()
    {
        EventManager.StopListening("Mine_Game_Begin", OnMineGameBegin);
    }
    
    public override void Execute()
    {
        bool spacePressed = Input.GetKeyDown(KeyCode.Space);

        if(m_CanPressSpace && spacePressed)
        {
            EventManager.TriggerEvent("StopBlueCircle", null);
            m_StateMachine.GetCurrentCharacter().GetComponent<Animator>().SetTrigger("StartMineGame");
            m_CanPressSpace = false;
        }
    }

    private void OnMineGameBegin(Dictionary<string, object> param)
    {
        m_StateMachine.GetCurrentCharacter().GetComponent<Animator>().SetTrigger("StartMineGame");
    }
}
