using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntityInputController))]
public class PlayerRobotEntityMovementController : RobotEntityMovementController
{
	public bool IsSliding {get; private set;}
	
	private PlayerRobotEntityInputController playerRobotEntityInputController;
	private StageSoundManager stageSoundManager;
	private bool playedSlidingSound;

	private readonly string ENEMY_LAYER_NAME = "Enemy";
	private readonly string SLIPPERY_FLOOR_LAYER_NAME = "Slippery Floor";

	protected override void Awake()
	{
		base.Awake();

		playerRobotEntityInputController = GetComponent<PlayerRobotEntityInputController>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();
	}

	protected override void OnDetectedGameObjectsUpdated(List<GameObject> gameObjects)
	{
		IsSliding = gameObjects != null && gameObjects.Count > 0 && gameObjects.All(go => go.layer == LayerMask.NameToLayer(SLIPPERY_FLOOR_LAYER_NAME));
		rb2D.constraints = gameObjects.Any(go => go.layer == LayerMask.NameToLayer(ENEMY_LAYER_NAME)) ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;

		if(IsSliding)
		{
			SetLastMovementVectorIfNeeded();
			PlaySlidingSoundIfNeeded();
		}
		else if(!IsSliding && playedSlidingSound)
		{
			SetLastMovementVectorIfNeeded();
			
			playedSlidingSound = false;
		}
	}

	private void SetLastMovementVectorIfNeeded()
	{
		if(playerRobotEntityInputController.LastMovementVector != playerRobotEntityInputController.MovementVector && !playerRobotEntityInputController.MovementVector.IsZero())
		{
			playerRobotEntityInputController.SetLastMovementVector(playerRobotEntityInputController.MovementVector);
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

		CurrentMovementDirection = IsSliding && !playerRobotEntityInputController.LastMovementVector.IsZero() ? playerRobotEntityInputController.LastMovementVector : playerRobotEntityInputController.MovementVector;
	}

	private void UpdateLastDirectionIfNeeded()
	{
		if(IsMovingInDifferentDirection())
		{
			lastDirection = CurrentMovementDirection;
		}
	}
	
	private bool IsMovingInDifferentDirection() => !CurrentMovementDirectionIsNone() && CurrentMovementDirection != lastDirection;
}