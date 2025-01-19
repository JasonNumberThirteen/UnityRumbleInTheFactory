using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntityInput))]
public class PlayerRobotEntityMovementController : RobotEntityMovementController
{
	private PlayerRobotEntityInput playerRobotEntityInput;
	private bool isSliding;

	private readonly string ENEMY_LAYER_NAME = "Enemy";
	private readonly string SLIPPERY_FLOOR_LAYER_NAME = "Slippery Floor";

	protected override void Awake()
	{
		base.Awake();

		playerRobotEntityInput = GetComponent<PlayerRobotEntityInput>();
	}

	protected override void OnDetectedGameObjectsUpdated(List<GameObject> gameObjects)
	{
		isSliding = gameObjects != null && gameObjects.Count > 0 && gameObjects.All(go => go.layer == LayerMask.NameToLayer(SLIPPERY_FLOOR_LAYER_NAME));
		rb2D.constraints = gameObjects.Any(go => go.layer == LayerMask.NameToLayer(ENEMY_LAYER_NAME)) ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
	}

	private void Update()
	{
		UpdateLastDirectionIfNeeded();

		CurrentMovementDirection = isSliding ? playerRobotEntityInput.LastMovementVector : GetMovementDirection();
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

		return PressedHorizontalMovementKey(movementVector) ? Vector2.right*movementVector.x : Vector2.up*movementVector.y;
	}

	private Vector2 GetMovementVector()
	{
		var x = Mathf.RoundToInt(playerRobotEntityInput.MovementVector.x);
		var y = Mathf.RoundToInt(playerRobotEntityInput.MovementVector.y);

		return new Vector2(x, y);
	}

	private bool PressedHorizontalMovementKey(Vector2 movement) => Mathf.Abs(movement.x) > Mathf.Abs(movement.y);
	private bool IsMovingInDifferentDirection() => !CurrentMovementDirectionIsNone() && CurrentMovementDirection != lastDirection;
}