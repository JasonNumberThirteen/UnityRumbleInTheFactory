using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRobotInput : MonoBehaviour
{
	public Vector2 MovementVector {get; private set;}
	public Vector2 LastMovementVector {get; private set;}

	private RobotShoot shoot;

	private void Awake() => shoot = GetComponent<RobotShoot>();

	private void Start()
	{
		if(StageManager.instance.State == StageManager.GameStates.OVER)
		{
			Destroy(this);
		}
	}

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

	private void OnPause(InputValue iv) => StageManager.instance.PauseGame();
}