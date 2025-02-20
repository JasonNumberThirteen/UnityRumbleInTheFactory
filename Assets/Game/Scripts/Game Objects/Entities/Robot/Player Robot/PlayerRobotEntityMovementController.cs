using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerRobotEntity), typeof(PlayerRobotEntityInputController), typeof(PlayerRobotEntityInputTypesActivationController))]
public class PlayerRobotEntityMovementController : RobotEntityMovementController
{
	public UnityEvent<PlayerRobotEntityMovementController, bool> movementValueChangedEvent;
	public UnityEvent<PlayerRobotEntityMovementController> playerDiedEvent;
	
	private bool isSliding;
	private bool playedSlidingSound;
	private Vector2 pressedMovementVector;
	private Vector2 currentMovementVector;
	private Vector2 movementVectorWhileSliding;
	private PlayerRobotEntity playerRobotEntity;
	private PlayerRobotEntityInputController playerRobotEntityInputController;
	private PlayerRobotEntityInputTypesActivationController playerRobotEntityInputTypesActivationController;
	private StageSoundManager stageSoundManager;
	private StageStateManager stageStateManager;
	private StageEventsManager stageEventsManager;

	private readonly string ENEMY_LAYER_NAME = "Enemy";
	private readonly string SLIPPERY_FLOOR_LAYER_NAME = "Slippery Floor";

	protected override void Awake()
	{
		playerRobotEntity = GetComponent<PlayerRobotEntity>();
		playerRobotEntityInputController = GetComponent<PlayerRobotEntityInputController>();
		playerRobotEntityInputTypesActivationController = GetComponent<PlayerRobotEntityInputTypesActivationController>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageEventsManager = ObjectMethods.FindComponentOfType<StageEventsManager>();
		
		base.Awake();
	}

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);

		if(register)
		{
			playerRobotEntityInputController.movementKeyPressedEvent.AddListener(OnMovementValueChanged);
			
			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}

			if(stageEventsManager != null)
			{
				stageEventsManager.eventReceivedEvent.AddListener(OnEventReceived);
			}
		}
		else
		{
			playerRobotEntityInputController.movementKeyPressedEvent.RemoveListener(OnMovementValueChanged);
			
			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}

			if(stageEventsManager != null)
			{
				stageEventsManager.eventReceivedEvent.RemoveListener(OnEventReceived);
			}
		}
	}

	protected override void OnDetectedGameObjectsUpdated(List<GameObject> gameObjects)
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
		if(!isSliding)
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

	protected override void OnDestroy()
	{
		base.OnDestroy();
		UpdateMovementVector(Vector2.zero);
		playerDiedEvent?.Invoke(this);
	}

	private void OnStageStateChanged(StageState stageState)
	{
		UpdateMovementVector((stageState == StageState.Paused || stageState == StageState.Over) ? GetIdleVectorDependingOnSlidingState() : currentMovementVector);
	}

	private void OnMovementValueChanged(Vector2 movementVector)
	{
		pressedMovementVector = movementVector.GetRawVector();

		if(CanPerformInputActionOfType(PlayerInputActionType.Movement) && !GameIsPaused())
		{
			UpdateMovementVector(pressedMovementVector);
		}
	}

	private void OnEventReceived(StageEvent stageEvent)
	{
		if(stageEvent is RobotEntitiesDisablingStageEvent robotEntitiesDisablingStageEvent && robotEntitiesDisablingStageEvent.AppliesTo(playerRobotEntity))
		{
			UpdateMovementVector(CanPerformInputActionOfType(PlayerInputActionType.Movement) && !robotEntitiesDisablingStageEvent.DisablingIsActive() ? pressedMovementVector : GetIdleVectorDependingOnSlidingState());
		}
	}

	private void UpdateMovementVector(Vector2 movementVector)
	{
		currentMovementVector = movementVector;

		movementValueChangedEvent?.Invoke(this, !currentMovementVector.IsZero());
	}

	private bool GameIsPaused() => stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused);
	private Vector2 GetIdleVectorDependingOnSlidingState() => isSliding ? movementVectorWhileSliding : Vector2.zero;
	private bool CanPerformInputActionOfType(PlayerInputActionType playerInputActionType) => playerRobotEntityInputTypesActivationController == null || playerRobotEntityInputTypesActivationController.PlayerCanPerformInputActionOfType(playerInputActionType);
}