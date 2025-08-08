using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundChannel : MonoBehaviour
{
	protected AudioSource audioSource;

	protected StageMusicManager stageMusicManager;
	protected StageStateManager stageStateManager;

	public virtual bool CanPlaySounds() => stageMusicManager == null || !stageMusicManager.MusicIsPlaying();

	public bool SoundIsPlaying() => audioSource.isPlaying;

	public virtual void Play(AudioClip audioClip)
	{
		if(audioClip != null && CanPlaySounds())
		{
			audioSource.PlayOneShot(audioClip);
		}
	}

	protected virtual void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		stageMusicManager = ObjectMethods.FindComponentOfType<StageMusicManager>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		audioSource.mute = true;

		RegisterToListeners(true);
	}

	protected virtual void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(stageMusicManager != null)
			{
				stageMusicManager.musicStoppedPlayingEvent.AddListener(OnMusicStoppedPlaying);
			}

			if(stageStateManager != null)
			{
				stageStateManager.stageStateWasChangedEvent.AddListener(OnStageStateWasChanged);
			}
		}
		else
		{
			if(stageMusicManager != null)
			{
				stageMusicManager.musicStoppedPlayingEvent.RemoveListener(OnMusicStoppedPlaying);
			}

			if(stageStateManager != null)
			{
				stageStateManager.stageStateWasChangedEvent.RemoveListener(OnStageStateWasChanged);
			}
		}
	}

	protected void MuteSoundDependingOnStageState(StageState stageState)
	{
		var stageStatesMutingSound = new List<StageState>
		{
			StageState.Paused,
			StageState.Over
		};

		audioSource.mute = stageStatesMutingSound.Contains(stageState);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void OnMusicStoppedPlaying()
	{
		if(stageStateManager != null)
		{
			MuteSoundDependingOnStageState(stageStateManager.GetStageState());
		}
	}

	private void OnStageStateWasChanged(StageState stageState)
	{
		MuteSoundDependingOnStageState(stageState);
	}
}