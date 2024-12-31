using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerRobotMovementSoundChannel : MonoBehaviour
{
	private AudioSource audioSource;
	private StageStateManager stageStateManager;
	private StageSoundManager stageSoundManager;
	private float initialVolume;

	public AudioSource GetAudioSource() => audioSource;

	public void Play(AudioClip audioClip)
	{
		if(audioSource.clip == audioClip)
		{
			return;
		}
		
		audioSource.clip = audioClip;

		audioSource.Play();
	}

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		stageStateManager = FindAnyObjectByType<StageStateManager>();
		stageSoundManager = FindAnyObjectByType<StageSoundManager>();
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

			if(stageSoundManager != null)
			{
				stageSoundManager.soundPlayedEvent.AddListener(OnSoundPlayed);
			}
		}
		else
		{
			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}

			if(stageSoundManager != null)
			{
				stageSoundManager.soundPlayedEvent.RemoveListener(OnSoundPlayed);
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

	private void OnSoundPlayed(SoundEffectType soundEffectType)
	{
		if(soundEffectType == SoundEffectType.BonusSpawn)
		{
			MuteTemporarily(1);
		}
		else if(soundEffectType == SoundEffectType.BonusCollect)
		{
			MuteTemporarily(0.9f);
		}
	}

	private void MuteTemporarily(float duration)
	{
		audioSource.volume = 0;

		Invoke(nameof(RestoreVolume), duration);
	}

	private void RestoreVolume()
	{
		audioSource.volume = initialVolume;
	}
}