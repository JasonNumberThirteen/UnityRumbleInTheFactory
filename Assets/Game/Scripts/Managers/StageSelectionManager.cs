using UnityEngine;
using UnityEngine.Events;

public class StageSelectionManager : MonoBehaviour
{
	public UnityEvent<int> navigationDirectionChangedEvent;
	
	[SerializeField] private GameData gameData;

	private MenuOptionsInput menuOptionsInput;
	private bool navigationIsActive;

	private void Awake()
	{
		menuOptionsInput = ObjectMethods.FindComponentOfType<MenuOptionsInput>();
		navigationIsActive = GameDataMethods.AnyStageFound(gameData);

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

	private void OnNavigateKeyPressed(int direction)
	{
		if(navigationIsActive)
		{
			navigationDirectionChangedEvent?.Invoke(direction);
		}
	}
}