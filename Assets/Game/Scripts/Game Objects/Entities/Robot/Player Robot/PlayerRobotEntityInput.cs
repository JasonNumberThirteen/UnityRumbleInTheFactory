using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(RobotEntityShootController), typeof(PlayerRobotEntityMovementController))]
public class PlayerRobotEntityInput : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField, Min(1)] private int ordinalNumber;
	
	public UnityEvent<PlayerRobotEntityInput, bool> movementValueChangedEvent;
	public UnityEvent<PlayerRobotEntityInput> playerDiedEvent;
	
	public Vector2 MovementVector {get; private set;}
	public Vector2 LastMovementVector {get; private set;}

	private Vector2 currentMovementVector;
	private PlayerInput playerInput;
	private RobotEntityShootController robotEntityShootController;
	private PlayerRobotEntityMovementController playerRobotEntityMovementController;
	private StageStateManager stageStateManager;
	private StageSceneFlowManager stageSceneFlowManager;

	public void SetLastMovementVector(Vector2 movementVector)
	{
		LastMovementVector = movementVector;
	}

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		robotEntityShootController = GetComponent<RobotEntityShootController>();
		playerRobotEntityMovementController = GetComponent<PlayerRobotEntityMovementController>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();
		
		RegisterToListeners(true);
	}

	private void Start()
	{
		SetControlScheme();
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
		UpdateMovementVector(Vector2.zero);
		playerDiedEvent?.Invoke(this);
	}

	private void SetControlScheme()
	{
		var controlSchemeName = ShouldAssignGamepadControlScheme() ? InputMethods.GAMEPAD_CONTROL_SCHEME_NAME : InputMethods.KEYBOARD_AND_MOUSE_CONTROL_SCHEME_NAME;
	
		InputMethods.SetControlSchemeTo(playerInput, controlSchemeName);
	}

	private bool ShouldAssignGamepadControlScheme()
	{
		var selectedTwoPlayersMode = GameDataMethods.SelectedTwoPlayersMode(gameData);
		var isFirstPlayer = ordinalNumber == 1;

		return Gamepad.current != null && (!selectedTwoPlayersMode || !isFirstPlayer);
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
		var movementVector = currentMovementVector;
		
		if(stageState == StageState.Paused)
		{
			movementVector = playerRobotEntityMovementController.IsSliding ? LastMovementVector : Vector2.zero;
		}
		
		UpdateMovementVector(movementVector);
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
		MovementVector = InputMethods.GetAdjustedMovementVector(movementVector);
		
		movementValueChangedEvent?.Invoke(this, !MovementVector.IsZero());
	}

	private bool GameIsPaused() => stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused);
}