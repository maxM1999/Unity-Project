using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictoryState : PlayerState
{
    public PlayerVictoryState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.GetCurrentCharacter().GetComponent<Animator>().SetTrigger("Victory");
    }
    public override void Execute()
    {
        m_StateMachine.StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2);
        RespawnCharacterToInitialPosition();
        ReinitializeInventory();
        m_StateMachine.SetState(new PlayerIdleState(m_StateMachine));
        EventManager.TriggerEvent("HidePauseMenu", null);
    }

    private void RespawnCharacterToInitialPosition()
    {
        EventManager.TriggerEvent("RespawnCharacter", null);
    }

    private void ReinitializeInventory()
    {
        EventManager.TriggerEvent("ReinitializeInventory", null);
    }
}
