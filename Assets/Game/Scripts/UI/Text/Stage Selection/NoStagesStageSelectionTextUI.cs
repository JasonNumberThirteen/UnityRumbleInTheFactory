using UnityEngine;
using UnityEngine.InputSystem;

public class NoStagesStageSelectionTextUI : StageSelectionTextUI
{
	[SerializeField] private InputActionReference inputActionReference;

	private PlayerInput playerInput;
	
	private readonly string KEY_LITERAL = "[KEY]";

	protected override void Awake()
	{
		base.Awake();

		playerInput = ObjectMethods.FindComponentOfType<PlayerInput>();
		text.text = text.text.Replace(KEY_LITERAL, GetBindingString());
	}

	private string GetBindingString()
	{
		if(inputActionReference == null || playerInput == null)
		{
			return KEY_LITERAL;
		}
		
		for (int i = 0; i < inputActionReference.action.bindings.Count; i++)
		{
			if(inputActionReference.action.bindings[i].groups.Contains(playerInput.currentControlScheme))
			{
				return inputActionReference.action.GetBindingDisplayString().ToUpper();
			}
		}

		return KEY_LITERAL;
	}
}