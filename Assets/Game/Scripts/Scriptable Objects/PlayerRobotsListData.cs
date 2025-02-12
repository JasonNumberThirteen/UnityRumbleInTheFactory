using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Robots List Data")]
public class PlayerRobotsListData : ScriptableObject
{
	[SerializeField] private List<PlayerRobotData> playerRobotsData = new();

	public List<PlayerRobotData> GetPlayerRobotsData() => playerRobotsData;
}