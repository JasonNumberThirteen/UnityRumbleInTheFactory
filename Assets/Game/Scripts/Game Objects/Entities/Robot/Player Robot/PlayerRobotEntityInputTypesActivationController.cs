using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntity))]
public class PlayerRobotEntityInputTypesActivationController : MonoBehaviour
{
	private PlayerRobotEntity playerRobotEntity;
	private StageEventsManager stageEventsManager;
	
	private readonly Dictionary<PlayerInputActionType, HashSet<StageEventType>> stageEventTypesByPlayerInputActionType = new()
	{
		{PlayerInputActionType.Movement, new HashSet<StageEventType>{StageEventType.RobotsActivationStateWasChanged, StageEventType.PlayerRobotActivationStateWasChangedByFriendlyFire}},
		{PlayerInputActionType.Shoot, new HashSet<StageEventType>{StageEventType.RobotsActivationStateWasChanged}}
	};
	private readonly HashSet<StageEventType> occuredStageEventTypes = new();

	public bool PlayerCanPerformInputActionOfType(PlayerInputActionType playerInputActionType) => !stageEventTypesByPlayerInputActionType.TryGetValue(playerInputActionType, out var stageEventTypes) || !occuredStageEventTypes.Any(stageEventTypes.Contains);

	private void Awake()
	{
		playerRobotEntity = GetComponent<PlayerRobotEntity>();
		stageEventsManager = ObjectMethods.FindComponentOfType<StageEventsManager>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(stageEventsManager != null)
			{
				stageEventsManager.eventReceivedEvent.AddListener(OnEventReceived);
			}
		}
		else
		{
			if(stageEventsManager != null)
			{
				stageEventsManager.eventReceivedEvent.RemoveListener(OnEventReceived);
			}
		}
	}

	private void OnEventReceived(StageEvent stageEvent)
	{
		if(stageEvent is not RobotEntitiesDisablingStageEvent robotEntitiesDisablingStateEvent || !robotEntitiesDisablingStateEvent.AppliesTo(playerRobotEntity))
		{
			return;
		}
		
		var stageEventType = robotEntitiesDisablingStateEvent.GetStageEventType();

		if(robotEntitiesDisablingStateEvent.DisablingIsActive())
		{
			occuredStageEventTypes.Add(stageEventType);
		}
		else
		{
			occuredStageEventTypes.Remove(stageEventType);
		}
	}
}