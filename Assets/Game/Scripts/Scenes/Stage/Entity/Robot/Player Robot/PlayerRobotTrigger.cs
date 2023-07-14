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

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("Slippery Floor"))
		{
			SetSliding(true);
		}
		else if(collider.CompareTag("Bonus"))
		{
			ITriggerable triggerable = collider.gameObject.GetComponent<ITriggerable>();

			if(triggerable != null)
			{
				triggerable.TriggerEffect(gameObject);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.CompareTag("Slippery Floor"))
		{
			SetSliding(false);
		}
	}

	private void SetSliding(bool isSliding) => movement.IsSliding = isSliding;
}