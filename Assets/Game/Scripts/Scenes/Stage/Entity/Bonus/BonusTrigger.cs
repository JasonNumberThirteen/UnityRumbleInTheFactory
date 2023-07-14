using UnityEngine;

public abstract class BonusTrigger : MonoBehaviour, ITriggerable
{
	public PlayerData playerData;
	
	public virtual void TriggerEffect(GameObject sender)
	{
		playerData.Score += StageManager.instance.pointsForBonus;

		StageManager.instance.uiManager.CreateGainedPointsCounter(gameObject.transform.position, StageManager.instance.pointsForBonus);
		Destroy(gameObject);
	}
}