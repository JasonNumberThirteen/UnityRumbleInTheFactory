using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class StageSelectionInput : MonoBehaviour
{
	public Counter stageCounter;
	public StageSelectionSceneManager sceneManager;
	public GameData gameData;
	public StagesLoader stagesLoader;
	public InputSystemUIInputModule inputModule;

	private int navigationDirection;
	private float navigationTimer;

	private void Update() => NavigateRepeatedly();

	private void NavigateRepeatedly()
	{
		if(gameData.StagesDoNotExist())
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
				ChangeStage(navigationDirection);

				navigationTimer = inputModule.moveRepeatRate;
			}
		}
		else if(navigationTimer != 0)
		{
			navigationTimer = 0;
		}
	}

	private void OnNavigate(InputValue iv)
	{
		if(gameData.StagesDoNotExist())
		{
			return;
		}
		
		Vector2 inputVector = iv.Get<Vector2>();

		navigationDirection = (int)inputVector.x;
	}

	private void ChangeStage(float x)
	{
		int stageOffset = Mathf.RoundToInt(x);
		
		if(stageOffset == -1)
		{
			stageCounter.DecreaseBy(1);

			if(stageCounter.CurrentValue <= 0)
			{
				stageCounter.SetTo(gameData.stages.Length);
			}
		}
		else if(stageOffset == 1)
		{
			stageCounter.IncreaseBy(1);

			if(stageCounter.CurrentValue > gameData.stages.Length)
			{
				stageCounter.SetTo(1);
			}
		}
	}

	private void OnSubmit(InputValue iv)
	{
		if(!gameData.StagesDoNotExist())
		{
			sceneManager.StartGame(stageCounter.CurrentValue);
		}
	}

	private void OnCancel(InputValue iv) => sceneManager.BackToMainMenu();
}