using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class StageSceneFlowManager : MonoBehaviour
{
	public UnityEvent stageStartedEvent;
	public UnityEvent stageWasActivatedEvent;
	
	[SerializeField] private GameData gameData;
	[SerializeField] private GameObject[] gosToActivateWhenStageIsActivated;
	[SerializeField, Min(0f)] private float delayOnStart = 1.5f;
	[SerializeField, Min(0f)] private float delayAfterInterrupting = 1f;
	
	private Timer timer;
	private NukeEntity nukeEntity;
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;
	private StageStateManager stageStateManager;
	private StageSoundManager stageSoundManager;
	private DataSerialisationManager dataSerialisationManager;

	public void PauseGameIfPossible()
	{
		var stageStatesBlockingPause = new List<StageState>
		{
			StageState.Interrupted,
			StageState.Won,
			StageState.Over
		};
		
		if(stageStateManager == null || stageStatesBlockingPause.Contains(stageStateManager.GetStageState()))
		{
			return;
		}

		var stateToSwitch = stageStateManager.StateIsSetTo(StageState.Active) ? StageState.Paused : StageState.Active;

		stageStateManager.SetStateTo(stateToSwitch);
		PlaySoundIfNeeded(stageStateManager.StateIsSetTo(StageState.Paused));
	}

	public void SetGameAsOverIfNeeded()
	{
		if(stageStateManager != null && !stageStateManager.StateIsSetTo(StageState.Over))
		{
			stageStateManager.SetStateTo(StageState.Over);
		}
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		nukeEntity = ObjectMethods.FindComponentOfType<NukeEntity>();
		translationBackgroundPanelUI = ObjectMethods.FindComponentOfType<TranslationBackgroundPanelUI>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();
		dataSerialisationManager = ObjectMethods.FindComponentOfType<DataSerialisationManager>();

		SetGOsActive(false);
		RegisterToListeners(true);
		timer.SetDuration(delayOnStart);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.timerFinishedEvent.AddListener(OnTimerFinished);

			if(nukeEntity != null)
			{
				nukeEntity.nukeWasDestroyedEvent.AddListener(OnNukeWasDestroyed);
			}

			if(translationBackgroundPanelUI != null)
			{
				translationBackgroundPanelUI.translationWasFinishedEvent.AddListener(OnTranslationWasFinished);
			}

			if(stageStateManager != null)
			{
				stageStateManager.stageStateWasChangedEvent.AddListener(OnStageStateWasChanged);
			}
		}
		else
		{
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);

			if(nukeEntity != null)
			{
				nukeEntity.nukeWasDestroyedEvent.RemoveListener(OnNukeWasDestroyed);
			}

			if(translationBackgroundPanelUI != null)
			{
				translationBackgroundPanelUI.translationWasFinishedEvent.RemoveListener(OnTranslationWasFinished);
			}

			if(stageStateManager != null)
			{
				stageStateManager.stageStateWasChangedEvent.RemoveListener(OnStageStateWasChanged);
			}
		}
	}

	private void OnTimerFinished()
	{
		if(stageStateManager == null)
		{
			return;
		}

		switch (stageStateManager.GetStageState())
		{
			case StageState.Active:
				stageStartedEvent?.Invoke();
				break;

			case StageState.Interrupted:
				SetGameAsOverIfNeeded();
				break;
		}
	}

	private void OnNukeWasDestroyed()
	{
		if(stageStateManager != null)
		{
			stageStateManager.SetStateTo(StageState.Interrupted);
		}
	}

	private void OnTranslationWasFinished()
	{
		SetGOsActive(true);
		stageWasActivatedEvent?.Invoke();
	}

	private void SetGOsActive(bool active)
	{
		gosToActivateWhenStageIsActivated.ForEach(go => go.SetActive(active));
	}

	private void OnStageStateWasChanged(StageState stageState)
	{
		SetGameAsOverIfNeeded(stageState);
		StartTimerWithDurationForInterruptedGameIfPossible(stageState);
		SaveAllDataIfNeeded(stageState);
	}

	private void SetGameAsOverIfNeeded(StageState stageState)
	{
		var stageStatesEndingGame = new List<StageState> {StageState.Interrupted, StageState.Over};
		
		if(!GameDataMethods.GameIsOver(gameData) && stageStatesEndingGame.Contains(stageState))
		{
			gameData.SetGameAsOver();
		}
	}

	private void StartTimerWithDurationForInterruptedGameIfPossible(StageState stageState)
	{
		if(stageState == StageState.Interrupted)
		{
			timer.StartTimerWithSetDuration(delayAfterInterrupting);
		}
	}

	private void SaveAllDataIfNeeded(StageState stageState)
	{
		if(dataSerialisationManager == null)
		{
			return;
		}
		
		var statesSavingData = new List<StageState>()
		{
			StageState.Interrupted,
			StageState.Over
		};

		if(statesSavingData.Contains(stageState))
		{
			dataSerialisationManager.SerialiseAllData();
		}
	}

	private void PlaySoundIfNeeded(bool playSound)
	{
		if(playSound && stageSoundManager != null)
		{
			stageSoundManager.PlaySound(SoundEffectType.GamePause);
		}
	}
}