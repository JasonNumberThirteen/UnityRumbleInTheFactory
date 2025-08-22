using UnityEngine;
using UnityEngine.Events;

public class PlayerRobotEntityInputController : MonoBehaviour
{
	public UnityEvent<Vector2> movementKeyWasPressedEvent;
	public UnityEvent shootKeyWasPressedEvent;

	public void OnMove(Vector2 movementVector)
	{
		movementKeyWasPressedEvent?.Invoke(movementVector);
	}

	public void OnFire()
	{
		shootKeyWasPressedEvent?.Invoke();
	}
}