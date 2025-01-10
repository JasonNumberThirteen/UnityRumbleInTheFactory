using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RobotEntityShootController))]
public class PlayerRobotEntityInput : MonoBehaviour
{
	public Vector2 MovementVector {get; private set;}
	public Vector2 LastMovementVector {get; private set;}

	private RobotEntityShootController robotEntityShootController;
	private StageSoundManager stageSoundManager;
	private StageSceneFlowManager stageSceneFlowManager;

	private void Awake()
	{
		robotEntityShootController = GetComponent<RobotEntityShootController>();
		stageSoundManager = FindAnyObjectByType<StageSoundManager>();
		stageSceneFlowManager = FindAnyObjectByType<StageSceneFlowManager>();
	}

	private void OnMove(InputValue inputValue)
	{
		if(!enabled)
		{
			return;
		}
		
		LastMovementVector = MovementVector;
		MovementVector = inputValue.Get<Vector2>();

		PlayMovementSoundIfPossible();
	}

	private void PlayMovementSoundIfPossible()
	{
		if(!enabled || stageSoundManager == null)
		{
			return;
		}

		var soundEffectType = MovementVector == Vector2.zero ? SoundEffectType.PlayerRobotIdle : SoundEffectType.PlayerRobotMovement;
		
		stageSoundManager.PlaySound(soundEffectType);
	}

	private void OnFire(InputValue inputValue)
	{
		if(enabled && robotEntityShootController != null)
		{
			robotEntityShootController.FireBullet();
		}
	}

	private void OnPause(InputValue inputValue)
	{
		if(stageSceneFlowManager != null)
		{
			stageSceneFlowManager.PauseGameIfPossible();
		}
	}
}