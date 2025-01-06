using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class StageSceneFlowManager : MonoBehaviour
{
	public UnityEvent stageStartedEvent;
	public UnityEvent stageActivatedEvent;
	
	[SerializeField] private GameObject[] gosToActivateWhenStageIsActivated;
	[SerializeField] private float delayOnStart = 1.5f;
	[SerializeField] private float delayAfterInterrupting = 1f;
	
	private Timer timer;
	private StageStateManager stageStateManager;
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		stageStateManager = FindAnyObjectByType<StageStateManager>();
		translationBackgroundPanelUI = FindAnyObjectByType<TranslationBackgroundPanelUI>(FindObjectsInactive.Include);
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
			StageManager.instance.SetGameAsOver();
		}
	}

	private void OnStageStateChanged(StageState stageState)
	{
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

	private void SetGOsActive(bool active)
	{
		foreach (var go in gosToActivateWhenStageIsActivated)
		{
			go.SetActive(active);
		}
	}
}