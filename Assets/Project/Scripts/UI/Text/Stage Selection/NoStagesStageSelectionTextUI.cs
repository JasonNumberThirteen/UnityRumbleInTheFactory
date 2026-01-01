using UnityEngine;
using UnityEngine.InputSystem;

public class NoStagesStageSelectionTextUI : StageSelectionTextUI
{
	[SerializeField] private InputActionReference inputActionReference;

	private PlayerInput playerInput;
	
	private static readonly string KEY_LITERAL = "[KEY]";

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

		var action = inputActionReference.action;
		var bindings = action.bindings;
		
		for (int i = 0; i < bindings.Count; i++)
		{
			if(bindings[i].groups.Contains(playerInput.currentControlScheme))
			{
				return action.GetBindingDisplayString().ToUpper();
			}
		}

		return KEY_LITERAL;
	}
}