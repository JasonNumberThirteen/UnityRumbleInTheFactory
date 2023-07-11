using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRobotInput : MonoBehaviour
{
	public Vector2 MovementVector {get; private set;}
	public Vector2 LastMovementVector {get; private set;}

	private PlayerRobotShoot shoot;

	private void Awake() => shoot = GetComponent<PlayerRobotShoot>();

	private void OnMove(InputValue iv)
	{
		Vector2 movement = iv.Get<Vector2>();

		LastMovementVector = MovementVector;
		MovementVector = movement;
	}

	private void OnFire(InputValue iv)
	{
		if(shoot != null)
		{
			shoot.FireBullet();
		}
	}
}