using UnityEngine;

public class MainMenuOptionsController : MonoBehaviour
{
	public Option[] options;

	private int optionIndex;

	public void SelectOption() => options[optionIndex].onSelect.Invoke();
	public void SubmitOption() => options[optionIndex].onSubmit.Invoke();

	public void IncreaseOptionValue()
	{
		++optionIndex;

		ClampOptionValue();
	}

	public void DecreaseOptionValue()
	{
		--optionIndex;

		ClampOptionValue();
	}

	private void ClampOptionValue()
	{
		int lastOptionIndex = options.Length - 1;

		if(optionIndex < 0)
		{
			optionIndex = lastOptionIndex;
		}
		else if(optionIndex > lastOptionIndex)
		{
			optionIndex = 0;
		}
	}
}