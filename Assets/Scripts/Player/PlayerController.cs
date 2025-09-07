using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SongRecordingController m_songRecordingController;
    [SerializeField]
    private SongController m_songController;

    [SerializeField]
    private float m_movementSpeed;

    [SerializeField]
    private CharacterController m_characterController;

    [SerializeField]
    private AudioSource m_footstepAudioSource;
    [SerializeField]
    private AudioSource m_teleportAudioSource;

    private IInteractable m_subscribedInteractable;

    #region Movement
    public void MovePlayer(Vector2 _direction)
    {
        m_characterController.Move(new Vector3(_direction.x, 0, _direction.y) * m_movementSpeed * Time.deltaTime);
        if (!m_footstepAudioSource.isPlaying)
        {
            m_footstepAudioSource.Play();
        }
    }

    public void StopPlayerMovement()
    {
        m_footstepAudioSource.Stop();
    }

    public void TeleportPlayer(Vector3 _location)
    {
        m_characterController.enabled = false;
        transform.position = _location;
        m_characterController.enabled = true;
        m_teleportAudioSource.Play();
    }

    public void EnablePlayerMovement()
    {
        InputController.s_Instance.SetMovementInputMap();
    }

    public void DisablePlayerMovement()
    {
        StopPlayerMovement();
        InputController.s_Instance.DisableMovementInputMap();
    }
    #endregion

    #region SongRecording
    public void ChangeInstrument()
    {
        m_songRecordingController.ChangeInstrument();
    }

    public void StartRecordingTone(int _toneNumber)
    {
        m_songRecordingController.StartRecordingTone(_toneNumber);
    }

    public void EndRecordingTone(float _duration)
    {
        m_songRecordingController.EndRecordingTone(_duration);
    }

    public void UndoRecording()
    {
        m_songRecordingController.UndoRecording();
    }

    public void StartRecordingSong()
    {
        InputController.s_Instance.SetSongRecordingInputMap();
        m_songRecordingController.StartRecordingSong();
    }

    public void AdvanceRecordingPhase()
    {
        if (m_songRecordingController.AdvanceRecordingPhase())
        {
            StartCoroutine(EndSongRecording());
        }
    }

    private IEnumerator EndSongRecording()
    {
        Song completedSong = m_songRecordingController.GetRecordedSong();
        m_songController.PlaySong(completedSong);
        yield return new WaitForSeconds(completedSong.GetSongLength());
        ((QuestgiverController)m_subscribedInteractable).TestSong(m_songRecordingController.GetRecordedSong());
    }

    public void PlaySong()
    {
        m_songController.PlaySong(m_songRecordingController.GetRecordedSong());
    }
    #endregion

    #region Interaction
    public void SubscribeQuestgiver(IInteractable _interactableController)
    {
        m_subscribedInteractable = _interactableController;
    }

    public void UnsubscribeQuestgiver(IInteractable _interactableController)
    {
        if(m_subscribedInteractable == _interactableController)
        {
            m_subscribedInteractable = null;
        }
    }

    public void StartInteraction()
    {
        if(m_subscribedInteractable != null)
        {
            m_subscribedInteractable.StartInteraction();
            DisablePlayerMovement();
        }
    }

    public void EndInteraction()
    {
        EnablePlayerMovement();
    }
    #endregion

    #region QuestRewards
    public void AddInstrument(InstrumentSO _instrument)
    {
        m_songRecordingController.AddInstrument(_instrument);
    }

    public void AddScale(ScaleSO _scale)
    {
        m_songRecordingController.AddScale(_scale);
    }

    public void IncreaseTrackCount(int _trackIncreaseCount)
    {
        m_songRecordingController.AddSongTracks(_trackIncreaseCount);
    }
    #endregion
}
