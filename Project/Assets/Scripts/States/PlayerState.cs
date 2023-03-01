using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState 
{
    protected PlayerStateMachine m_StateMachine;
    protected Transform m_OwnerTransform;
    protected Rigidbody m_OwnerRb;

    public PlayerState(PlayerStateMachine stateMachine)
    {
        m_StateMachine = stateMachine;
    }
    public abstract void Execute();
}
