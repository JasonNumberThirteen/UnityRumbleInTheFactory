using UnityEngine.InputSystem;

public class NoStagesStageSelectionTextUI : StageSelectionTextUI
{
	private PlayerInput playerInput;
	private MenuOptionsInputController menuOptionsInputController;

	private readonly CachedString cachedInputBindingString = new();
	
	private static readonly string KEY_LITERAL = "[KEY]";
	private static readonly string CANCEL_ACTION_NAME = "Cancel";

	protected override void Awake()
	{
		base.Awake();

		playerInput = ObjectMethods.FindComponentOfType<PlayerInput>();
		menuOptionsInputController = ObjectMethods.FindComponentOfType<MenuOptionsInputController>();

		SetCachedInputBindingString(KEY_LITERAL);
		UpdateTextIfNeeded();
		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(menuOptionsInputController != null)
			{
				menuOptionsInputController.controlsWereChangedEvent.AddListener(UpdateTextIfNeeded);
			}
		}
		else
		{
			if(menuOptionsInputController != null)
			{
				menuOptionsInputController.controlsWereChangedEvent.RemoveListener(UpdateTextIfNeeded);
			}
		}
	}

	private void UpdateTextIfNeeded()
	{
		var inputBindingString = GetInputBindingString();

		if(inputBindingString.Equals(GetCachedInputBindingString()))
		{
			return;
		}
		
		ReplaceCachedInputBindingStringInTextWith(inputBindingString);
		SetCachedInputBindingString(inputBindingString);
	}

	private void ReplaceCachedInputBindingStringInTextWith(string bindingString)
	{
		text.text = text.text.Replace(GetCachedInputBindingString(), bindingString);
	}

	private void SetCachedInputBindingString(string newString)
	{
		cachedInputBindingString.SetString(newString);
	}

	private string GetInputBindingString()
	{
		if(playerInput == null)
		{
			return GetCachedInputBindingString();
		}

		var cancelAction = playerInput.actions[CANCEL_ACTION_NAME];
		var currentControlSchemeInputBinding = InputBinding.MaskByGroup(playerInput.currentControlScheme);

		return cancelAction.GetBindingDisplayString(currentControlSchemeInputBinding);
	}

	private string GetCachedInputBindingString() => cachedInputBindingString.GetCachedString();
}