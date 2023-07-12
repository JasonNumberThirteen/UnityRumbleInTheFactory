using UnityEngine;

public class PlayerRobotCollider : MonoBehaviour
{
	public Counter scoreCounter;
	
	private PlayerRobotMovement movement;

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
			
			scoreCounter.IncreaseBy(StageManager.instance.pointsForBonus);
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