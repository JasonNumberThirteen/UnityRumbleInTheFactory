using UnityEngine;

public class PlayerRobotTrigger : RobotTrigger
{
	public PlayerData data;
	
	private PlayerRobotMovement movement;

	public override void TriggerEffect(GameObject sender)
	{
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
		if(collider.CompareTag("Slippery Floor") && movement.IsSliding)
		{
			movement.IsSliding = false;
		}
	}
}