using System;
using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntity))]
public class PlayerRobotEntityRankController : RobotEntityRankController<PlayerRobotRank>
{
	private PlayersDataManager playersDataManager;
	
	public override void IncreaseRank()
	{
		InvokeActionOnPlayerDataIfPossible(playerData =>
		{
			if(playersDataManager != null)
			{
				playersDataManager.ModifyRank(playerData, 1);
			}
		});
	}

	protected override void Awake()
	{
		base.Awake();
		
		playersDataManager = FindAnyObjectByType<PlayersDataManager>();

		RegisterToListeners(true);
	}

	private void Start()
	{
		OnPlayerRankChanged();
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(playersDataManager != null)
			{
				playersDataManager.playerRankChangedEvent.AddListener(OnPlayerRankChanged);
			}
		}
		else
		{
			if(playersDataManager != null)
			{
				playersDataManager.playerRankChangedEvent.RemoveListener(OnPlayerRankChanged);
			}
		}
	}

	private void OnPlayerRankChanged()
	{
		InvokeActionOnPlayerDataIfPossible(playerData => CurrentRank = playerData.GetRank());
		rankChangedEvent?.Invoke(CurrentRank);
	}

	private void InvokeActionOnPlayerDataIfPossible(Action<PlayerData> action)
	{
		if(robotEntity is not PlayerRobotEntity playerRobotEntity)
		{
			return;
		}
		
		var playerData = playerRobotEntity.GetPlayerData();

		if(playerData != null)
		{
			action?.Invoke(playerData);
		}
	}
}