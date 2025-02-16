using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(PlayerRobotEntityMovementController))]
public class PlayerRobotEntityInputController : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField, Min(1)] private int ordinalNumber;
	
	public UnityEvent<PlayerRobotEntityInputController, bool> movementValueChangedEvent;
	public UnityEvent<PlayerRobotEntityInputController> playerDiedEvent;
	
	public Vector2 MovementVector {get; private set;}
	public Vector2 LastMovementVector {get; private set;}

	private Vector2 currentMovementVector;
	private PlayerInput playerInput;
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
		UpdateMovementVector(stageState == StageState.Paused ? GetIdleVectorDependingOnSlidingState() : currentMovementVector);
	}

	private void OnMove(InputValue inputValue)
	{
		currentMovementVector = inputValue.Get<Vector2>();

		if(enabled && !GameIsPaused())
		{
			UpdateMovementVector(currentMovementVector);
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
		UpdateMovementVector(GetIdleVectorDependingOnSlidingState());
	}

	private void UpdateMovementVector(Vector2 movementVector)
	{
		MovementVector = InputMethods.GetAdjustedMovementVector(movementVector);
		
		movementValueChangedEvent?.Invoke(this, !MovementVector.IsZero());
	}

	private bool GameIsPaused() => stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused);
	private Vector2 GetIdleVectorDependingOnSlidingState() => playerRobotEntityMovementController.IsSliding ? LastMovementVector : Vector2.zero;
}