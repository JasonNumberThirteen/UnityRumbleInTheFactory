using UnityEngine;

[RequireComponent(typeof(RobotEntityRotation))]
public class RobotEntityMovement : EntityMovement
{
	protected RobotEntityRotation robotEntityRotation;
	protected RobotEntityCollisionDetector robotEntityCollisionDetector;
	protected Vector2 lastDirection;

	protected override void Awake()
	{
		base.Awake();
		
		robotEntityRotation = GetComponent<RobotEntityRotation>();
		robotEntityCollisionDetector = GetComponentInChildren<RobotEntityCollisionDetector>();
	}
}