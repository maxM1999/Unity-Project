using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* State dans lequel le joueur se trouve quand il joue au jeu de bucheron */
public class PlayerChopState : PlayerState
{
    private bool m_CanChop = true;
    /* Nombre de coups de hache que le joueur réussit à faire. */
    private int m_ChopCount = 0;

    bool m_HasGameBegun = false;
    
    public PlayerChopState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        m_OwnerRb = stateMachine.GetComponent<Rigidbody>();
        m_OwnerRb.velocity = Vector3.zero;
        EventManager.StartListening("Wood_Game_Begin", OnGameBegin);
    }
    ~PlayerChopState()
    {
        EventManager.StopListening("Wood_Game_Begin", OnGameBegin);
    }

    public override void Execute()
    {
        bool Space = Input.GetKeyDown(KeyCode.Space);

        if(Space && m_CanChop && m_HasGameBegun)
        {
            Chop();
            m_StateMachine.GetCurrentCharacter().GetComponent<Animator>().SetTrigger("Chops");
            m_CanChop = false;
        }
    }

    public void SetCanChop()
    {
        m_CanChop = true;
    }

    private void Chop()
    {
        m_ChopCount++;
        Dictionary<string, object> param = new Dictionary<string, object>();
        param.Add("chopCount", m_ChopCount);
        EventManager.TriggerEvent("Chop", param);
    }

    private void OnGameBegin(Dictionary<string, object> param)
    {
        m_HasGameBegun = true;
    }
}
