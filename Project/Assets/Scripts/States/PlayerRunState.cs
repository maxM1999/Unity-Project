using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    private Vector3 m_Direction;


    public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        if(stateMachine != null)
        {
            m_OwnerTransform = stateMachine.GetComponent<Transform>();
            m_OwnerRb = stateMachine.GetComponent<Rigidbody>();
            stateMachine.GetCurrentCharacter().GetComponent<Animator>().SetBool("IsRunning", true);
        }
    }
    public override void Execute()
    {
        bool isKeyAPressed = Input.GetKeyUp(KeyCode.A);
        bool isKeyWPressed = Input.GetKeyUp(KeyCode.W);
        bool isKeySPressed = Input.GetKeyUp(KeyCode.S);
        bool isKeyDPressed = Input.GetKeyUp(KeyCode.D);
        if (isKeyAPressed || isKeyWPressed || isKeySPressed || isKeyDPressed)
        {
            m_OwnerRb.velocity = Vector3.zero;
            m_StateMachine.SetState(new PlayerIdleState(m_StateMachine));
            return;
        }

        UpdateDirection();
        Move();
        OrientCharacterToMovement();
    }

    void UpdateDirection()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 cameraFwd = m_StateMachine.GetCameraTransform().forward;
        Vector3 cameraRight = m_StateMachine.GetCameraTransform().right;
        cameraFwd.y = 0;
        cameraRight.y = 0;
        cameraFwd.Normalize();
        cameraRight.Normalize();
        m_Direction = cameraFwd * verticalInput + cameraRight * horizontalInput;
    }

    void Move()
    {
        if (m_StateMachine != null)
        {
            float speed = m_StateMachine.GetMoveSpeed();
            Vector3 newVelo = m_Direction * speed;
            m_OwnerRb.velocity = newVelo;
        }
    }

    private void OrientCharacterToMovement()
    {
        Quaternion newRotation = Quaternion.LookRotation(m_Direction, Vector3.up);
        m_OwnerTransform.rotation = Quaternion.RotateTowards(m_OwnerTransform.rotation, newRotation, 700 * Time.deltaTime);
    }
}
