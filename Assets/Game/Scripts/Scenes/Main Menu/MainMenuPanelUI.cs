using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer), typeof(RectTransformTimedMover))]
public class MainMenuPanelUI : MonoBehaviour
{
	public UnityEvent panelReachedTargetPositionEvent;
	
	[SerializeField] private GameData gameData;
	
	private Timer timer;
	private RectTransformTimedMover rectTransformTimedMover;
	private bool reachedTargetPosition;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		rectTransformTimedMover = GetComponent<RectTransformTimedMover>();

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
			timer.onEnd.AddListener(SetTargetPosition);
			timer.onInterrupt.AddListener(SetTargetPosition);
		}
		else
		{
			timer.onEnd.RemoveListener(SetTargetPosition);
			timer.onInterrupt.RemoveListener(SetTargetPosition);
		}
	}

	private void SetTargetPositionImmediatelyIfNeeded()
	{
		if(gameData != null && gameData.enteredStageSelection)
		{
			SetTargetPosition();
		}
	}

	private void SetTargetPosition()
	{
		if(reachedTargetPosition)
		{
			return;
		}

		reachedTargetPosition = true;
		
		rectTransformTimedMover.SetPositionY(0);
		panelReachedTargetPositionEvent?.Invoke();
	}
}