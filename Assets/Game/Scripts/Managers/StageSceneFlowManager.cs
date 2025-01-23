using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class StageSceneFlowManager : MonoBehaviour
{
	public UnityEvent stageStartedEvent;
	public UnityEvent stageActivatedEvent;
	
	[SerializeField] private GameData gameData;
	[SerializeField] private GameObject[] gosToActivateWhenStageIsActivated;
	[SerializeField] private float delayOnStart = 1.5f;
	[SerializeField] private float delayAfterInterrupting = 1f;
	
	private Timer timer;
	private StageStateManager stageStateManager;
	private StageSoundManager stageSoundManager;
	private DataSerialisationManager dataSerialisationManager;
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;
	private NukeEntity nukeEntity;

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
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();
		dataSerialisationManager = ObjectMethods.FindComponentOfType<DataSerialisationManager>();
		translationBackgroundPanelUI = ObjectMethods.FindComponentOfType<TranslationBackgroundPanelUI>();
		nukeEntity = ObjectMethods.FindComponentOfType<NukeEntity>();

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

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}

			if(translationBackgroundPanelUI != null)
			{
				translationBackgroundPanelUI.panelFinishedTranslationEvent.AddListener(OnPanelFinishedTranslation);
			}

			if(nukeEntity != null)
			{
				nukeEntity.nukeDestroyedEvent.AddListener(OnNukeDestroyed);
			}
		}
		else
		{
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}

			if(translationBackgroundPanelUI != null)
			{
				translationBackgroundPanelUI.panelFinishedTranslationEvent.RemoveListener(OnPanelFinishedTranslation);
			}

			if(nukeEntity != null)
			{
				nukeEntity.nukeDestroyedEvent.RemoveListener(OnNukeDestroyed);
			}
		}
	}

	private void OnTimerFinished()
	{
		if(stageStateManager == null)
		{
			return;
		}

		if(stageStateManager.StateIsSetTo(StageState.Active))
		{
			stageStartedEvent?.Invoke();
		}
		else if(stageStateManager.StateIsSetTo(StageState.Interrupted))
		{
			SetGameAsOverIfNeeded();
		}
	}

	private void OnStageStateChanged(StageState stageState)
	{
		if(gameData != null && !gameData.GameIsOver && (stageState == StageState.Interrupted || stageState == StageState.Over))
		{
			gameData.SetGameAsOver();
		}
		
		if(stageState == StageState.Interrupted)
		{
			timer.SetDuration(delayAfterInterrupting);
			timer.StartTimer();
		}

		SaveAllDataIfNeeded(stageState);
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
			dataSerialisationManager.SaveAllData();
		}
	}

	private void OnPanelFinishedTranslation()
	{
		SetGOsActive(true);
		stageActivatedEvent?.Invoke();
	}

	private void OnNukeDestroyed()
	{
		if(stageStateManager != null)
		{
			stageStateManager.SetStateTo(StageState.Interrupted);
		}
	}

	private void SetGOsActive(bool active)
	{
		foreach (var go in gosToActivateWhenStageIsActivated)
		{
			go.SetActive(active);
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