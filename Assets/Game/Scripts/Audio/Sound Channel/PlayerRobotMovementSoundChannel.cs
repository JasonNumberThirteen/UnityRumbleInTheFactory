using System.Collections.Generic;
using UnityEngine;

public class PlayerRobotMovementSoundChannel : SoundChannel
{
	private StageStateManager stageStateManager;
	private StageMusicManager stageMusicManager;
	private StageSoundManager stageSoundManager;

	public override void Play(AudioClip audioClip)
	{
		if(audioSource.clip == audioClip)
		{
			return;
		}
		
		audioSource.clip = audioClip;

		audioSource.Play();
	}

	protected override void Awake()
	{
		base.Awake();
		
		stageStateManager = FindAnyObjectByType<StageStateManager>();
		stageMusicManager = FindAnyObjectByType<StageMusicManager>();
		stageSoundManager = FindAnyObjectByType<StageSoundManager>();

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

			if(stageMusicManager != null)
			{
				stageMusicManager.musicStoppedPlayingEvent.AddListener(OnMusicStoppedPlaying);
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

			if(stageMusicManager != null)
			{
				stageMusicManager.musicStoppedPlayingEvent.RemoveListener(OnMusicStoppedPlaying);
			}

			if(stageSoundManager != null)
			{
				stageSoundManager.soundPlayedEvent.RemoveListener(OnSoundPlayed);
			}
		}
	}

	private void OnStageStateChanged(StageState stageState)
	{
		var stageStatesMutingSound = new List<StageState>
		{
			StageState.Paused,
			StageState.Over
		};
		
		audioSource.mute = stageStatesMutingSound.Contains(stageState);
	}

	private void OnMusicStoppedPlaying()
	{
		audioSource.mute = stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Over);
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
		audioSource.mute = true;

		Invoke(nameof(RestoreVolume), duration);
	}

	private void RestoreVolume()
	{
		audioSource.mute = false;
	}
}