using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuInput : MonoBehaviour
{
	public MainMenuOptionsController optionsController;

	private void OnNavigate(InputValue iv)
	{
		float y = iv.Get<Vector2>().y;
		int optionOffset = Mathf.RoundToInt(-y);

		if(optionOffset == -1)
		{
			optionsController.DecreaseOptionValue();
			optionsController.SelectOption();
		}
		else if(optionOffset == 1)
		{
			optionsController.IncreaseOptionValue();
			optionsController.SelectOption();
		}
	}

	private void OnSubmit(InputValue iv) => optionsController.SubmitOption();
}