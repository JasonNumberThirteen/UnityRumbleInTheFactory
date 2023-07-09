using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuInput : MonoBehaviour
{
	private int optionIndex;

	private void OnNavigate(InputValue iv)
	{
		float y = iv.Get<Vector2>().y;
		int optionOffset = Mathf.RoundToInt(-y);

		optionIndex += optionOffset;

		if(optionIndex < 0)
		{
			optionIndex = 2;
		}
		else if(optionIndex > 2)
		{
			optionIndex = 0;
		}

		Debug.Log("Option index: " + optionIndex);
	}

	private void OnSubmit(InputValue iv) => Debug.Log("Selected option index: " + optionIndex);
}