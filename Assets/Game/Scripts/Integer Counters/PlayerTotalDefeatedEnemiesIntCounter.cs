using System.Linq;
using UnityEngine;

public class PlayerTotalDefeatedEnemiesIntCounter : IntCounter
{
	[SerializeField] private PlayerRobotData playerRobotData;

	public void ActivateIfPossible()
	{
		SetActive(playerRobotData != null && playerRobotData.WasAliveOnCurrentStage);
	}

	public void Deactivate()
	{
		SetActive(false);
	}

	private void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}
	
	private void Start()
	{
		if(playerRobotData != null)
		{
			SetTo(playerRobotData.DefeatedEnemies.Sum(pair => pair.Value));
		}
	}
}