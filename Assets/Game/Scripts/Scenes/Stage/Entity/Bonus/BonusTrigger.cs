using UnityEngine;

public abstract class BonusTrigger : MonoBehaviour, ITriggerableOnEnter
{
	protected StageSoundManager stageSoundManager;
	
	public virtual void TriggerOnEnter(GameObject sender)
	{
		AddPointsToPlayer(sender);
		Destroy(gameObject);
	}

	private void Awake()
	{
		stageSoundManager = FindAnyObjectByType<StageSoundManager>(FindObjectsInactive.Include);
	}

	private void AddPointsToPlayer(GameObject sender)
	{
		StageManager sm = StageManager.instance;
		
		if(sender.TryGetComponent(out PlayerRobotData prd) && !sm.stateManager.GameIsOver())
		{
			sm.AddPoints(gameObject, prd.Data, sm.pointsForBonus);

			if(stageSoundManager != null)
			{
				stageSoundManager.PlaySound(SoundEffectType.BonusCollect);
			}
		}
	}
}