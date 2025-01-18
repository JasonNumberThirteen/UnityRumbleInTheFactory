using UnityEngine;

[RequireComponent(typeof(RobotEntityRankController))]
public class RobotEntityMovementController : EntityMovementController
{
	protected RobotEntityCollisionDetector robotEntityCollisionDetector;
	protected Vector2 lastDirection;

	private RobotEntityRankController robotEntityRankController;

	protected override void Awake()
	{
		base.Awake();
		
		robotEntityCollisionDetector = GetComponentInChildren<RobotEntityCollisionDetector>();
		robotEntityRankController = GetComponent<RobotEntityRankController>();

		RegisterToListeners(true);
	}

	protected virtual void RegisterToListeners(bool register)
	{
		if(register)
		{
			robotEntityRankController.rankChangedEvent.AddListener(OnRankChanged);
		}
		else
		{
			robotEntityRankController.rankChangedEvent.RemoveListener(OnRankChanged);
		}
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void OnRankChanged(RobotRank robotRank)
	{
		SetMovementSpeed(robotRank.GetMovementSpeed());
	}
}