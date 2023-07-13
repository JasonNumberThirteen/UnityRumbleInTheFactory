using UnityEngine;

public class PlayerRobotTrigger : RobotTrigger
{
	public PlayerData data;
	
	private PlayerRobotMovement movement;

	public override void TriggerEffect()
	{
		PlayerRobotRespawn respawn = GetComponent<PlayerRobotRespawn>();
		
		if(respawn != null)
		{
			StageManager.instance.InitiatePlayerRespawn(respawn);
		}

		base.TriggerEffect();
	}

	private void Awake() => movement = GetComponent<PlayerRobotMovement>();

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("Slippery Floor"))
		{
			SetSliding(true);
		}
		else if(collider.CompareTag("Bonus"))
		{
			BonusEffect be = collider.gameObject.GetComponent<BonusEffect>();

			if(be != null)
			{
				be.PerformEffect();
			}
			
			data.score += StageManager.instance.pointsForBonus;
			
			Destroy(collider.gameObject);
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