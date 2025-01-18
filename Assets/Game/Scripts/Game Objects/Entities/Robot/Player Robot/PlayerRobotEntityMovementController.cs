using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntityInput), typeof(PlayerRobotEntityGameObjectsDetector))]
public class PlayerRobotEntityMovementController : RobotEntityMovementController
{
	private PlayerRobotEntityInput playerRobotEntityInput;
	private PlayerRobotEntityGameObjectsDetector playerRobotEntityGameObjectsDetector;
	private bool isSliding;

	private readonly string SLIPPERY_FLOOR_LAYER_NAME = "Slippery Floor";

	public Vector2 GetLastDirection() => lastDirection;

	protected override void Awake()
	{
		playerRobotEntityGameObjectsDetector = GetComponent<PlayerRobotEntityGameObjectsDetector>();
		
		base.Awake();

		playerRobotEntityInput = GetComponent<PlayerRobotEntityInput>();
	}

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);

		if(register)
		{
			playerRobotEntityGameObjectsDetector.detectedGameObjectsUpdatedEvent.AddListener(OnDetectedGameObjectsUpdated);
		}
		else
		{
			playerRobotEntityGameObjectsDetector.detectedGameObjectsUpdatedEvent.RemoveListener(OnDetectedGameObjectsUpdated);
		}
	}

	private void OnDetectedGameObjectsUpdated(List<GameObject> gameObjects)
	{
		isSliding = gameObjects != null && gameObjects.Count > 0 && gameObjects.All(go => go.layer == LayerMask.NameToLayer(SLIPPERY_FLOOR_LAYER_NAME));
	}

	private void Update()
	{
		UpdateLastDirectionIfNeeded();
		UpdateCurrentMovementDirection();
		RotateByDirectionIfNeeded();
		LockMovementWhenHitObject();
	}

	private void UpdateLastDirectionIfNeeded()
	{
		if(IsMovingInDifferentDirection())
		{
			lastDirection = CurrentMovementDirection;
		}
	}

	private void UpdateCurrentMovementDirection()
	{
		CurrentMovementDirection = isSliding ? playerRobotEntityInput.LastMovementVector : GetMovementDirection();
	}

	private Vector2 GetMovementDirection()
	{
		var movementVector = GetMovementVector();

		return PressedHorizontalMovementKey(movementVector) ? Vector2.right*movementVector.x : Vector2.up*movementVector.y;
	}

	private Vector2 GetMovementVector()
	{
		var x = Mathf.RoundToInt(playerRobotEntityInput.MovementVector.x);
		var y = Mathf.RoundToInt(playerRobotEntityInput.MovementVector.y);

		return new Vector2(x, y);
	}

	private void RotateByDirectionIfNeeded()
	{
		if(IsMovingInDifferentDirection())
		{
			robotEntityCollisionDetector.AdjustRotationIfPossible(CurrentMovementDirection);
		}
	}
	
	private void LockMovementWhenHitObject()
	{
		if(robotEntityCollisionDetector != null && robotEntityCollisionDetector.OverlapBox() != null)
		{
			rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		else if(rb2D.constraints != RigidbodyConstraints2D.FreezeRotation)
		{
			rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}

	private bool PressedHorizontalMovementKey(Vector2 movement) => Mathf.Abs(movement.x) > Mathf.Abs(movement.y);
	private bool IsMovingInDifferentDirection() => !CurrentMovementDirectionIsNone() && CurrentMovementDirection != lastDirection;
}