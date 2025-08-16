using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerRobotEntityInputController : MonoBehaviour
{
	public UnityEvent<Vector2> movementKeyWasPressedEvent;
	public UnityEvent shootKeyWasPressedEvent;

	public void OnMove(InputValue inputValue)
	{
		movementKeyWasPressedEvent?.Invoke(inputValue.Get<Vector2>());
	}

	public void OnFire(InputValue inputValue)
	{
		shootKeyWasPressedEvent?.Invoke();
	}
}