using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuInput : MonoBehaviour
{
	public MainMenuOptionsController optionsController;
	public Timer mainMenuPanelTimer;

	private void OnNavigate(InputValue iv)
	{
		Vector2 inputVector = iv.Get<Vector2>();

		if(mainMenuPanelTimer.Finished)
		{
			ChangeOption(inputVector.y);
		}
		else
		{
			mainMenuPanelTimer.InterruptTimer();
		}
	}

	private void ChangeOption(float y)
	{
		int optionOffset = Mathf.RoundToInt(-y);
		
		if(optionOffset == -1)
		{
			SelectPreviousOption();
		}
		else if(optionOffset == 1)
		{
			SelectNextOption();
		}
	}

	private void SelectPreviousOption()
	{
		optionsController.DecreaseOptionValue();
		optionsController.SelectOption();
	}

	private void SelectNextOption()
	{
		optionsController.IncreaseOptionValue();
		optionsController.SelectOption();
	}

	private void OnSubmit(InputValue iv) => optionsController.SubmitOption();
}