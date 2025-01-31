using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class PlayerRobotMovementSoundChannel : SoundChannel
{
	private Timer timer;
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
		
		timer = GetComponent<Timer>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageMusicManager = ObjectMethods.FindComponentOfType<StageMusicManager>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();

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
			timer.timerStartedEvent.AddListener(OnTimerStarted);
			timer.timerFinishedEvent.AddListener(OnTimerFinished);
			
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
			timer.timerStartedEvent.RemoveListener(OnTimerStarted);
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);
			
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

	private void OnTimerStarted()
	{
		audioSource.mute = true;
	}

	private void OnTimerFinished()
	{
		audioSource.mute = stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Over);
	}

	private void OnStageStateChanged(StageState stageState)
	{
		if(stageState != StageState.Interrupted)
		{
			MuteSoundDependingOnStageState(stageState);
		}
		
		StopSoundIfNeeded(stageState);
	}

	private void MuteSoundDependingOnStageState(StageState stageState)
	{
		var stageStatesMutingSound = new List<StageState>
		{
			StageState.Paused,
			StageState.Over
		};
		
		audioSource.mute = stageStatesMutingSound.Contains(stageState);
	}

	private void StopSoundIfNeeded(StageState stageState)
	{
		var stageStatesStoppingSound = new List<StageState>
		{
			StageState.Interrupted,
			StageState.Won
		};
		
		if(!stageStatesStoppingSound.Contains(stageState) || stageSoundManager == null || audioSource.clip != stageSoundManager.GetAudioClipBySoundEffectType(SoundEffectType.PlayerRobotIdle))
		{
			return;
		}

		audioSource.Stop();

		audioSource.clip = null;
	}

	private void OnMusicStoppedPlaying()
	{
		if(stageStateManager != null)
		{
			MuteSoundDependingOnStageState(stageStateManager.GetStageState());
		}
	}

	private void OnSoundPlayed(SoundEffectType soundEffectType)
	{
		var temporarilyMutingSoundEffectTypes = new List<SoundEffectType>
		{
			SoundEffectType.PlayerRobotLifeGain,
			SoundEffectType.BonusSpawn,
			SoundEffectType.BonusCollect
		};

		if(stageSoundManager == null || !temporarilyMutingSoundEffectTypes.Contains(soundEffectType))
		{
			return;
		}

		var audioClip = stageSoundManager.GetAudioClipBySoundEffectType(soundEffectType);

		if(audioClip == null)
		{
			return;
		}

		timer.SetDuration(audioClip.length);
		timer.StartTimer();
	}
}