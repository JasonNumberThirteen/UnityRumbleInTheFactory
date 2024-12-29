using UnityEngine;

[RequireComponent(typeof(Timer))]
public class StageSelectionManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private MenuOptionsInput menuOptionsInput;
	private StageCounterStageSelectionTextUI stageSelectionStageCounterTextUI;
	private Timer timer;
	private int navigationDirection;
	private float navigationTimer;

	private void Awake()
	{
		menuOptionsInput = FindFirstObjectByType<MenuOptionsInput>();
		stageSelectionStageCounterTextUI = FindFirstObjectByType<StageCounterStageSelectionTextUI>();
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
		NavigateRepeatedly();
	}

	private void NavigateRepeatedly()
	{
		if(gameData == null || gameData.NoStagesFound())
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
				if(stageSelectionStageCounterTextUI != null)
				{
					stageSelectionStageCounterTextUI.ModifyCounterBy(navigationDirection);
				}

				navigationTimer = timer.duration;
			}
		}
		else if(navigationTimer != 0)
		{
			navigationTimer = 0;
		}
	}

	private void OnNavigateKeyPressed(int direction)
	{
		if(gameData != null && !gameData.NoStagesFound())
		{
			navigationDirection = direction;
		}
	}
}