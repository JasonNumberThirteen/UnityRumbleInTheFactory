using UnityEngine;
using UnityEngine.Events;

public class StageSelectionManager : MonoBehaviour
{
	public UnityEvent<int> navigationDirectionChangedEvent;
	
	[SerializeField] private GameData gameData;

	private MenuOptionsInput menuOptionsInput;

	private void Awake()
	{
		if(GameDataMethods.AnyStageFound(gameData))
		{
			menuOptionsInput = ObjectMethods.FindComponentOfType<MenuOptionsInput>();
		}

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
		navigationDirectionChangedEvent?.Invoke(direction);
	}
}