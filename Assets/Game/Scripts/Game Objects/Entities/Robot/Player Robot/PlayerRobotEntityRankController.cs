using System;
using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntity))]
public class PlayerRobotEntityRankController : MonoBehaviour
{
	public PlayerRobotRank CurrentRank {get; private set;}

	private PlayerRobotEntity playerRobotEntity;
	private PlayersDataManager playersDataManager;
	
	public void IncreaseRank()
	{
		InvokeActionOnPlayerDataIfPossible(playerData =>
		{
			if(playersDataManager != null)
			{
				playersDataManager.ModifyRank(playerData, 1);
			}
		});
	}

	private void Awake()
	{
		playerRobotEntity = GetComponent<PlayerRobotEntity>();
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
		UpdateValuesUpgradeableByRobotRank();
	}

	private void UpdateValuesUpgradeableByRobotRank()
	{
		var upgradeableByRobotRankComponents = GetComponents<IUpgradeableByRobotRank>();

		foreach (var upgradeableByRobotRank in upgradeableByRobotRankComponents)
		{
			upgradeableByRobotRank.UpdateValuesUpgradeableByRobotRank(CurrentRank);
		}
	}

	private void InvokeActionOnPlayerDataIfPossible(Action<PlayerData> action)
	{
		var playerData = playerRobotEntity.GetPlayerData();

		if(playerData != null)
		{
			action?.Invoke(playerData);
		}
	}
}