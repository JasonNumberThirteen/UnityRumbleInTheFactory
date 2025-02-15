using System.Collections.Generic;
using UnityEngine;
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

	public static Vector2 GetAdjustedMovementVector(Vector2 vector)
	{
		var oneDirectionalVector = Mathf.Abs(vector.x) > Mathf.Abs(vector.y) ? Vector2.right*vector.x : Vector2.up*vector.y;

		return oneDirectionalVector.GetCeiledVector();
	}

	public static void SetControlSchemeTo(PlayerInput playerInput, string controlSchemeName)
	{
		if(playerInput != null)
		{
			playerInput.SwitchCurrentControlScheme(controlSchemeName, GetInputDevicesByControlSchemeName(controlSchemeName));
		}
	}

	private static InputDevice[] GetInputDevicesByControlSchemeName(string controlSchemeName) => inputDevicesByControlSchemeName.TryGetValue(controlSchemeName, out var inputDevices) ? inputDevices : new InputDevice[0];
}