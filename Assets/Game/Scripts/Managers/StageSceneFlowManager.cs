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
	}

	public void SetGameAsOver()
	{
		if(stageStateManager != null)
		{
			stageStateManager.SetStateTo(StageState.Over);
		}
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		stageStateManager = FindAnyObjectByType<StageStateManager>();
		translationBackgroundPanelUI = FindAnyObjectByType<TranslationBackgroundPanelUI>(FindObjectsInactive.Include);
		nukeEntity = FindAnyObjectByType<NukeEntity>(FindObjectsInactive.Include);
		timer.duration = delayOnStart;

		SetGOsActive(false);
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
			timer.onEnd.AddListener(OnTimerEnd);

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
			timer.onEnd.RemoveListener(OnTimerEnd);

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

	private void OnTimerEnd()
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
			SetGameAsOver();
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
			timer.duration = delayAfterInterrupting;
			
			timer.ResetTimer();
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
}