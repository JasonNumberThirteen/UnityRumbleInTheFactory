using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Timer))]
public class MenuOptionsInput : MonoBehaviour
{
	public UnityEvent<int> navigateKeyPressedEvent;
	public UnityEvent submitKeyPressedEvent;
	public UnityEvent cancelKeyPressedEvent;
	
	[SerializeField] private Axis navigationAxis;

	private Timer timer;
	private int navigationDirection;

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
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
			timer.timerStartedEvent.AddListener(OnTimerStarted);
			timer.timerFinishedEvent.AddListener(timer.StartTimer);
		}
		else
		{
			timer.timerStartedEvent.RemoveListener(OnTimerStarted);
			timer.timerFinishedEvent.RemoveListener(timer.StartTimer);
		}
	}

	private void OnTimerStarted()
	{
		navigateKeyPressedEvent?.Invoke(navigationDirection);
	}

	private void OnNavigate(InputValue inputValue)
	{
		navigationDirection = GetNavigationValue(inputValue.Get<Vector2>());
	}

	private void OnSubmit(InputValue inputValue)
	{
		submitKeyPressedEvent?.Invoke();
	}

	private void OnCancel(InputValue inputValue)
	{
		cancelKeyPressedEvent?.Invoke();
	}

	private int GetNavigationValue(Vector2 inputVector)
	{
		return navigationAxis switch
		{
			Axis.Horizontal => Mathf.RoundToInt(inputVector.x),
			Axis.Vertical => Mathf.RoundToInt(-inputVector.y),
			_ => 0
		};
	}

	private void Update()
	{
		if(navigationDirection != 0 && !timer.TimerWasStarted)
		{
			timer.StartTimer();
		}
		else if(navigationDirection == 0)
		{
			timer.InterruptTimerIfPossible();
		}
	}
}