using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RobotEntityShootController))]
public class PlayerRobotInput : MonoBehaviour
{
	public Vector2 MovementVector {get; private set;}
	public Vector2 LastMovementVector {get; private set;}

	private RobotEntityShootController robotEntityShootController;
	private StageSoundManager stageSoundManager;

	private void Awake()
	{
		robotEntityShootController = GetComponent<RobotEntityShootController>();
		stageSoundManager = FindAnyObjectByType<StageSoundManager>();
	}

	private void PlayMovementSoundIfPossible()
	{
		if(stageSoundManager == null)
		{
			return;
		}

		var soundEffectType = MovementVector == Vector2.zero ? SoundEffectType.PlayerRobotIdle : SoundEffectType.PlayerRobotMovement;
		
		stageSoundManager.PlaySound(soundEffectType);
	}

	private void OnMove(InputValue inputValue)
	{
		LastMovementVector = MovementVector;
		MovementVector = inputValue.Get<Vector2>();

		PlayMovementSoundIfPossible();
	}

	private void OnFire(InputValue inputValue)
	{
		if(robotEntityShootController != null)
		{
			robotEntityShootController.FireBullet();
		}
	}

	private void OnPause(InputValue inputValue)
	{
		StageManager.instance.PauseGame();
	}
}