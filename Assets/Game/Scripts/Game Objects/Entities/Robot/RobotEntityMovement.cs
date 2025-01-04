using UnityEngine;

[RequireComponent(typeof(RobotRotation))]
public class RobotEntityMovement : EntityMovement
{
	protected RobotRotation robotRotation;
	protected RobotCollisionDetector robotCollisionDetector;
	protected Vector2 lastDirection;

	protected override void Awake()
	{
		base.Awake();
		
		robotRotation = GetComponent<RobotRotation>();
		robotCollisionDetector = GetComponentInChildren<RobotCollisionDetector>();
	}
}