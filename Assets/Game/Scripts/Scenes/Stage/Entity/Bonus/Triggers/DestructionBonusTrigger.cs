using UnityEngine;

public class DestructionBonusTrigger : BonusTrigger
{
	public string enemyTag;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		DestroyAllFoundEnemies();
		base.TriggerOnEnter(sender);
	}

	private void DestroyAllFoundEnemies()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		
		foreach (GameObject enemy in enemies)
		{
			DestroyEnemy(enemy);
		}

		PlayExplosionSound(enemies);
	}

	private void DestroyEnemy(GameObject enemy)
	{
		if(enemy.TryGetComponent(out EntityExploder ee))
		{
			ee.TriggerExplosion();
		}

		StageManager.instance.CountDefeatedEnemy();
	}

	private void PlayExplosionSound(GameObject[] enemies)
	{
		if(enemies.Length > 0)
		{
			StageManager.instance.audioManager.PlaySound(SoundEffectType.EnemyRobotExplosion);
		}
	}
}