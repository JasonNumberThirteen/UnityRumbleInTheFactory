using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerRobotEntityInputController), typeof(PlayerRobotEntityInputTypesActivationController))]
public class PlayerRobotEntityMovementController : RobotEntityMovementController
{
	public UnityEvent<PlayerRobotEntityMovementController, bool> movementValueWasChangedEvent;
	public UnityEvent<PlayerRobotEntityMovementController> playerDiedEvent;
	
	private bool isSliding;
	private bool playedSlidingSound;
	private Vector2 pressedMovementVector;
	private Vector2 currentMovementVector;
	private Vector2 movementVectorWhileSliding;
	private PlayerRobotEntityInputController playerRobotEntityInputController;
	private PlayerRobotEntityInputTypesActivationController playerRobotEntityInputTypesActivationController;
	private StageSoundManager stageSoundManager;
	private StageStateManager stageStateManager;

	private static readonly string ENEMY_LAYER_NAME = "Enemy";
	private static readonly string SLIPPERY_FLOOR_LAYER_NAME = "Slippery Floor";

	public void OnMovementKeyWasPressed(Vector2 movementVector)
	{
		pressedMovementVector = movementVector.GetRawVector();

		if(!CanPerformInputActionOfType(PlayerInputActionType.Movement) || GameIsPaused() || GameIsOver())
		{
			return;
		}

		UpdateMovementVectorWhileSlidingIfNeeded(pressedMovementVector);
		UpdateCurrentMovementVector(pressedMovementVector);
	}

	protected override void Awake()
	{
		playerRobotEntityInputController = GetComponent<PlayerRobotEntityInputController>();
		playerRobotEntityInputTypesActivationController = GetComponent<PlayerRobotEntityInputTypesActivationController>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		
		base.Awake();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		UpdateCurrentMovementVector(Vector2.zero);
		playerDiedEvent?.Invoke(this);
	}

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);

		if(register)
		{
			playerRobotEntityInputController.movementKeyWasPressedEvent.AddListener(OnMovementKeyWasPressed);
			playerRobotEntityInputTypesActivationController.occuredStageEventTypesWereUpdatedEvent.AddListener(OnOccuredStageEventTypesWereUpdated);
			
			if(stageStateManager != null)
			{
				stageStateManager.stageStateWasChangedEvent.AddListener(OnStageStateWasChanged);
			}
		}
		else
		{
			playerRobotEntityInputController.movementKeyWasPressedEvent.RemoveListener(OnMovementKeyWasPressed);
			playerRobotEntityInputTypesActivationController.occuredStageEventTypesWereUpdatedEvent.RemoveListener(OnOccuredStageEventTypesWereUpdated);
			
			if(stageStateManager != null)
			{
				stageStateManager.stageStateWasChangedEvent.RemoveListener(OnStageStateWasChanged);
			}
		}
	}

	protected override void OnDetectedGameObjectsWereUpdated(List<GameObject> gameObjects)
	{
		isSliding = gameObjects != null && gameObjects.Count > 0 && gameObjects.All(go => go.layer == LayerMask.NameToLayer(SLIPPERY_FLOOR_LAYER_NAME));
		rb2D.constraints = gameObjects.Any(go => go.layer == LayerMask.NameToLayer(ENEMY_LAYER_NAME)) ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;

		if(isSliding)
		{
			UpdateMovementVectorsIfNeeded();
			PlaySlidingSoundIfNeeded();
		}
		else if(!isSliding && playedSlidingSound)
		{
			UpdateMovementVectorsIfNeeded();

			playedSlidingSound = false;
		}
	}

	private void UpdateMovementVectorsIfNeeded()
	{
		if(!isSliding && pressedMovementVector.IsZero())
		{
			currentMovementVector = Vector2.zero;
		}
		else if(movementVectorWhileSliding != currentMovementVector && !currentMovementVector.IsZero())
		{
			movementVectorWhileSliding = currentMovementVector;
		}
	}

	private void PlaySlidingSoundIfNeeded()
	{
		if(playedSlidingSound || stageSoundManager == null)
		{
			return;
		}
		
		stageSoundManager.PlaySound(SoundEffectType.PlayerRobotSliding);

		playedSlidingSound = true;
	}

	private void Update()
	{
		UpdateLastDirectionIfNeeded();

		CurrentMovementDirection = isSliding && !movementVectorWhileSliding.IsZero() ? movementVectorWhileSliding : currentMovementVector;
	}

	private void UpdateLastDirectionIfNeeded()
	{
		if(CurrentMovementDirection != lastDirection && !CurrentMovementDirectionIsNone())
		{
			lastDirection = CurrentMovementDirection;
		}
	}

	private void UpdateMovementVectorWhileSlidingIfNeeded(Vector2 movementVector)
	{
		if(!movementVector.IsZero())
		{
			movementVectorWhileSliding = movementVector;
		}
	}

	private void OnOccuredStageEventTypesWereUpdated()
	{
		UpdateCurrentMovementVector(CanPerformInputActionOfType(PlayerInputActionType.Movement) ? pressedMovementVector : GetIdleVectorDependingOnSlidingState());
	}

	private void OnStageStateWasChanged(StageState stageState)
	{
		UpdateCurrentMovementVector((stageState == StageState.Paused || stageState == StageState.Over) ? GetIdleVectorDependingOnSlidingState() : pressedMovementVector);
	}

	private void UpdateCurrentMovementVector(Vector2 movementVector)
	{
		currentMovementVector = movementVector;

		movementValueWasChangedEvent?.Invoke(this, !currentMovementVector.IsZero());
	}

	private bool GameIsPaused() => stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused);
	private bool GameIsOver() => stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Over);
	private Vector2 GetIdleVectorDependingOnSlidingState() => isSliding ? movementVectorWhileSliding : Vector2.zero;
	private bool CanPerformInputActionOfType(PlayerInputActionType playerInputActionType) => playerRobotEntityInputTypesActivationController == null || playerRobotEntityInputTypesActivationController.PlayerCanPerformInputActionOfType(playerInputActionType);
}