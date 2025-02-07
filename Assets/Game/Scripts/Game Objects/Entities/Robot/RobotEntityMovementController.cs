using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RobotEntityRankController), typeof(RobotEntityGameObjectsDetector))]
public class RobotEntityMovementController : EntityMovementController
{
	protected Vector2 lastDirection;

	private RobotEntityRankController robotEntityRankController;
	private RobotEntityGameObjectsDetector robotEntityGameObjectsDetector;

	public Vector2 GetLastDirection() => lastDirection;

	protected override void Awake()
	{
		base.Awake();
		
		robotEntityRankController = GetComponent<RobotEntityRankController>();
		robotEntityGameObjectsDetector = GetComponent<RobotEntityGameObjectsDetector>();

		RegisterToListeners(true);
	}

	protected virtual void RegisterToListeners(bool register)
	{
		if(register)
		{
			robotEntityRankController.rankChangedEvent.AddListener(OnRankChanged);
			robotEntityGameObjectsDetector.detectedGameObjectsUpdatedEvent.AddListener(OnDetectedGameObjectsUpdated);
		}
		else
		{
			robotEntityRankController.rankChangedEvent.RemoveListener(OnRankChanged);
			robotEntityGameObjectsDetector.detectedGameObjectsUpdatedEvent.RemoveListener(OnDetectedGameObjectsUpdated);
		}
	}

	protected virtual void OnDetectedGameObjectsUpdated(List<GameObject> gameObjects)
	{

	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void OnRankChanged(RobotRank robotRank, bool setOnStart)
	{
		if(robotRank != null)
		{
			SetMovementSpeed(robotRank.GetMovementSpeed());
		}
	}
}