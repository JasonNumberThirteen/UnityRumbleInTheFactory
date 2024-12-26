using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer), typeof(RectTransformTimedMover))]
public class MainMenuPanelUI : MonoBehaviour
{
	public UnityEvent panelReachedTargetPositionEvent;

	public bool ReachedTargetPosition {get; private set;}
	
	[SerializeField] private GameData gameData;
	
	private Timer timer;
	private RectTransformTimedMover rectTransformTimedMover;

	public void SetTargetPosition()
	{
		if(ReachedTargetPosition)
		{
			return;
		}

		ReachedTargetPosition = true;
		
		rectTransformTimedMover.SetPositionY(0);
		panelReachedTargetPositionEvent?.Invoke();
	}

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
		}
		else
		{
			timer.onEnd.RemoveListener(SetTargetPosition);
		}
	}

	private void SetTargetPositionImmediatelyIfNeeded()
	{
		if(gameData != null && gameData.enteredStageSelection)
		{
			SetTargetPosition();
		}
	}
}