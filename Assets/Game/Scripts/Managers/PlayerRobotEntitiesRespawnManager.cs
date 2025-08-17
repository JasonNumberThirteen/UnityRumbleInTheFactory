using System;
using System.Collections;
using UnityEngine;

public class PlayerRobotEntitiesRespawnManager : MonoBehaviour
{
	private StageStateManager stageStateManager;
	private StageEventsManager stageEventsManager;
	private PlayerRobotsDataManager playerRobotsDataManager;
	
	private void Awake()
	{
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageEventsManager = ObjectMethods.FindComponentOfType<StageEventsManager>();
		playerRobotsDataManager = ObjectMethods.FindComponentOfType<PlayerRobotsDataManager>();

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
				stageEventsManager.eventWasSentEvent.AddListener(OnEventWasSent);
			}
		}
		else
		{
			if(stageEventsManager != null)
			{
				stageEventsManager.eventWasSentEvent.RemoveListener(OnEventWasSent);
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

		if(go == null || !go.TryGetComponent(out PlayerRobotEntity playerRobotEntity))
		{
			return;
		}

		var playerRobotData = playerRobotEntity.GetPlayerRobotData();

		SetPlayerRobotStateToNotAliveIfNeeded(playerRobotData);
		InvokeActionDependingOnAllPlayersLives(playerRobotData);
	}

	private void SetPlayerRobotStateToNotAliveIfNeeded(PlayerRobotData playerRobotData)
	{
		if(playerRobotData != null && playerRobotData.Lives == 0)
		{
			playerRobotData.IsAlive = false;
		}
	}

	private void InvokeActionDependingOnAllPlayersLives(PlayerRobotData playerRobotData)
	{
		if(playerRobotData == null)
		{
			return;
		}
		
		var allPlayersLostAllLives = playerRobotsDataManager != null && playerRobotsDataManager.AllPlayersLostAllLives();
		var spawnerIsDefined = playerRobotData != null && playerRobotData.Spawner != null;
		var respawnDelay = spawnerIsDefined ? playerRobotData.Spawner.GetRespawnDelay() : 0f;

		if(stageStateManager != null && allPlayersLostAllLives)
		{
			SetStageState(StageState.Interrupted);
			StartCoroutine(InvokeMethodAfterElapsedTime(() => SetStageState(StageState.Over), respawnDelay));
		}
		else if(spawnerIsDefined)
		{
			StartCoroutine(InvokeMethodAfterElapsedTime(() => RespawnEntityIfPossible(playerRobotData), respawnDelay));
		}
	}

	private IEnumerator InvokeMethodAfterElapsedTime(Action action, float delay)
	{
		yield return new WaitForSeconds(delay);

		action?.Invoke();
	}

	private void SetStageState(StageState stageState)
	{
		if(stageStateManager != null)
		{
			stageStateManager.SetStateTo(stageState);
		}
	}

	private void RespawnEntityIfPossible(PlayerRobotData playerRobotData)
	{
		if(playerRobotData != null && playerRobotData.Spawner != null)
		{
			playerRobotData.Spawner.RespawnEntityIfPossible();
		}
	}
}