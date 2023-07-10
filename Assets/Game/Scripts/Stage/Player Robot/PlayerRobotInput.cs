using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRobotInput : MonoBehaviour
{
	public Vector2 MovementVector {get; private set;}

	private PlayerRobotShoot shoot;

	private void Awake() => shoot = GetComponent<PlayerRobotShoot>();

	private void OnMove(InputValue iv)
	{
		Vector2 movement = iv.Get<Vector2>();

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