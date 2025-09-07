using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static InputController s_Instance;

    [SerializeField]
    private PlayerController m_playerController;

    [SerializeField]
    private PlayerInput m_playerInput;

    private InputActionMap m_movementInputActionMap;
    private InputActionMap m_songInputActionMap;

    private InputAction m_movementInputAction;

    private const string MOVEMENT_MAP_NAME = "WalkControls";
    private const string SONG_MAP_NAME = "GameControls";
    private const string MOVEMENT_ACTION_NAME = "Movement";

    private void Awake()
    {
        s_Instance = this;

        m_movementInputActionMap = m_playerInput.actions.FindActionMap(MOVEMENT_MAP_NAME);
        m_songInputActionMap = m_playerInput.actions.FindActionMap(SONG_MAP_NAME);

        m_movementInputAction = m_movementInputActionMap.FindAction(MOVEMENT_ACTION_NAME);

        m_playerInput.currentActionMap = m_movementInputActionMap;
        m_movementInputActionMap.Enable();
        m_songInputActionMap.Disable();
    }

    private void Update()
    {
        if (m_playerInput.currentActionMap == m_movementInputActionMap)
        {
            object movementDirection = m_movementInputAction.ReadValueAsObject();
            if (movementDirection != null)
            {
                m_playerController.MovePlayer((Vector2)movementDirection);
            }
            else
            {
                m_playerController.StopPlayerMovement();
            }
        }
    }

    #region InputMapSelection
    public void SetMovementInputMap()
    {
        m_playerInput.currentActionMap = m_movementInputActionMap;
        m_movementInputActionMap.Enable();
        m_songInputActionMap.Disable();
    }

    public void DisableMovementInputMap()
    {
        m_playerInput.currentActionMap = null;
        m_movementInputActionMap.Disable();
        m_songInputActionMap.Disable();
    }

    public void SetSongRecordingInputMap()
    {
        m_playerInput.currentActionMap = m_songInputActionMap;
        m_movementInputActionMap.Disable();
        m_songInputActionMap.Enable();
    }
    #endregion

    #region QuestActions
    public void StartQuest(InputAction.CallbackContext _value)
    {
        m_playerController.StartInteraction();
    }
    #endregion

    #region ToneActions
    public void Tone1(InputAction.CallbackContext _value)
    {
        HandleToneKey(_value, 1);
    }
    public void Tone2(InputAction.CallbackContext _value)
    {
        HandleToneKey(_value, 2);
    }
    public void Tone3(InputAction.CallbackContext _value)
    {
        HandleToneKey(_value, 3);
    }
    public void Tone4(InputAction.CallbackContext _value)
    {
        HandleToneKey(_value, 4);
    }
    public void Tone5(InputAction.CallbackContext _value)
    {
        HandleToneKey(_value, 5);
    }
    public void Tone6(InputAction.CallbackContext _value)
    {
        HandleToneKey(_value, 6);
    }
    public void Tone7(InputAction.CallbackContext _value)
    {
        HandleToneKey(_value, 7);
    }
    public void HandleToneKey(InputAction.CallbackContext _value, int _toneNumber)
    {
        if (_value.started)
        {
            //Debug.Log("Starting Note Recording");
            m_playerController.StartRecordingTone(_toneNumber);
        }
        else if (_value.canceled)
        {
            //Debug.Log("Ending Note Recording");
            m_playerController.EndRecordingTone((float)_value.duration);
        }
    }
    #endregion

    #region RecordingActions
    public void ChangeInstrument(InputAction.CallbackContext _value)
    {
        if (_value.started)
        {
            m_playerController.ChangeInstrument();
        }
    }

    public void AdvanceRecordingPhase(InputAction.CallbackContext _value)
    {
        if (_value.started)
        {
            m_playerController.AdvanceRecordingPhase();
        }
    }

    public void UndoRecording(InputAction.CallbackContext _value)
    {
        if (_value.started)
        {
            m_playerController.UndoRecording();
        }
    }

    public void PlayRecordedSong(InputAction.CallbackContext _value)
    {
        if (_value.started)
        {
            m_playerController.PlaySong();
        }
    }
    #endregion
}
