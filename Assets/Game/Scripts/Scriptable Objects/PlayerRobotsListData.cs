using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Robots List Data")]
public class PlayerRobotsListData : ScriptableObject
{
	[SerializeField] private List<PlayerRobotData> playerRobotsData = new();

	public bool Any(Func<PlayerRobotData, bool> func) => playerRobotsData.Any(func);

	public void ForEachIndexed(Action<PlayerRobotData, int> action, int counterInitialValue = 0)
	{
		var i = counterInitialValue;

		ForEach(playerRobotData => action?.Invoke(playerRobotData, i++));
	}

	public void ForEach(Action<PlayerRobotData> action)
	{
		playerRobotsData.ForEach(action);
	}
}