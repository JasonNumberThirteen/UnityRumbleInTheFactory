using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputGamepadControlSchemeSetter : MonoBehaviour
{
	private static readonly string GAMEPAD_CONTROL_SCHEME_NAME = "Gamepad";
	
	private void Awake()
	{
		var playerInput = ObjectMethods.FindComponentOfType<PlayerInput>();
		
		if(playerInput != null && InputMethods.GamepadIsAvailable())
		{
			playerInput.SwitchCurrentControlScheme(GAMEPAD_CONTROL_SCHEME_NAME, Gamepad.current);
		}
	}
}