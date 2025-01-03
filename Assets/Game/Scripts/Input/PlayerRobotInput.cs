using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRobotInput : MonoBehaviour
{
	public Vector2 MovementVector {get; private set;}
	public Vector2 LastMovementVector {get; private set;}

	private RobotShoot shoot;
	private StageSoundManager stageSoundManager;

	private void Awake()
	{
		shoot = GetComponent<RobotShoot>();
		stageSoundManager = FindAnyObjectByType<StageSoundManager>();
	}

	private void Start()
	{
		if(StageManager.instance.stateManager.StateIsSetTo(StageState.Over))
		{
			Destroy(this);
		}
	}

	private void OnMove(InputValue iv)
	{
		if(StageManager.instance.stateManager.StateIsSetTo(StageState.Paused))
		{
			return;
		}
		
		Vector2 movement = iv.Get<Vector2>();

		LastMovementVector = MovementVector;
		MovementVector = movement;

		SetMovementSound();
	}

	private void SetMovementSound()
	{
		if(stageSoundManager == null)
		{
			return;
		}
		
		if(MovementVector == Vector2.zero)
		{
			stageSoundManager.PlaySound(SoundEffectType.PlayerRobotIdle);
		}
		else
		{
			stageSoundManager.PlaySound(SoundEffectType.PlayerRobotMovement);
		}
	}

	private void OnFire(InputValue iv)
	{
		if(!StageManager.instance.stateManager.StateIsSetTo(StageState.Paused) && shoot != null)
		{
			shoot.FireBullet();
		}
	}

	private void OnPause(InputValue iv) => StageManager.instance.PauseGame();
}