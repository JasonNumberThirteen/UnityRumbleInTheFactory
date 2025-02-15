using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntityInput))]
public class PlayerRobotEntityMovementController : RobotEntityMovementController
{
	public bool IsSliding {get; private set;}
	
	private PlayerRobotEntityInput playerRobotEntityInput;
	private StageSoundManager stageSoundManager;
	private bool playedSlidingSound;

	private readonly string ENEMY_LAYER_NAME = "Enemy";
	private readonly string SLIPPERY_FLOOR_LAYER_NAME = "Slippery Floor";

	protected override void Awake()
	{
		base.Awake();

		playerRobotEntityInput = GetComponent<PlayerRobotEntityInput>();
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
			playedSlidingSound = false;
		}
	}

	private void SetLastMovementVectorIfNeeded()
	{
		if(playerRobotEntityInput.LastMovementVector != playerRobotEntityInput.MovementVector && !playerRobotEntityInput.MovementVector.IsZero())
		{
			playerRobotEntityInput.SetLastMovementVector(playerRobotEntityInput.MovementVector);
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

		CurrentMovementDirection = IsSliding && !playerRobotEntityInput.LastMovementVector.IsZero() ? playerRobotEntityInput.LastMovementVector : GetMovementDirection();
	}

	private void UpdateLastDirectionIfNeeded()
	{
		if(IsMovingInDifferentDirection())
		{
			lastDirection = CurrentMovementDirection;
		}
	}

	private Vector2 GetMovementDirection()
	{
		var movementVector = GetMovementVector();

		return InputMethods.PressedHorizontalMovementKey(movementVector) ? Vector2.right*movementVector.x : Vector2.up*movementVector.y;
	}

	private Vector2 GetMovementVector()
	{
		var x = Mathf.RoundToInt(playerRobotEntityInput.MovementVector.x);
		var y = Mathf.RoundToInt(playerRobotEntityInput.MovementVector.y);

		return new Vector2(x, y);
	}
	private bool IsMovingInDifferentDirection() => !CurrentMovementDirectionIsNone() && CurrentMovementDirection != lastDirection;
}