using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class StageSelectionManager : MonoBehaviour
{
	public UnityEvent<int> navigationDirectionChangedEvent;
	
	[SerializeField] private GameData gameData;

	private MenuOptionsInput menuOptionsInput;
	private Timer timer;
	private int navigationDirection;
	private float navigationTimer;

	private void Awake()
	{
		menuOptionsInput = FindAnyObjectByType<MenuOptionsInput>();
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
			if(menuOptionsInput != null)
			{
				menuOptionsInput.navigateKeyPressedEvent.AddListener(OnNavigateKeyPressed);
			}
		}
		else
		{
			if(menuOptionsInput != null)
			{
				menuOptionsInput.navigateKeyPressedEvent.RemoveListener(OnNavigateKeyPressed);
			}
		}
	}

	private void Update()
	{
		if(navigationDirection != 0)
		{
			if(navigationTimer >= 0)
			{
				navigationTimer -= Time.deltaTime;
			}
			else
			{
				navigationTimer = timer.duration;

				navigationDirectionChangedEvent?.Invoke(navigationDirection);
			}
		}
		else if(navigationTimer != 0)
		{
			navigationTimer = 0;
		}
	}

	private void OnNavigateKeyPressed(int direction)
	{
		if(!GameDataMethods.NoStagesFound(gameData))
		{
			navigationDirection = direction;
		}
	}
}