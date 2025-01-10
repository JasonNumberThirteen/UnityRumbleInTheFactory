using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Robots List Data")]
public class PlayerRobotsListData : ScriptableObject
{
	[SerializeField] private List<PlayerRobotData> playerRobotsData = new();

	public bool Any(Func<PlayerRobotData, bool> func) => playerRobotsData.Any(func);

	public void ForEach(Action<PlayerRobotData> action)
	{
		playerRobotsData.ForEach(action);
	}
}