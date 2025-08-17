using System.Collections.Generic;
using UnityEngine;

public class PlayerRobotEntitiesTracker : MonoBehaviour
{
	private readonly List<PlayerRobotEntity> playerRobotEntities = new();
	
	private PlayerRobotEntitySpawner[] activePlayerRobotEntitySpawners;
	private StageEventsManager stageEventsManager;
	private StageSceneFlowManager stageSceneFlowManager;

	public List<PlayerRobotEntity> GetPlayerRobotEntities() => playerRobotEntities;

	private void Awake()
	{
		stageEventsManager = ObjectMethods.FindComponentOfType<StageEventsManager>();
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
		RegisterToActivePlayerRobotEntitySpawnersListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(stageEventsManager != null)
			{
				stageEventsManager.eventWasSentEvent.AddListener(OnEventWasSent);
			}
			
			if(stageSceneFlowManager != null)
			{
				stageSceneFlowManager.stageWasActivatedEvent.AddListener(OnStageWasActivated);
			}
		}
		else
		{
			if(stageEventsManager != null)
			{
				stageEventsManager.eventWasSentEvent.RemoveListener(OnEventWasSent);
			}

			if(stageSceneFlowManager != null)
			{
				stageSceneFlowManager.stageWasActivatedEvent.RemoveListener(OnStageWasActivated);
			}
		}
	}

	private void OnEventWasSent(StageEvent stageEvent)
	{
		if(stageEvent is not GameObjectStageEvent gameObjectStageEvent || gameObjectStageEvent.GetStageEventType() != StageEventType.PlayerWasDestroyed)
		{
			return;
		}

		var go = gameObjectStageEvent.GetGO();

		if(go != null && go.TryGetComponent(out PlayerRobotEntity playerRobotEntity) && playerRobotEntities.Contains(playerRobotEntity))
		{
			playerRobotEntities.Remove(playerRobotEntity);
		}
	}

	private void OnStageWasActivated()
	{
		activePlayerRobotEntitySpawners = ObjectMethods.FindComponentsOfType<PlayerRobotEntitySpawner>(false);

		RegisterToActivePlayerRobotEntitySpawnersListeners(true);
	}

	private void RegisterToActivePlayerRobotEntitySpawnersListeners(bool register)
	{
		activePlayerRobotEntitySpawners?.ForEach(playerRobotEntitySpawner =>
		{
			if(register)
			{
				playerRobotEntitySpawner.entityWasSpawnedEvent.AddListener(OnEntityWasSpawned);
			}
			else
			{
				playerRobotEntitySpawner.entityWasSpawnedEvent.RemoveListener(OnEntityWasSpawned);
			}
		});
	}

	private void OnEntityWasSpawned(GameObject go)
	{
		if(go != null && go.TryGetComponent(out PlayerRobotEntity playerRobotEntity) && !playerRobotEntities.Contains(playerRobotEntity))
		{
			playerRobotEntities.Add(playerRobotEntity);
		}
	}
}