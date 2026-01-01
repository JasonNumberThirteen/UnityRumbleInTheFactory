using UnityEngine;

public class PlayerRobotEntityHealth : RobotEntityHealth
{
	[SerializeField] private PlayerRobotData playerRobotData;
	
	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);

		if(register)
		{
			currentHealthValueWasChangedEvent.AddListener(OnCurrentHealthValueWasChanged);
		}
		else
		{
			currentHealthValueWasChangedEvent.RemoveListener(OnCurrentHealthValueWasChanged);
		}
	}

	protected override void OnRankWasChanged(RobotRank robotRank, bool setOnStart)
	{
		if(setOnStart && playerRobotData != null && playerRobotData.Spawner != null && playerRobotData.Spawner.NumberOfSpawns == 1)
		{
			SetCurrentHealth(playerRobotData.CurrentHealth);
		}
		else
		{
			base.OnRankWasChanged(robotRank, setOnStart);
		}
	}

	private void OnCurrentHealthValueWasChanged(int currentHealth)
	{
		if(playerRobotData != null)
		{
			playerRobotData.CurrentHealth = currentHealth;
		}
	}
}