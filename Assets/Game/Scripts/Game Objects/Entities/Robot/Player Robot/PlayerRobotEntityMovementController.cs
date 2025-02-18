using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerRobotEntityInputController))]
public class PlayerRobotEntityMovementController : RobotEntityMovementController
{
	public UnityEvent<PlayerRobotEntityMovementController, bool> movementValueChangedEvent;
	public UnityEvent<PlayerRobotEntityMovementController> playerDiedEvent;
	
	public bool IsSliding {get; private set;}
	public Vector2 MovementVector {get; set;}
	public Vector2 LastMovementVector {get; private set;} = Vector2.one;
	
	private Vector2 currentMovementVector;
	private PlayerRobotEntityInputController playerRobotEntityInputController;
	private StageSoundManager stageSoundManager;
	private StageStateManager stageStateManager;
	private bool playedSlidingSound;

	private readonly string ENEMY_LAYER_NAME = "Enemy";
	private readonly string SLIPPERY_FLOOR_LAYER_NAME = "Slippery Floor";

	protected override void Awake()
	{
		playerRobotEntityInputController = GetComponent<PlayerRobotEntityInputController>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		
		base.Awake();
	}

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);

		if(register)
		{
			playerRobotEntityInputController.movementValueChangedEvent.AddListener(OnMovementValueChanged);
			
			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}
		}
		else
		{
			playerRobotEntityInputController.movementValueChangedEvent.RemoveListener(OnMovementValueChanged);
			
			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}
		}
	}

	protected override void OnDetectedGameObjectsUpdated(List<GameObject> gameObjects)
	{
		IsSliding = gameObjects != null && gameObjects.Count > 0 && gameObjects.All(go => go.layer == LayerMask.NameToLayer(SLIPPERY_FLOOR_LAYER_NAME));
		rb2D.constraints = gameObjects.Any(go => go.layer == LayerMask.NameToLayer(ENEMY_LAYER_NAME)) ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;

		if(IsSliding)
		{
			UpdateMovementVectorsIfNeeded();
			PlaySlidingSoundIfNeeded();
		}
		else if(!IsSliding && playedSlidingSound)
		{
			UpdateMovementVectorsIfNeeded();

			playedSlidingSound = false;
		}
	}

	private void UpdateMovementVectorsIfNeeded()
	{
		if(!IsSliding && !playerRobotEntityInputController.enabled)
		{
			MovementVector = Vector2.zero;
		}
		else if(LastMovementVector != MovementVector && !MovementVector.IsZero())
		{
			LastMovementVector = MovementVector;
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

		CurrentMovementDirection = IsSliding && !LastMovementVector.IsZero() ? LastMovementVector : MovementVector;
	}

	private void UpdateLastDirectionIfNeeded()
	{
		if(IsMovingInDifferentDirection())
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
		currentMovementVector = movementVector;

		if(enabled && !GameIsPaused())
		{
			UpdateMovementVector(currentMovementVector);
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
	private Vector2 GetIdleVectorDependingOnSlidingState() => IsSliding ? LastMovementVector : Vector2.zero;
	private bool IsMovingInDifferentDirection() => !CurrentMovementDirectionIsNone() && CurrentMovementDirection != lastDirection;
}