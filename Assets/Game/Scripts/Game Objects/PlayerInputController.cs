using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
	private int playerOrdinalNumber;
	private Vector2 pressedMovementVector;
	private PlayerInput playerInput;
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

	public void Setup(PlayerInput playerInput, PlayerRobotEntitySpawner playerRobotEntitySpawner)
	{
		this.playerInput = playerInput;
		PlayerRobotEntitySpawner = playerRobotEntitySpawner;
		playerOrdinalNumber = RobotDataMethods.GetOrdinalNumber(this.playerRobotEntitySpawner.GetPlayerRobotData());

		SetControlSchemeIfNeeded();
		ActivateInputIfPossible();
	}

	private void SetControlSchemeIfNeeded()
	{
		var gamepadIsAvailable = InputMethods.GamepadIsAvailable();
		var twoPlayerModeWasSelected = stagePlayersInputManager != null && GameDataMethods.SelectedTwoPlayerMode(stagePlayersInputManager.GetGameData());
		
		if(playerOrdinalNumber == 1)
		{
			var controlSchemeName = gamepadIsAvailable && !twoPlayerModeWasSelected ? InputMethods.GAMEPAD_CONTROL_SCHEME_NAME : InputMethods.KEYBOARD_AND_MOUSE_CONTROL_SCHEME_NAME;
			
			InputMethods.SetControlSchemeToPlayerInputIfPossible(playerInput, controlSchemeName);
		}
		else if(playerOrdinalNumber == 2 && gamepadIsAvailable)
		{
			InputMethods.SetControlSchemeToPlayerInputIfPossible(playerInput, InputMethods.GAMEPAD_CONTROL_SCHEME_NAME);
		}
	}

	private void ActivateInputIfPossible()
	{
		if(playerInput != null)
		{
			playerInput.ActivateInput();
		}
	}

	private void Awake()
	{
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();
		stagePlayersInputManager = ObjectMethods.FindComponentOfType<StagePlayersInputManager>();
	}

	private void OnDestroy()
	{
		PlayerRobotEntitySpawner = null;
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

	private void OnMoveP1(InputValue inputValue)
	{
		InvokeActionIfConditionIsMet(OnMove, inputValue, playerOrdinalNumber == 1);
	}

	private void OnMoveP2(InputValue inputValue)
	{
		InvokeActionIfConditionIsMet(OnMove, inputValue, playerOrdinalNumber == 2);
	}

	private void OnMove(InputValue inputValue)
	{
		pressedMovementVector = inputValue.Get<Vector2>();
		
		if(playerRobotEntityInputController != null)
		{
			playerRobotEntityInputController.OnMove(inputValue);
		}
	}

	private void OnFireP1(InputValue inputValue)
	{
		InvokeActionIfConditionIsMet(OnFire, inputValue, playerOrdinalNumber == 1);
	}

	private void OnFireP2(InputValue inputValue)
	{
		InvokeActionIfConditionIsMet(OnFire, inputValue, playerOrdinalNumber == 2);
	}

	private void OnFire(InputValue inputValue)
	{
		if(playerRobotEntityInputController != null)
		{
			playerRobotEntityInputController.OnFire(inputValue);
		}
	}

	private void OnPauseP1(InputValue inputValue)
	{
		InvokeActionIfConditionIsMet(OnPause, inputValue, playerOrdinalNumber == 1);
	}

	private void OnPauseP2(InputValue inputValue)
	{
		InvokeActionIfConditionIsMet(OnPause, inputValue, playerOrdinalNumber == 2);
	}

	private void OnPause(InputValue inputValue)
	{
		if(stageSceneFlowManager != null)
		{
			stageSceneFlowManager.PauseGameIfPossible();
		}
	}

	private void InvokeActionIfConditionIsMet(Action<InputValue> action, InputValue inputValue, bool condition)
	{
		if(condition)
		{
			action?.Invoke(inputValue);
		}
	}
}