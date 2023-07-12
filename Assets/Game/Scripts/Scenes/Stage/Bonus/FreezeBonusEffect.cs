using UnityEngine;
using System.Collections;

public class FreezeBonusEffect : BonusEffect
{
	[Min(0.01f)] public float duration = 10f;
	
	public override void PerformEffect() => StartCoroutine(FreezeEnemies());
	
	private IEnumerator FreezeEnemies()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach (GameObject enemy in enemies)
		{
			EnemyRobotMovement erm = enemy.GetComponent<EnemyRobotMovement>();
			EnemyRobotShoot ers = enemy.GetComponent<EnemyRobotShoot>();

			erm.enabled = ers.enabled = false;
		}

		yield return new WaitForSeconds(duration);

		foreach (GameObject enemy in enemies)
		{
			EnemyRobotMovement erm = enemy.GetComponent<EnemyRobotMovement>();
			EnemyRobotShoot ers = enemy.GetComponent<EnemyRobotShoot>();

			erm.enabled = ers.enabled = true;
		}
	}
}