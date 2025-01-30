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

	private Vector2 currentMovementVector;
	private RobotEntityShootController robotEntityShootController;
	private StageStateManager stageStateManager;
	private StageSceneFlowManager stageSceneFlowManager;

	private void Awake()
	{
		robotEntityShootController = GetComponent<RobotEntityShootController>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
		UpdateMovementVector(Vector2.zero);
		playerDiedEvent?.Invoke(this);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}
		}
		else
		{
			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}
		}
	}

	private void OnStageStateChanged(StageState stageState)
	{
		UpdateMovementVector(stageState == StageState.Paused ? Vector2.zero : currentMovementVector);
	}

	private void OnMove(InputValue inputValue)
	{
		currentMovementVector = inputValue.Get<Vector2>();

		if(enabled && !GameIsPaused())
		{
			UpdateMovementVector(currentMovementVector);
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

	private void OnEnable()
	{
		UpdateMovementVector(currentMovementVector);
	}

	private void OnDisable()
	{
		UpdateMovementVector(Vector2.zero);
	}

	private void UpdateMovementVector(Vector2 movementVector)
	{
		LastMovementVector = MovementVector;
		MovementVector = movementVector;

		movementValueChangedEvent?.Invoke(this, !MovementVector.IsZero());
	}

	private bool GameIsPaused() => stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused);
}