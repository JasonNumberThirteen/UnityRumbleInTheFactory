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
			robotEntityRankController.rankWasChangedEvent.AddListener(OnRankWasChanged);
			robotEntityGameObjectsDetector.detectedGameObjectsWereUpdatedEvent.AddListener(OnDetectedGameObjectsWereUpdated);
		}
		else
		{
			robotEntityRankController.rankWasChangedEvent.RemoveListener(OnRankWasChanged);
			robotEntityGameObjectsDetector.detectedGameObjectsWereUpdatedEvent.RemoveListener(OnDetectedGameObjectsWereUpdated);
		}
	}

	protected virtual void OnDetectedGameObjectsWereUpdated(List<GameObject> gameObjects)
	{

	}

	protected virtual void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void OnRankWasChanged(RobotRank robotRank, bool setOnStart)
	{
		if(robotRank != null)
		{
			SetMovementSpeed(robotRank.GetMovementSpeed());
		}
	}
}