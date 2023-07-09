using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRobotInput : MonoBehaviour
{
	public Vector2 MovementVector {get; private set;}

	private void OnMove(InputValue iv)
	{
		Vector2 movement = iv.Get<Vector2>();

		MovementVector = movement;
	}
}