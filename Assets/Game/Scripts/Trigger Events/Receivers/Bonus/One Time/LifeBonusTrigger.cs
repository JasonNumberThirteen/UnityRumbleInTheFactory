using UnityEngine;

public class LifeBonusTrigger : BonusTriggerEventsReceiver
{
	public override void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobot playerRobot))
		{
			var playerData = playerRobot.GetPlayerData();

			if(playersDataManager != null && playerData != null)
			{
				playersDataManager.ModifyLives(playerData, 1);
			}
		}

		base.TriggerOnEnter(sender);
	}
}