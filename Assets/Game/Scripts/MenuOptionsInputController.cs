using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Timer), typeof(PlayerInput))]
public class MenuOptionsInputController : MonoBehaviour
{
	public UnityEvent<int> navigateKeyWasPressedEvent;
	public UnityEvent submitKeyWasPressedEvent;
	public UnityEvent cancelKeyWasPressedEvent;
	
	[SerializeField] private Axis navigationAxis;

	private int navigationDirection;
	private Timer timer;
	private PlayerInput playerInput;

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		playerInput = GetComponent<PlayerInput>();

		SetControlScheme();
		RegisterToListeners(true);
	}

	private void SetControlScheme()
	{
		var controlSchemeName = Gamepad.current != null ? InputMethods.GAMEPAD_CONTROL_SCHEME_NAME : InputMethods.KEYBOARD_AND_MOUSE_CONTROL_SCHEME_NAME;

		InputMethods.SetControlSchemeTo(playerInput, controlSchemeName);
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
		navigateKeyWasPressedEvent?.Invoke(navigationDirection);
	}

	private void OnNavigate(InputValue inputValue)
	{
		navigationDirection = GetNavigationValue(inputValue.Get<Vector2>());
	}

	private void OnSubmit(InputValue inputValue)
	{
		submitKeyWasPressedEvent?.Invoke();
	}

	private void OnCancel(InputValue inputValue)
	{
		cancelKeyWasPressedEvent?.Invoke();
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