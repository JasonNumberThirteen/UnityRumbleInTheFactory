using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class TranslationBackgroundPanelUI : MonoBehaviour
{
	public UnityEvent panelStartedTranslationEvent;
	public UnityEvent panelFinishedTranslationEvent;
	
	private Timer timer;

	public void StartTranslation()
	{
		timer.StartTimer();
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();

		SetTimerToAllChildrenComponents();
		RegisterToListeners(true);
	}

	private void SetTimerToAllChildrenComponents()
	{
		var childrenRectTransformOffsetControllers = GetComponentsInChildren<TranslationBackgroundPartPanelUIRectTransformOffsetController>();

		foreach (var rectTransformOffsetController in childrenRectTransformOffsetControllers)
		{
			rectTransformOffsetController.SetTimer(timer);
		}
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
			timer.onEnd.AddListener(OnTimerEnd);
		}
		else
		{
			timer.timerStartedEvent.RemoveListener(OnTimerStarted);
			timer.onEnd.RemoveListener(OnTimerEnd);
		}
	}

	private void OnTimerStarted()
	{
		panelStartedTranslationEvent?.Invoke();
	}

	private void OnTimerEnd()
	{
		panelFinishedTranslationEvent?.Invoke();
	}
}