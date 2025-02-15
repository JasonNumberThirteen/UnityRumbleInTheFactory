using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class PlayerRobotMovementSoundChannel : SoundChannel
{
	private Timer timer;
	private StageSoundManager stageSoundManager;

	public override bool CanPlaySounds() => true;

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
		timer = GetComponent<Timer>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();

		base.Awake();
	}

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);
		
		if(register)
		{
			timer.timerStartedEvent.AddListener(OnTimerStarted);
			timer.timerFinishedEvent.AddListener(OnTimerFinished);
			
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
			timer.timerStartedEvent.RemoveListener(OnTimerStarted);
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);
			
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

	private void StopSoundIfNeeded(StageState stageState)
	{
		if(stageState != StageState.Won || stageSoundManager == null || audioSource.clip != stageSoundManager.GetAudioClipBySoundEffectType(SoundEffectType.PlayerRobotIdle))
		{
			return;
		}

		audioSource.Stop();

		audioSource.clip = null;
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

		if(audioClip != null)
		{
			timer.StartTimerWithSetDuration(audioClip.length);
		}
	}
}