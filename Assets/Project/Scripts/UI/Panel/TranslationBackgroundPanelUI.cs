using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class TranslationBackgroundPanelUI : MonoBehaviour
{
	public UnityEvent translationWasStartedEvent;
	public UnityEvent translationWasFinishedEvent;
	
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

		childrenRectTransformOffsetControllers.ForEach(rectTransformOffsetController => rectTransformOffsetController.Setup(timer));
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
		}
		else
		{
			timer.timerStartedEvent.RemoveListener(OnTimerStarted);
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);
		}
	}

	private void OnTimerStarted()
	{
		translationWasStartedEvent?.Invoke();
	}

	private void OnTimerFinished()
	{
		translationWasFinishedEvent?.Invoke();
	}
}