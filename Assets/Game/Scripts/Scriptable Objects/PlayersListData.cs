using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Players List Data")]
public class PlayersListData : ScriptableObject
{
	[SerializeField] private PlayerData[] playersData;

	public void ForEach(Action<PlayerData> action)
	{
		foreach (var playerData in playersData)
		{
			action?.Invoke(playerData);
		}
	}

	public bool Any(Func<PlayerData, bool> func)
	{
		foreach (var playerData in playersData)
		{
			if(func(playerData))
			{
				return true;
			}
		}

		return false;
	}

	public void ResetPlayersData()
	{
		foreach (var playerData in playersData)
		{
			playerData.ResetData();
		}
	}
}