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
		panelStartedTranslationEvent?.Invoke();
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();

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
		}
		else
		{
			timer.onEnd.RemoveListener(OnTimerEnd);
		}
	}

	private void OnTimerEnd()
	{
		panelFinishedTranslationEvent?.Invoke();
	}
}