using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
	private int playerOrdinalNumber;
	private Vector2 pressedMovementVector;
	private InputAction movementInputAction;
	private InputAction fireInputAction;
	private InputAction pauseInputAction;
	private PlayerRobotEntitySpawner playerRobotEntitySpawner;
	private PlayerRobotEntityInputController playerRobotEntityInputController;
	private StageSceneFlowManager stageSceneFlowManager;
	private StagePlayersInputManager stagePlayersInputManager;

	private PlayerRobotEntitySpawner PlayerRobotEntitySpawner
	{
		set
		{
			if(playerRobotEntitySpawner != null)
			{
				RegisterToPlayerRobotEntitySpawnerListeners(false);
			}

			playerRobotEntitySpawner = value;

			if(playerRobotEntitySpawner != null)
			{
				RegisterToPlayerRobotEntitySpawnerListeners(true);
			}
		}
	}

	public void Setup(PlayerRobotEntitySpawner playerRobotEntitySpawner)
	{
		PlayerRobotEntitySpawner = playerRobotEntitySpawner;
		playerOrdinalNumber = RobotDataMethods.GetOrdinalNumber(this.playerRobotEntitySpawner.GetPlayerRobotData());
		
		GetInputActions();
		RegisterToInputActionCallbacks(true);
	}
	
	private void GetInputActions()
	{
		if(stagePlayersInputManager == null)
		{
			return;
		}
		
		var inputActions = stagePlayersInputManager.GetInputActions();

		if(inputActions == null)
		{
			return;
		}

		movementInputAction = inputActions.FindAction(GetInputActionName("Move"));
		fireInputAction = inputActions.FindAction(GetInputActionName("Fire"));
		pauseInputAction = inputActions.FindAction(GetInputActionName("Pause"));

		inputActions.Enable();
	}

	private string GetInputActionName(string prefix)
	{
		var keyboardInputActionName = $"{prefix}ByKeyboardP{playerOrdinalNumber}";
		
		if(!InputMethods.GamepadIsAvailable())
		{
			return keyboardInputActionName;
		}

		var twoPlayerModeWasSelected = stagePlayersInputManager != null && GameDataMethods.SelectedTwoPlayerMode(stagePlayersInputManager.GetGameData());
		
		return ((playerOrdinalNumber == 1 && !twoPlayerModeWasSelected) || playerOrdinalNumber == 2) ? $"{prefix}ByGamepad" : keyboardInputActionName;
	}

	private void Awake()
	{
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();
		stagePlayersInputManager = ObjectMethods.FindComponentOfType<StagePlayersInputManager>();
	}

	private void OnDestroy()
	{
		PlayerRobotEntitySpawner = null;

		RegisterToInputActionCallbacks(false);
	}

	private void RegisterToInputActionCallbacks(bool register)
	{
		if(register)
		{
			if(movementInputAction != null)
			{
				movementInputAction.performed += OnMovementInputActionWasPerformed;
				movementInputAction.canceled += OnMovementInputActionWasCanceled;
			}
			
			if(fireInputAction != null)
			{
				fireInputAction.performed += OnFireInputActionWasPerformed;
			}

			if(pauseInputAction != null)
			{
				pauseInputAction.performed += OnPauseInputActionWasPerformed;
			}
		}
		else
		{
			if(movementInputAction != null)
			{
				movementInputAction.performed -= OnMovementInputActionWasPerformed;
				movementInputAction.canceled -= OnMovementInputActionWasCanceled;
			}
			
			if(fireInputAction != null)
			{
				fireInputAction.performed -= OnFireInputActionWasPerformed;
			}

			if(pauseInputAction != null)
			{
				pauseInputAction.performed -= OnPauseInputActionWasPerformed;
			}
		}
	}
	
	private void OnMovementInputActionWasPerformed(InputAction.CallbackContext callbackContext)
	{
		OnMove(callbackContext.ReadValue<Vector2>());
	}

	private void OnMovementInputActionWasCanceled(InputAction.CallbackContext callbackContext)
	{
		OnMove(Vector2.zero);
	}

	private void OnMove(Vector2 movementVector)
	{
		pressedMovementVector = movementVector;
		
		if(playerRobotEntityInputController != null)
		{
			playerRobotEntityInputController.OnMove(pressedMovementVector);
		}
	}

	private void OnFireInputActionWasPerformed(InputAction.CallbackContext callbackContext)
	{
		if(playerRobotEntityInputController != null)
		{
			playerRobotEntityInputController.OnFire();
		}
	}

	private void OnPauseInputActionWasPerformed(InputAction.CallbackContext callbackContext)
	{
		if(stageSceneFlowManager != null)
		{
			stageSceneFlowManager.PauseGameIfPossible();
		}
	}

	private void RegisterToPlayerRobotEntitySpawnerListeners(bool register)
	{
		if(register)
		{
			if(playerRobotEntitySpawner != null)
			{
				playerRobotEntitySpawner.entityWasSpawnedEvent.AddListener(OnEntityWasSpawned);
			}
		}
		else
		{
			if(playerRobotEntitySpawner != null)
			{
				playerRobotEntitySpawner.entityWasSpawnedEvent.RemoveListener(OnEntityWasSpawned);
			}
		}
	}

	private void OnEntityWasSpawned(GameObject go)
	{
		if(go.TryGetComponent(out PlayerRobotEntityInputController playerRobotEntityInputController))
		{
			this.playerRobotEntityInputController = playerRobotEntityInputController;
		}

		if(go.TryGetComponent(out PlayerRobotEntityMovementController playerRobotEntityMovementController))
		{
			playerRobotEntityMovementController.OnMovementKeyWasPressed(pressedMovementVector);
		}
	}
}