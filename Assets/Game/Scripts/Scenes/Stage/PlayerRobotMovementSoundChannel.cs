using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerRobotMovementSoundChannel : MonoBehaviour
{
	private AudioSource audioSource;
	private StageStateManager stageStateManager;
	private float initialVolume;

	public void Play(AudioClip audioClip)
	{
		if(audioSource.clip == audioClip)
		{
			return;
		}
		
		audioSource.clip = audioClip;

		audioSource.Play();
	}

	public void MuteTemporarily(float duration)
	{
		audioSource.volume = 0;

		Invoke(nameof(RestoreVolume), duration);
	}

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		stageStateManager = FindAnyObjectByType<StageStateManager>();
		initialVolume = audioSource.volume;

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}
		}
		else
		{
			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}
		}
	}

	private void OnStageStateChanged(StageState stageState)
	{
		if(stageState == StageState.Over)
		{
			audioSource.Stop();
		}
		if(stageState == StageState.Paused)
		{
			audioSource.Pause();
		}
		else
		{
			audioSource.UnPause();
		}
	}

	private void RestoreVolume()
	{
		audioSource.volume = initialVolume;
	}
}