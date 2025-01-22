using System.Linq;
using UnityEngine;

public class PlayerTotalDefeatedEnemiesIntCounter : IntCounter
{
	[SerializeField] private PlayerRobotData playerRobotData;
	
	private void Start()
	{
		if(playerRobotData != null)
		{
			SetTo(playerRobotData.DefeatedEnemies.Sum(pair => pair.Value));
		}
	}
}