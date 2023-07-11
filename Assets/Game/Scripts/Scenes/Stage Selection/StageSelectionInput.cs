using UnityEngine;
using UnityEngine.InputSystem;

public class StageSelectionInput : MonoBehaviour
{
	public Counter stageCounter;
	public GameSceneManager sceneManager;
	public string stageScene;

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
		}
		else if(stageOffset == 1)
		{
			stageCounter.IncreaseBy(1);
		}
	}

	private void OnSubmit(InputValue iv) => sceneManager.LoadScene(stageScene);
}