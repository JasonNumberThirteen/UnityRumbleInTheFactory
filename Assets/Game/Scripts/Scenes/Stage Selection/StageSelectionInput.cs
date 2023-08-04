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
				stageCounter.SetTo(stagesLoader.DetectedStages());
			}
		}
		else if(stageOffset == 1)
		{
			stageCounter.IncreaseBy(1);

			if(stageCounter.CurrentValue > stagesLoader.DetectedStages())
			{
				stageCounter.SetTo(1);
			}
		}
	}

	private void OnSubmit(InputValue iv) => sceneManager.StartGame(stageCounter.CurrentValue);
	private void OnCancel(InputValue iv) => sceneManager.BackToMainMenu();
}