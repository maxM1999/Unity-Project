using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState m_CurrentState;

    [SerializeField]
    private Transform m_CameraTransform;

    [SerializeField]
    private float m_MoveSpeed;

    [SerializeField]
    private List<GameObject> m_CharacterList;

    private GameObject m_CurrentCharacter;

    
    void Start()
    {
        m_CurrentState = new PlayerIdleState(this);
        
        foreach(GameObject character in m_CharacterList)
        {
            if(character.name == "Character_Wandering")
            {
                m_CurrentCharacter = character;
            }
        }

        EventManager.StartListening("Wood_Zone_Entered", OnWoodGameBegin);
        EventManager.StartListening("Mine_Zone_Entered", OnMineGameBegin);
        EventManager.StartListening("Wood_Completed", OnGameTerminates);
        EventManager.StartListening("Mine_Completed", OnGameTerminates);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("Wood_Zone_Entered", OnWoodGameBegin);
        EventManager.StopListening("Mine_Zone_Entered", OnMineGameBegin);
        EventManager.StopListening("Wood_Completed", OnGameTerminates);
        EventManager.StopListening("Mine_Completed", OnGameTerminates);
    }

    void Update()
    {
        m_CurrentState.Execute();
    }

    public void SetState(PlayerState newState)
    {
        m_CurrentState = newState;
    }

    public float GetMoveSpeed()
    {
        return m_MoveSpeed;
    }

    public Transform GetCameraTransform()
    {
        return m_CameraTransform;
    }

    public GameObject GetCurrentCharacter()
    {
        foreach(GameObject character in m_CharacterList)
        {
            if(character.activeInHierarchy)
            {
                return character;
            }
        }
        return null;
    }

    public void ChangeCharacter(string name)
    {
        foreach(GameObject character in m_CharacterList)
        {
            if(character.name == name)
            {
                m_CurrentCharacter.SetActive(false);
                character.SetActive(true);
                m_CurrentCharacter = character;
            }
        }

        switch(m_CurrentCharacter.name)
        {
            case "Character_Wandering":
                SetState(new PlayerIdleState(this));
                break;
            case "Character_Axe":
                SetState(new PlayerChopState(this));
                break;
            case "Character_Pick":
                SetState(new PlayerMineState(this));
                break;
        }
    }

    public PlayerState GetCurrentState()
    {
        return m_CurrentState;
    }

    private void OnWoodGameBegin(Dictionary<string, object> param)
    {
        ChangeCharacter("Character_Axe");

        WoodGame game = FindObjectOfType<WoodGame>();
       
        Quaternion newRotation = Quaternion.LookRotation(-game.transform.forward, Vector3.up);
        m_CurrentCharacter.transform.rotation = newRotation;
    }

    private void OnMineGameBegin(Dictionary<string, object> param)
    {
        ChangeCharacter("Character_Pick");

        MineGame game = FindObjectOfType<MineGame>();

        Quaternion newRotation = Quaternion.LookRotation(-game.transform.forward, Vector3.up);
        m_CurrentCharacter.transform.rotation = newRotation;
    }

    private void OnGameTerminates(Dictionary<string, object> param)
    {
        ChangeCharacter("Character_Wandering");
    }
}
