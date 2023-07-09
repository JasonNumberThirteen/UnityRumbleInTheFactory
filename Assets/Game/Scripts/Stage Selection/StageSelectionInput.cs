using UnityEngine;
using UnityEngine.InputSystem;

public class StageSelectionInput : MonoBehaviour
{
	public Counter stageCounter;

	private void OnNavigate(InputValue iv)
	{
		float x = iv.Get<Vector2>().x;
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

	private void OnSubmit(InputValue iv) => Debug.Log("Selected stage index: " + stageCounter.CurrentValue);
}