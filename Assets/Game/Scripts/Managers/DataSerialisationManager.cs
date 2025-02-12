using System;
using UnityEngine;

public class DataSerialisationManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;

	public void SaveAllData()
	{
		SaveGameData();
		SavePlayersData();
	}

	public void SaveGameData()
	{
		OperateOnGameDataIfPossible(DataSerialisationMethods.SaveData);
	}

	public void SavePlayersData()
	{
		OperateOnPlayerRobotDataIfPossible(DataSerialisationMethods.SaveData);
	}

	public void LoadAllData()
	{
		LoadGameData();
		LoadPlayersData();
	}

	public void LoadGameData()
	{
		OperateOnGameDataIfPossible(DataSerialisationMethods.LoadDataIfPossible);
	}

	public void LoadPlayersData()
	{
		OperateOnPlayerRobotDataIfPossible(DataSerialisationMethods.LoadDataIfPossible);
	}

	private void OperateOnGameDataIfPossible(Action<string, object> action)
	{
		if(gameData != null)
		{
			action?.Invoke("gameData", gameData.saveableGameData);
		}
	}
	
	private void OperateOnPlayerRobotDataIfPossible(Action<string, object> action)
	{
		if(playerRobotsListData != null)
		{
			playerRobotsListData.GetPlayerRobotsData().ForEachIndexed((playerRobotData, i) => action?.Invoke($"player{i}Data", playerRobotData.saveablePlayerData), 1);
		}
	}
}