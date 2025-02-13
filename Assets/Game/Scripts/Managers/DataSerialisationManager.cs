using System;
using UnityEngine;

public class DataSerialisationManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;

	public void SerialiseAllData()
	{
		SerialiseGameData();
		SerialisePlayersData();
	}

	public void SerialiseGameData()
	{
		OperateOnGameData(DataSerialisationMethods.SerialiseData);
	}

	public void SerialisePlayersData()
	{
		OperateOnPlayerData(DataSerialisationMethods.SerialiseData);
	}

	public void DeserialiseAllDataIfPossible()
	{
		DeserialiseGameDataIfPossible();
		DeserialisePlayersDataIfPossible();
	}

	public void DeserialiseGameDataIfPossible()
	{
		OperateOnGameData(DataSerialisationMethods.DeserialiseDataIfPossible);
	}

	public void DeserialisePlayersDataIfPossible()
	{
		OperateOnPlayerData(DataSerialisationMethods.DeserialiseDataIfPossible);
	}

	private void OperateOnGameData(Action<string, object> action)
	{
		if(gameData != null)
		{
			action?.Invoke("gameData", gameData.saveableGameData);
		}
	}
	
	private void OperateOnPlayerData(Action<string, object> action)
	{
		if(playerRobotsListData != null)
		{
			playerRobotsListData.GetPlayerRobotsData().ForEachIndexed((playerRobotData, i) => action?.Invoke($"player{i}Data", playerRobotData.saveablePlayerData), 1);
		}
	}
}