using UnityEngine;

[RequireComponent(typeof(RobotEntityRotationController))]
public class RobotEntityMovementController : EntityMovementController
{
	protected RobotEntityRotationController robotEntityRotationController;
	protected RobotEntityCollisionDetector robotEntityCollisionDetector;
	protected Vector2 lastDirection;

	protected override void Awake()
	{
		base.Awake();
		
		robotEntityRotationController = GetComponent<RobotEntityRotationController>();
		robotEntityCollisionDetector = GetComponentInChildren<RobotEntityCollisionDetector>();
	}
}