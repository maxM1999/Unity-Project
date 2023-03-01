using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAnimation : MonoBehaviour
{
    [SerializeField]
    private PlayerStateMachine m_StateMachine;
    

    public void SetCanChop()
    {
        PlayerState currState = m_StateMachine.GetCurrentState();
        if(currState != null && currState.GetType() == typeof(PlayerChopState))
        {
            PlayerChopState chopState = (PlayerChopState)currState;
            chopState.SetCanChop();
        }
    }
}
