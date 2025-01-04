using UnityEngine;

public class LifeBonusTriggerEventsReceiver : BonusTriggerEventsReceiver
{
	[SerializeField, Min(1)] private int lives = 1;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		AddLifeToPlayerRobotIfPossible(sender);
		base.TriggerOnEnter(sender);
	}

	private void AddLifeToPlayerRobotIfPossible(GameObject sender)
	{
		if(!sender.TryGetComponent(out PlayerRobot playerRobot))
		{
			return;
		}
		
		var playerData = playerRobot.GetPlayerData();

		if(playersDataManager != null && playerData != null)
		{
			playersDataManager.ModifyLives(playerData, lives);
		}
	}
}