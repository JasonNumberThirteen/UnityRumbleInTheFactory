using UnityEngine;

public class RobotEntityMovementController : EntityMovementController
{
	protected RobotEntityCollisionDetector robotEntityCollisionDetector;
	protected Vector2 lastDirection;

	protected override void Awake()
	{
		base.Awake();
		
		robotEntityCollisionDetector = GetComponentInChildren<RobotEntityCollisionDetector>();
	}
}