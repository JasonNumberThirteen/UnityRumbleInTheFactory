using UnityEngine;
using UnityEngine.InputSystem;

public class StageSelectionInput : MonoBehaviour
{
	public Counter stageCounter;
	public StageSelectionSceneManager sceneManager;
	public GameData gameData;
	public StagesLoader stagesLoader;

	private void OnNavigate(InputValue iv)
	{
		Vector2 inputVector = iv.Get<Vector2>();
		
		ChangeStage(inputVector.x);
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