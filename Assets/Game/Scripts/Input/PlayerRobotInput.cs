using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RobotEntityShootController))]
public class PlayerRobotInput : MonoBehaviour
{
	public Vector2 MovementVector {get; private set;}
	public Vector2 LastMovementVector {get; private set;}

	private RobotEntityShootController robotEntityShootController;
	private StageSoundManager stageSoundManager;
	private StageStateManager stageStateManager;
	private StageSceneFlowManager stageSceneFlowManager;

	private void Awake()
	{
		robotEntityShootController = GetComponent<RobotEntityShootController>();
		stageSoundManager = FindAnyObjectByType<StageSoundManager>();
		stageStateManager = FindAnyObjectByType<StageStateManager>();
		stageSceneFlowManager = FindAnyObjectByType<StageSceneFlowManager>();
	}

	private void PlayMovementSoundIfPossible()
	{
		if(stageSoundManager == null || InputIsLocked())
		{
			return;
		}

		var soundEffectType = MovementVector == Vector2.zero ? SoundEffectType.PlayerRobotIdle : SoundEffectType.PlayerRobotMovement;
		
		stageSoundManager.PlaySound(soundEffectType);
	}

	private void OnMove(InputValue inputValue)
	{
		if(InputIsLocked())
		{
			return;
		}
		
		LastMovementVector = MovementVector;
		MovementVector = inputValue.Get<Vector2>();

		PlayMovementSoundIfPossible();
	}

	private void OnFire(InputValue inputValue)
	{
		if(robotEntityShootController != null && !InputIsLocked())
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

	private bool InputIsLocked() => stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused);
}