using UnityEngine;
using UnityEngine.Events;

public class StageSelectionManager : MonoBehaviour
{
	public UnityEvent<int> navigationDirectionChangedEvent;
	
	[SerializeField] private GameData gameData;

	private MenuOptionsInputController menuOptionsInputController;
	private bool navigationIsActive;

	private void Awake()
	{
		menuOptionsInputController = ObjectMethods.FindComponentOfType<MenuOptionsInputController>();

		RegisterToListeners(true);
	}

	private void Start()
	{
		navigationIsActive = GameDataMethods.AnyStageFound(gameData);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(menuOptionsInputController != null)
			{
				menuOptionsInputController.navigateKeyPressedEvent.AddListener(OnNavigateKeyPressed);
			}
		}
		else
		{
			if(menuOptionsInputController != null)
			{
				menuOptionsInputController.navigateKeyPressedEvent.RemoveListener(OnNavigateKeyPressed);
			}
		}
	}

	private void OnNavigateKeyPressed(int direction)
	{
		if(navigationIsActive)
		{
			navigationDirectionChangedEvent?.Invoke(direction);
		}
	}
}