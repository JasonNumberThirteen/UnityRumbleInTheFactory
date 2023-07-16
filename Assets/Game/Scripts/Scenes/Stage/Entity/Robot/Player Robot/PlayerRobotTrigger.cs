using UnityEngine;

public class PlayerRobotTrigger : RobotTrigger
{
	public PlayerData data;
	
	private PlayerRobotMovement movement;

	public override void TriggerEffect(GameObject sender)
	{
		PlayerRobotShield shield = GetComponent<PlayerRobotShield>();

		if(shield != null)
		{
			if(shield.ShieldTimer.gameObject.activeInHierarchy)
			{
				return;
			}
		}
		
		PlayerRobotRespawn respawn = GetComponent<PlayerRobotRespawn>();
		
		if(respawn != null)
		{
			StageManager.instance.InitiatePlayerRespawn(respawn);
		}

		base.TriggerEffect(sender);
	}

	protected override void Awake()
	{
		base.Awake();
		
		movement = GetComponent<PlayerRobotMovement>();
	}

	private void OnTriggerStay2D(Collider2D collider)
	{
		ITriggerable triggerable = collider.gameObject.GetComponent<ITriggerable>();

		if(triggerable != null)
		{
			triggerable.TriggerEffect(gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		IReversibleTrigger reversibleTrigger = collider.gameObject.GetComponent<IReversibleTrigger>();

		if(reversibleTrigger != null)
		{
			reversibleTrigger.ReverseTriggerEffect(gameObject);
		}
	}
}