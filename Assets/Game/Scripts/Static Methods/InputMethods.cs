using System.Collections.Generic;
using UnityEngine.InputSystem;

public static class InputMethods
{
	public static readonly string KEYBOARD_AND_MOUSE_CONTROL_SCHEME_NAME = "Keyboard&Mouse";
	public static readonly string GAMEPAD_CONTROL_SCHEME_NAME = "Gamepad";

	private static readonly Dictionary<string, InputDevice[]> inputDevicesByControlSchemeName = new()
	{
		{KEYBOARD_AND_MOUSE_CONTROL_SCHEME_NAME, new InputDevice[]{Keyboard.current, Mouse.current}},
		{GAMEPAD_CONTROL_SCHEME_NAME, new InputDevice[]{Gamepad.current}}
	};

	public static bool GamepadIsAvailable() => Gamepad.current != null;

	public static void SetControlSchemeToPlayerInputIfPossible(PlayerInput playerInput, string controlSchemeName)
	{
		if(playerInput != null && playerInput.user.valid)
		{
			playerInput.SwitchCurrentControlScheme(controlSchemeName, GetInputDevicesByControlSchemeName(controlSchemeName));
		}
	}

	private static InputDevice[] GetInputDevicesByControlSchemeName(string controlSchemeName) => inputDevicesByControlSchemeName.TryGetValue(controlSchemeName, out var inputDevices) ? inputDevices : new InputDevice[0];
}