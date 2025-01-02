using System.Linq;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
	[SerializeField] private PlayersListData playersListData;

	public void ResetDefeatedEnemiesByPlayer()
	{
		if(playersListData != null)
		{
			playersListData.ForEach(playerData => playerData.ResetDefeatedEnemies());
		}
	}

	public void CheckPlayersLives()
	{
		if(playersListData != null && !playersListData.Any(playerData => playerData.Lives > 0))
		{
			StageManager.instance.SetGameAsOver();
		}
	}

	public void DisablePlayers()
	{
		var robots = FindObjectsByType<Robot>(FindObjectsSortMode.None).Where(robot => robot.IsFriendly());
		var robotDisablerComponents = robots.Select(robot => robot.GetComponent<RobotDisabler>()).Where(component => component != null);

		foreach (var robotDisabler in robotDisablerComponents)
		{
			robotDisabler.SetBehavioursActive(false);
		}
	}
}