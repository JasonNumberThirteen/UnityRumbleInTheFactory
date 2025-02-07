using UnityEngine;

public class PlayerRobotEntityHealth : RobotEntityHealth
{
	[SerializeField] private PlayerRobotData playerRobotData;
	
	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);

		if(register)
		{
			currentHealthValueChangedEvent.AddListener(OnCurrentHealthValueChanged);
		}
		else
		{
			currentHealthValueChangedEvent.RemoveListener(OnCurrentHealthValueChanged);
		}
	}

	protected override void OnRankChanged(RobotRank robotRank, bool setOnStart)
	{
		if(setOnStart && playerRobotData != null && playerRobotData.Spawner != null && playerRobotData.Spawner.NumberOfSpawns == 1)
		{
			SetCurrentHealth(playerRobotData.CurrentHealth);
		}
		else
		{
			base.OnRankChanged(robotRank, setOnStart);
		}
	}

	private void OnCurrentHealthValueChanged(int currentHealth)
	{
		if(playerRobotData != null)
		{
			playerRobotData.CurrentHealth = currentHealth;
		}
	}
}