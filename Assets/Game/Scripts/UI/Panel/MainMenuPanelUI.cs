using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer), typeof(TimedRectTransformPositionController))]
public class MainMenuPanelUI : MonoBehaviour
{
	public UnityEvent panelReachedTargetPositionEvent;

	public bool ReachedTargetPosition {get; private set;}
	
	[SerializeField] private GameData gameData;
	
	private Timer timer;
	private TimedRectTransformPositionController timedRectTransformPositionController;

	public void SetTargetPosition()
	{
		if(ReachedTargetPosition)
		{
			return;
		}

		ReachedTargetPosition = true;
		
		timedRectTransformPositionController.SetPositionY(0);
		panelReachedTargetPositionEvent?.Invoke();
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		timedRectTransformPositionController = GetComponent<TimedRectTransformPositionController>();

		RegisterToListeners(true);
	}

	private void Start()
	{
		SetTargetPositionImmediatelyIfNeeded();
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.timerFinishedEvent.AddListener(SetTargetPosition);
		}
		else
		{
			timer.timerFinishedEvent.RemoveListener(SetTargetPosition);
		}
	}

	private void SetTargetPositionImmediatelyIfNeeded()
	{
		if(gameData != null && gameData.EnteredStageSelection)
		{
			SetTargetPosition();
		}
	}
}