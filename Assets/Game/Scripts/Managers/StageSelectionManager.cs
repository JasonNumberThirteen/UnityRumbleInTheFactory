using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class StageSelectionManager : MonoBehaviour
{
	public UnityEvent<int> navigationDirectionChangedEvent;
	
	[SerializeField] private GameData gameData;

	private Timer timer;
	private int navigationDirection;
	private MenuOptionsInput menuOptionsInput;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		menuOptionsInput = ObjectMethods.FindComponentOfType<MenuOptionsInput>();

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
			
			if(menuOptionsInput != null)
			{
				menuOptionsInput.navigateKeyPressedEvent.AddListener(OnNavigateKeyPressed);
			}
		}
		else
		{
			timer.timerStartedEvent.RemoveListener(OnTimerStarted);
			timer.timerFinishedEvent.RemoveListener(timer.StartTimer);
			
			if(menuOptionsInput != null)
			{
				menuOptionsInput.navigateKeyPressedEvent.RemoveListener(OnNavigateKeyPressed);
			}
		}
	}

	private void OnTimerStarted()
	{
		navigationDirectionChangedEvent?.Invoke(navigationDirection);
	}

	private void OnNavigateKeyPressed(int direction)
	{
		if(GameDataMethods.AnyStageFound(gameData))
		{
			navigationDirection = direction;
		}
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