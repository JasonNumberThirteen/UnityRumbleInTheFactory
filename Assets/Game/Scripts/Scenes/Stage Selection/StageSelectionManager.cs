using UnityEngine;
using UnityEngine.InputSystem.UI;

public class StageSelectionManager : MonoBehaviour
{
	[SerializeField] private LoopingCounter loopingCounter;
	[SerializeField] private StageSelectionGameSceneManager sceneManager;
	[SerializeField] private GameData gameData;
	[SerializeField] private StagesLoader stagesLoader;
	[SerializeField] private InputSystemUIInputModule inputModule;

	private int navigationDirection;
	private float navigationTimer;
	private MenuOptionsInput menuOptionsInput;

	private void Awake()
	{
		menuOptionsInput = FindFirstObjectByType<MenuOptionsInput>();

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
				menuOptionsInput.submitKeyPressedEvent.AddListener(OnSubmitKeyPressed);
				menuOptionsInput.cancelKeyPressedEvent.AddListener(OnCancelKeyPressed);
			}
		}
		else
		{
			if(menuOptionsInput != null)
			{
				menuOptionsInput.navigateKeyPressedEvent.RemoveListener(OnNavigateKeyPressed);
				menuOptionsInput.submitKeyPressedEvent.RemoveListener(OnSubmitKeyPressed);
				menuOptionsInput.cancelKeyPressedEvent.RemoveListener(OnCancelKeyPressed);
			}
		}
	}

	private void Update()
	{
		NavigateRepeatedly();
	}

	private void NavigateRepeatedly()
	{
		if(gameData == null || gameData.StagesDoNotExist())
		{
			return;
		}

		if(navigationDirection != 0)
		{
			if(navigationTimer >= 0)
			{
				navigationTimer -= Time.deltaTime;
			}
			else
			{
				ChangeStage();

				if(inputModule != null)
				{
					navigationTimer = inputModule.moveRepeatRate;
				}
			}
		}
		else if(navigationTimer != 0)
		{
			navigationTimer = 0;
		}
	}

	private void ChangeStage()
	{
		if(loopingCounter == null)
		{
			return;
		}
		
		if(navigationDirection == -1)
		{
			loopingCounter.DecreaseBy(1);
		}
		else if(navigationDirection == 1)
		{
			loopingCounter.IncreaseBy(1);
		}
	}

	private void OnNavigateKeyPressed(int direction)
	{
		if(gameData != null && !gameData.StagesDoNotExist())
		{
			navigationDirection = direction;
		}
	}

	private void OnSubmitKeyPressed()
	{
		if(gameData != null && !gameData.StagesDoNotExist() && sceneManager != null && loopingCounter != null)
		{
			sceneManager.StartGame(loopingCounter.CurrentValue);
		}
	}

	private void OnCancelKeyPressed()
	{
		sceneManager.BackToMainMenu();
	}
}