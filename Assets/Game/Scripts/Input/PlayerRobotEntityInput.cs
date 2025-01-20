using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RobotEntityShootController))]
public class PlayerRobotEntityInput : MonoBehaviour
{
	public Vector2 MovementVector {get; private set;}
	public Vector2 LastMovementVector {get; private set;}

	private RobotEntityShootController robotEntityShootController;
	private StageStateManager stageStateManager;
	private StageSoundManager stageSoundManager;
	private StageSceneFlowManager stageSceneFlowManager;

	private void Awake()
	{
		robotEntityShootController = GetComponent<RobotEntityShootController>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();
	}

	private void OnMove(InputValue inputValue)
	{
		if(!enabled || GameIsPaused())
		{
			return;
		}
		
		LastMovementVector = MovementVector;
		MovementVector = inputValue.Get<Vector2>();

		PlayMovementSoundIfPossible();
	}

	private void PlayMovementSoundIfPossible()
	{
		if(!enabled || stageSoundManager == null || GameIsPaused())
		{
			return;
		}

		var soundEffectType = MovementVector.IsZero() ? SoundEffectType.PlayerRobotIdle : SoundEffectType.PlayerRobotMovement;
		
		stageSoundManager.PlaySound(soundEffectType);
	}

	private void OnFire(InputValue inputValue)
	{
		if(enabled && robotEntityShootController != null && !GameIsPaused())
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

	private bool GameIsPaused() => stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused);
}