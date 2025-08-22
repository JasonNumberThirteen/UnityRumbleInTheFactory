using UnityEngine.InputSystem;

public static class InputMethods
{
	public static bool GamepadIsAvailable() => Gamepad.current != null;
}