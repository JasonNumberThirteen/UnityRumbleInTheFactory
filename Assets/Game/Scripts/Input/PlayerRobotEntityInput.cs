using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RobotEntityShootController))]
public class PlayerRobotEntityInput : MonoBehaviour
{
	public UnityEvent<PlayerRobotEntityInput, bool> movementValueChangedEvent;
	public UnityEvent<PlayerRobotEntityInput> playerDiedEvent;
	
	public Vector2 MovementVector {get; private set;}
	public Vector2 LastMovementVector {get; private set;}

	private RobotEntityShootController robotEntityShootController;
	private StageStateManager stageStateManager;
	private StageSceneFlowManager stageSceneFlowManager;

	private void Awake()
	{
		robotEntityShootController = GetComponent<RobotEntityShootController>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();
	}

	private void OnMove(InputValue inputValue)
	{
		var gameIsPaused = GameIsPaused();
		
		if(gameIsPaused && !MovementVector.IsZero())
		{
			UpdateMovementVector(Vector2.zero);
		}
		else if(!gameIsPaused && enabled)
		{
			UpdateMovementVector(inputValue.Get<Vector2>());
		}
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

	private void OnDisable()
	{
		UpdateMovementVector(Vector2.zero);
	}

	private void OnDestroy()
	{
		UpdateMovementVector(Vector2.zero);
		playerDiedEvent?.Invoke(this);
	}

	private void UpdateMovementVector(Vector2 movementVector)
	{
		LastMovementVector = MovementVector;
		MovementVector = movementVector;

		movementValueChangedEvent?.Invoke(this, !MovementVector.IsZero());
	}

	private bool GameIsPaused() => stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused);
}