using UnityEngine;

public class DestructionBonusTrigger : BonusTrigger
{
	public string enemyTag;
	
	public override void TriggerEffect(GameObject sender)
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

		foreach (GameObject enemy in enemies)
		{
			if(enemy.TryGetComponent<EntityExploder>(out EntityExploder ee))
			{
				ee.Explode();
			}
		}

		StageManager.instance.DefeatedEnemies += enemies.Length;
		
		base.TriggerEffect(sender);
	}
}