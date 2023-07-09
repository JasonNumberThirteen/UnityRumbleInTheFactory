using UnityEngine;
using UnityEngine.InputSystem;

public class StageSelectionInput : MonoBehaviour
{
	private int stageIndex;

	private void OnNavigate(InputValue iv)
	{
		float x = iv.Get<Vector2>().x;
		int stageOffset = Mathf.RoundToInt(x);

		if(stageOffset == -1)
		{
			Debug.Log("Stage index: " + --stageIndex);
		}
		else if(stageOffset == 1)
		{
			Debug.Log("Stage index: " + ++stageIndex);
		}
	}

	private void OnSubmit(InputValue iv) => Debug.Log("Selected stage index: " + stageIndex);
}